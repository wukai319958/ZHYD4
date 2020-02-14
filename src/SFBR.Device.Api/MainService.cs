using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Serilog;
using SFBR.Device.Infrastructure;
using SFBR.IntegrationEventLogEF;
using Dapper;
using Topshelf;
using SFBR.Device.Common.Interface;
using SFBR_Client.SkynetTerminal;
using SFBR.Device.Common.Commands;
using SFBR.Device.Api.Application.Workers;

namespace SFBR.Device.Api
{
    public class MainService
    {
        private static IWebHost webHost;
        //public void Start(string[] args) => Console.WriteLine("start");
        public void Start(string[] args)
        {

            var configuration = GetConfiguration();
            RunWebHost(args, configuration);
            if(webHost != null)
            {
                var terminal = webHost.Services.GetService<ISkynetTerminalClient>() as SkynetTerminalClient;
                if(terminal != null)
                {
                    terminal.MessageCallBack += (topic, data) => TerminalWorker.Instance.Execute(data, webHost.Services);
                    terminal.Subscribe($"#.SkynetTerminal.{SkynetTerminalCmdEnum.DeviceAlarm}");//警报状态
                    terminal.Subscribe($"#.SkynetTerminal.{SkynetTerminalCmdEnum.ChannelStatus}");//开关状态
                    terminal.Subscribe($"#.SkynetTerminal.Config");//操作
                }
            }
        }

        private void RunWebHost(string[] args, IConfiguration configuration)
        {
            Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...",Program.AppName);
                webHost = BuildWebHost(configuration, args);

                Log.Information("Applying migrations ({ApplicationContext})...",Program.AppName);
                webHost.MigrateDbContext<DeviceContext>((context, services) =>
                {
                    InitData(context);//TODO:初始化种子数据
                }).MigrateDbContext<IntegrationEventLogContext>((c, s) => { });


                Log.Information("Starting web host ({ApplicationContext})...",Program.AppName);

                //webHost.Run();
                webHost.Start();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!",Program.AppName);
            }
            finally
            {
                Log.CloseAndFlush();
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
            var logstashUrl = configuration["Serilog:LogstashgUrl"];
            var actionlogUrl = configuration["Serilog:actionlogUrl"];
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", Program.AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Http(logstashUrl,restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)//如果要收集日志需要部署日志服务
                .WriteTo.Http(actionlogUrl, textFormatter: new Serilog.Formatting.Compact.CompactJsonFormatter(),restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)//操作日志
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        /// <summary>
        /// 初始化基础数据
        /// </summary>
        /// <param name="context"></param>
        private static void InitData(DeviceContext context)
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
        private static void InitSqlScript(DeviceContext context)
        {
            #region 展会临时救急，展会结束需要删除该逻辑
            try
            {
                var function = context.DeviceTypeFunctions.FirstOrDefault(where => where.FunctionCode == "onoff");
                if (function == null)
                {
                    var type = context.DeviceTypes.FirstOrDefault();
                    context.DeviceTypeFunctions.Add(new Domain.AggregatesModel.DeviceTypeAggregate.DeviceTypeFunction(type.Id, false, "回路开关", "onoff", "11", "ChannelStatus", setting: new Common.ConfigModel.SkynetTerminal.ChannelControlDto().ToJson(), settingTypeName: typeof(Common.ConfigModel.SkynetTerminal.ChannelControlDto).FullName));
                }
            }
            catch 
            {
            }
            #endregion
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
            if (webHost != null)
            {
                using (webHost)
                {
                    var eventBus = webHost.Services.GetService<EventBus.Abstractions.IEventBus>();
                    (eventBus as IDisposable)?.Dispose();
                    webHost.StopAsync(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();
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
            //if (context.GetType().Equals(typeof(IntegrationEventLogContext)))
            //{
            //    var databaseCreator = context.GetService<IRelationalDatabaseCreator>();
            //    databaseCreator.CreateTables();
            //}
            //else
            //{
            //    context.Database.EnsureDeleted();  
            //    context.Database.EnsureCreated();
            //}
#else
            context.Database.Migrate();//dotnet ef migrations add 20190924_initial -p ..\SFBR.Device.Infrastructure -s ..\SFBR.Device.Api -o ..\SFBR.Device.Api\Infrastructure\Migrations
#endif

            seeder(context, services);
        }
    }
}
