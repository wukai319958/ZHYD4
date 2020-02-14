using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Serilog;
using SFBR.IntegrationEventLogEF;
using Dapper;
using SFBR.Data.Api.Infrastructure;

namespace SFBR.Data.Api
{
    public class MainService
    {
        private static IWebHost webHost;
        private static TerminalConsumer consumer;

        public void Start(string[] args)
        {
            var configuration = GetConfiguration();
            RunWebHost(args, configuration);
            if (consumer == null)
                consumer = new TerminalConsumer(configuration, webHost.Services);
        }


        private void RunWebHost(string[] args, IConfiguration configuration)
        {

            Serilog.Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                Serilog.Log.Information("Configuring web host ({ApplicationContext})...", Program.AppName);
                webHost = BuildWebHost(configuration, args);

                Serilog.Log.Information("Applying migrations ({ApplicationContext})...", Program.AppName);
                webHost
                    .MigrateDbContext<DataContext>((context, services) =>
                {
                    InitData(context);//TODO:初始化种子数据
                }).MigrateDbContext<IntegrationEventLogContext>((c, s) => { });


                Serilog.Log.Information("Starting web host ({ApplicationContext})...", Program.AppName);

                webHost.Start();
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", Program.AppName);
            }
            finally
            {
                Serilog.Log.CloseAndFlush();
            }
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(configuration)
                .UseSerilog()
                .Build();

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            //var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            //var logstashUrl = configuration["Serilog:LogstashgUrl"];
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", Program.AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                //.WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                //.WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)//如果要收集日志需要部署日志服务
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        /// <summary>
        /// 初始化基础数据
        /// </summary>
        /// <param name="context"></param>
        private static void InitData(DataContext context)
        {
            InitSqlScript(context);
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "baseData");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                return;
            }
            var files = Directory.GetFiles(dir, "*.sql");
            if (files.Length <= 0) return;
            using (var connection = context.Database.GetDbConnection())
            {
                if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                foreach (var file in files)
                {
                    string name = Path.GetFileName(file);
                    if (string.IsNullOrEmpty(name)) continue;
                    //检查系统表是否存在数据
                    var hasData = connection.ExecuteScalar($"select count(1) from {name}");
                    if (hasData != null) continue;//已经存在数据不再初始化
                    string script = File.ReadAllText(file);
                    if (string.IsNullOrEmpty(script)) continue;
                    connection.Execute(script);
                }
            }
        }
        /// <summary>
        /// 初始化视图、函数、存储过程等
        /// </summary>
        private static void InitSqlScript(DataContext context)
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "schema");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                return;
            }
            var files = Directory.GetFiles(dir, "*.sql");
            if (files.Length <= 0) return;
            using (var connection = context.Database.GetDbConnection())
            {
                if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                foreach (var file in files)
                {
                    string name = Path.GetFileName(file);
                    if (string.IsNullOrEmpty(name)) continue;
                    string script = File.ReadAllText(file);
                    if (string.IsNullOrEmpty(script)) continue;
                    connection.Execute(script);
                }
            }
        }


        public void Stop(string[] args)
        {
            consumer?.Dispose();
            if (webHost != null)
            {
                using (webHost)
                {
                    var eventBus = webHost.Services.GetService<EventBus.Abstractions.IEventBus>();
                    (eventBus as IDisposable)?.Dispose();
                    webHost.StopAsync(TimeSpan.FromSeconds(120)).GetAwaiter().GetResult();
                }
                if (webHost != null) webHost = null;
            }
        }
    }

    static class IWebHostExtensions
    {
        public static bool IsInKubernetes(this IWebHost webHost)
        {
            var cfg = webHost.Services.GetService<IConfiguration>();
            var orchestratorType = cfg.GetValue<string>("OrchestratorType");
            return orchestratorType?.ToUpper() == "K8S";
        }

        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            var underK8s = webHost.IsInKubernetes();

            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<TContext>>();

                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    if (underK8s)
                    {
                        InvokeSeeder(seeder, context, services);
                    }
                    else
                    {
                        var retry = Policy.Handle<SqlException>()
                             .WaitAndRetry(new TimeSpan[]
                             {
                             TimeSpan.FromSeconds(3),
                             TimeSpan.FromSeconds(5),
                             TimeSpan.FromSeconds(8),
                             });
                        retry.Execute(() => InvokeSeeder(seeder, context, services));
                    }

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                    if (underK8s)
                    {
                        throw;
                    }
                }
            }

            return webHost;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : DbContext
        {
            //仅开发初期使用，当发布第一版之后必须使用 context.Database.Migrate() 初始化数据库
#if DEBUG
            if (context.GetType().Equals(typeof(IntegrationEventLogContext)))
            {
                var databaseCreator = context.GetService<IRelationalDatabaseCreator>();
                databaseCreator.CreateTables();
            }
            else
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
#else
            context.Database.Migrate();//dotnet ef migrations add 20190924_initial -p ..\SFBR.Device.Infrastructure -s ..\SFBR.Device.Api -o ..\SFBR.Device.Api\Infrastructure\Migrations
#endif

            seeder(context, services);
        }
    }
}
