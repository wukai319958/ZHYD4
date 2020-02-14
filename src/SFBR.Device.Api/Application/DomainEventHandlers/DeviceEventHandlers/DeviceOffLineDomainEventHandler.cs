using MediatR;
using Microsoft.Extensions.Logging;
using SFBR.Device.Api.Application.IntegrationEvents;
using SFBR.Device.Api.Application.IntegrationEvents.Events;
using SFBR.Device.Api.Application.Queries;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate;
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
    public class DeviceOffLineDomainEventHandler : INotificationHandler<DeviceOffLineDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private readonly IDeviceIntegrationEventService _deviceIntegrationEventService;
        private readonly IRegionRepository _regionRepository;
        private readonly IUserRepository _userRepository;

        public DeviceOffLineDomainEventHandler(ILoggerFactory logger, IDeviceIntegrationEventService deviceIntegrationEventService, IRegionRepository regionRepository, IUserRepository userRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _deviceIntegrationEventService = deviceIntegrationEventService ?? throw new ArgumentNullException(nameof(deviceIntegrationEventService));
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task Handle(DeviceOffLineDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO:正在报警的子设备警报解除，同时触发警报解除事件（但不发警报解除消息）
            var terminal = notification.Device as Terminal;
            if(terminal != null)
            {
                foreach (var alarm in terminal.DeviceAlarms)
                {
                    terminal.SetAlarmStatus(alarm.AlarmCode, "0", alarm.TargetCode);
                }
                var region = await _regionRepository.GetAsync(terminal.RegionId);
                var tentant = await _userRepository.GetAccountAsync(terminal.TentantId);

                await _deviceIntegrationEventService.AddAndSaveEventAsync(new DeviceOffLineIntegrationEvent(terminal.Id, terminal.DeviceName, terminal.DeviceTypeCode, terminal.ModelCode, terminal.EquipNum, terminal.RegionId, region?.RegionCode, region?.RegionName, terminal.TentantId, tentant.Name, terminal.ParentId, terminal.Location.Latitude,terminal.Location.Longitude));

            }

        }
    }
}
