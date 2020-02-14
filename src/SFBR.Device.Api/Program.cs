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

namespace SFBR.Device.Api
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = "SFBRDeviceApi";
        public const string ServiceName = "四方博瑞设备管理系统API服务4.0";

        public static void Main(string[] args) => Run(args);


        private static void Run(string[] args)
        {
            //Console.Title = ServiceName;// "四方博瑞设备管理系统API服务4.0";
            //捕获未处理的异常
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Console.WriteLine(e);
            };
            //启动服务
            HostFactory.Run(config =>
            {
                config.Service<MainService>(s =>
                {
                    s.ConstructUsing(() => new MainService());
                    s.WhenStarted(ws => ws.Start(args));
                    s.WhenStopped(ws => ws.Stop(args));
                });

                config.SetDescription(ServiceName);
                config.SetDisplayName(ServiceName);
                config.SetServiceName("SFBR_Device_Api4");

                //启动方式
                config.StartAutomaticallyDelayed();//必须延时启动，否则数据库尚未启动服务可能启动失败
                //config.StartAutomatically();
                //服务运行账户
                config.RunAsLocalSystem();
                //config.
                //自动恢复（服务重启）设置
                config.EnableServiceRecovery(reStart =>
                {
                    //第一次重启
                    reStart.RestartService(1);
                    //仅在崩溃时重启
                    reStart.OnCrashOnly();
                    // 恢复计算周期
                    reStart.SetResetPeriod(1);
                });
                config.OnException(e => Console.WriteLine(e));
            });
        }
    }

}
