using MediatR;
using SFBR.Device.Api.Application.IntegrationEvents;
using SFBR.Device.Api.Application.IntegrationEvents.Events;
using SFBR.Device.Common.Interface;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using SFBR.Device.Domain.AggregatesModel.RegionAggregate;
using SFBR.Device.Domain.AggregatesModel.UserAggregate;
using SFBR.Device.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.DomainEventHandlers.DeviceEventHandlers
{
    public class DeviceOnlineDomainEventHandler : INotificationHandler<DeviceOnlineDomainEvent>
    {
        private readonly ISkynetTerminalClient _terminalClient;
        private readonly IDeviceIntegrationEventService _deviceIntegrationEventService;
        private readonly IRegionRepository _regionRepository;
        private readonly IUserRepository _userRepository;
        private const int delay = 200;

        public DeviceOnlineDomainEventHandler(ISkynetTerminalClient terminalClient, IDeviceIntegrationEventService deviceIntegrationEventService, IRegionRepository regionRepository, IUserRepository userRepository)
        {
            _terminalClient = terminalClient ?? throw new ArgumentNullException(nameof(terminalClient));
            _deviceIntegrationEventService = deviceIntegrationEventService ?? throw new ArgumentNullException(nameof(deviceIntegrationEventService));
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task Handle(DeviceOnlineDomainEvent notification, CancellationToken cancellationToken)
        {
            await SyncConfigProfile(notification.Device.EquipNum);
            //TODO:发布设备上线事件，用于警报日志端解除离线警报
            if (notification.Device is Terminal)
            {
                var terminal = notification.Device as Terminal;//设备详情
                var region = await _regionRepository.GetAsync(terminal.RegionId);
                var tentant = await _userRepository.GetAccountAsync(terminal.TentantId);

                await _deviceIntegrationEventService.AddAndSaveEventAsync(new DeviceOnLineIntegrationEvent(terminal.Id, terminal.DeviceName, terminal.DeviceTypeCode, terminal.ModelCode, terminal.EquipNum, terminal.RegionId, region?.RegionCode, region?.RegionName, terminal.TentantId, tentant?.Name, terminal.ParentId));
            }
        }

        /// <summary>
        /// 设备上线同步配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Task SyncConfigProfile(string id)
        {
            return Task.Run(async () =>
            {
                _terminalClient.RdDeviceInfo(id);
                await Task.Delay(delay);
                _terminalClient.RdDisarmControl(id);
                await Task.Delay(delay);
                _terminalClient.RdCameraFaultCheckTime(id);
                await Task.Delay(delay);
                _terminalClient.RdDisarmControl(id);
                await Task.Delay(delay);
                _terminalClient.RdVATHLimit(id);
                await Task.Delay(delay);
                for (int port = 1; port < 9; port++)
                {
                    _terminalClient.RdCameraIP(port, id);
                    await Task.Delay(delay);
                    _terminalClient.RdVedioAssign(port, id);
                    await Task.Delay(delay);

                }
                for (int i = 0; i < 5; i++)
                {
                    _terminalClient.RdChannelMode((Common.ConfigModel.SkynetTerminal.Enums.ChannelTypeEnum)i, id);
                    await Task.Delay(delay);
                    _terminalClient.RdChannelTask((Common.ConfigModel.SkynetTerminal.Enums.ChannelTypeEnum)i, id);
                    await Task.Delay(delay);
                }
            });

        }
    }
}
