using Microsoft.Extensions.Configuration;
using System.IO;

namespace SFBR.Device.Gateway
{
    internal class MainService
    {
        static SFBR.Gataway.SkynetTerminal.SkynetTerminalServer server;
        public void Start(string[] args)
        {
            var configuration = GetConfiguration();
            server = new SFBR.Gataway.SkynetTerminal.SkynetTerminalServer(8022,
            new Common.MsgServerInfo { HostIP = configuration["EventBusConnection"] ?? "127.0.0.1", Password = configuration["EventBusPassword"] ?? "123456", UserName = configuration["EventBusUserName"] ?? "sfbr" },
            new Common.MsgServerInfo { HostIP = configuration["EventBusConnection"] ?? "127.0.0.1", Password = configuration["EventBusPassword"] ?? "123456", UserName = configuration["EventBusUserName"] ?? "sfbr" });
            server.Start(args);
        }

        public void Stop(string[] args)
        {
            server.Stop(args);

        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}