using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace SFBR.Terminal.Web
{
    public class MainService
    {
        private static IWebHost webHost;
        public void Start(string[] args) => RunWebHost(args);

        public void Stop(string[] args)
        {
            if (webHost != null)
            {
                using (webHost)
                {
                    webHost.StopAsync(TimeSpan.FromSeconds(120)).GetAwaiter().GetResult();
                }
                if (webHost != null) webHost = null;
            }
        }
        private void RunWebHost(string[] args)
        {
            var configuration = GetConfiguration();
            Serilog.Log.Logger = CreateSerilogLogger(configuration);
            try
            {
                Serilog.Log.Information("Configuring web host ({ApplicationContext})...", Program.AppName);
                webHost = BuildWebHost(configuration, args);
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
        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", Program.AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                //.WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                //.WriteTo.Http("http://logstash:8080")//如果要收集日志需要部署日志服务
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .CaptureStartupErrors(false)
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseConfiguration(configuration)
        .UseSerilog()
        .Build();
    }
}
