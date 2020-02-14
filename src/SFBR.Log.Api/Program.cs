using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Topshelf;

namespace SFBR.Log.Api
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = "SFBRLogApi";


        public static void Main(string[] args) => Run(args);


        private static void Run(string[] args)
        {
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

                config.SetDescription("四方博瑞日志API服务2.0");
                config.SetDisplayName("四方博瑞日志API服务2.0");
                config.SetServiceName("SFBR_Log_Api2");

                //启动方式
                config.StartAutomaticallyDelayed();//必须延时启动，否则数据库尚未启动服务可能启动失败
                //config.StartAutomatically();
                //服务运行账户
                config.RunAsLocalSystem();
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
