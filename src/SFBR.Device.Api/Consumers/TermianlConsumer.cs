using Microsoft.Extensions.Configuration;
using SFBR.Device.Common;
using SFBR.Device.Common.Commands;
using SFBR_Client.SkynetTerminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Consumers
{
    public class TermianlConsumer
    {
        //TODO:生成警报，设备指令发送，设备上下线，千万别订阅实时数据，实时数据交给数据采集服务
        private readonly SkynetTerminalClient _client;
        private IConfiguration _configuration;
        private IServiceProvider _serviceProvider;


        public TermianlConsumer(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _client = new SkynetTerminalClient(configuration["EventBusConnection"], configuration["EventBusConnection"]);
            _client.MessageCallBack += Client_MessageCallBack;
            _client.Subscribe($"#.SkynetTerminal.{SkynetTerminalCmdEnum.DeviceAlarm}");
            _client.Subscribe($"#.SkynetTerminal.{SkynetTerminalCmdEnum.ChannelStatus}");
        }

        private async void Client_MessageCallBack(BaseResultDto<SkynetTerminalCmdEnum> data)
        {
        }

    }
}
