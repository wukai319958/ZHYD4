using MediatR;
using Microsoft.Extensions.Logging;
using MQTTnet.Client;
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
    public class DeviceAlarmStatusChangeDomainEventHandler : INotificationHandler<DeviceAlarmStatusChangeDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private readonly IDeviceIntegrationEventService _deviceIntegrationEventService;
        private readonly IRegionRepository _regionRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IMqttClient _mqttClient;

        public DeviceAlarmStatusChangeDomainEventHandler(ILoggerFactory logger, IDeviceTypeRepository deviceTypeRepository, IDeviceIntegrationEventService deviceIntegrationEventService, IDeviceRepository deviceRepository, IRegionRepository regionRepository, IUserRepository userRepository, IMqttClient mqttClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _deviceIntegrationEventService = deviceIntegrationEventService ?? throw new ArgumentNullException(nameof(deviceIntegrationEventService));
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _deviceTypeRepository = deviceTypeRepository ?? throw new ArgumentNullException(nameof(deviceTypeRepository));
            _mqttClient = mqttClient ?? throw new ArgumentNullException(nameof(mqttClient));
        }

        public async Task Handle(DeviceAlarmStatusChangeDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Device is Terminal)
            {
                var terminal = notification.Device as Terminal;//设备详情
                var arr = notification.TargetCode.Split('_');
                string codeNum = arr[arr.Length - 1];
                var alarmSetting = terminal.DeviceAlarms.FirstOrDefault(where => where.AlarmCode == notification.AlarmCode);//警报状态及配置
                if (nameof(Domain.AggregatesModel.DeviceAggregate.Camera).Equals(arr[0], StringComparison.InvariantCultureIgnoreCase))
                {
                    var camera = terminal.Loads.FirstOrDefault(where => where.EquipNum.EndsWith(notification.TargetCode));
                    var status = int.Parse(alarmSetting.Status);
                    camera.SetConnetion(status == 0 ? 1 : 0);
                }
                else if (nameof(Domain.AggregatesModel.DeviceAggregate.Sensor).Equals(arr[0], StringComparison.InvariantCultureIgnoreCase))
                {
                    terminal.SetSensorAlarmStatus(notification.TargetCode, alarmSetting.Status);
                }
                else if (nameof(Domain.AggregatesModel.DeviceAggregate.Part).Equals(arr[0], StringComparison.InvariantCultureIgnoreCase))
                {
                    terminal.SetPartAlarmStatus(notification.TargetCode, alarmSetting.Status);
                }
                var items = terminal.ModelCode.Split('-');
                var version = items[items.Length - 1];
                string alarmId = Guid.NewGuid().ToString();
                var region = await _regionRepository.GetAsync(terminal.RegionId);
                var type = await _deviceTypeRepository.FindAsync(terminal.DeviceTypeCode, version);
                var alarmInfo = type.Alarms.FirstOrDefault(where => where.AlarmCode == alarmSetting.AlarmCode);

                if (alarmSetting.NormalValue.Contains(alarmSetting.Status))
                {
                    await _deviceIntegrationEventService.AddAndSaveEventAsync(new TerminalClearAlarmIntegrationEvent(terminal.Id, terminal.DeviceName, terminal.DeviceTypeCode, terminal.ModelCode, terminal.EquipNum, alarmSetting.TargetCode, alarmSetting.AlarmCode, terminal.Connection == 0 ? 1 : 0, GetAlarmDescription(alarmSetting, alarmInfo, region, terminal)));//解除警报
                }
                else
                {
                    var tentant = await _userRepository.GetAccountAsync(terminal.TentantId);
                    var sensor = terminal.Sensors.FirstOrDefault(where => where.SensorCode == alarmSetting.TargetCode);
                    await _deviceIntegrationEventService.AddAndSaveEventAsync(new TerminalRaiseAlarmIntegrationEvent
                         (terminal.Id, terminal.DeviceName, terminal.DeviceTypeCode, terminal.ModelCode, terminal.EquipNum, terminal.RegionId, region?.RegionCode, region?.RegionName, terminal.TentantId, tentant?.Name, terminal.ParentId, terminal.Location.Latitude, terminal.Location.Longitude,
                         new Alarm(alarmId, alarmSetting.AlarmCode, alarmInfo.AlarmName, alarmInfo.GroupName, alarmInfo.AlarmType, (int)alarmInfo.AlarmFrom, alarmSetting.TargetCode ?? alarmInfo.TargetCode, alarmInfo.AlarmLevel, alarmSetting.Status, GetAlarmDescription(alarmSetting, alarmInfo, region, terminal), alarmSetting.RepairTime ?? alarmInfo.RepairTime, DateTime.UtcNow, alarmInfo.IsStatistics, sensor?.RealValue, sensor?.UpperValue, sensor?.LowerValue)
                         ));//发生警报

                }
            }
            //todo:补全内容

            //TODO:推送警报状态给前端页面（在其他服务中订阅并执行）

            //TODO:发送短信、邮件等其他操作（在其他服务中订阅并执行）

            //TODO:触发分布式消息（在其他服务中订阅并执行）
        }

        private string GetAlarmDescription(Domain.AggregatesModel.DeviceAggregate.DeviceAlarm deviceAlarm, Domain.AggregatesModel.DeviceTypeAggregate.DeviceTypeAlarm typeAlarm, Domain.AggregatesModel.RegionAggregate.Region region, Device.Domain.AggregatesModel.DeviceAggregate.Device device)
        {
            string template = @"{0}编号{1}的站点{2}，请及时处理！";
            if (deviceAlarm.Status == "0")//警报解除
            {
                template = @"{0}编号{1}的站点{2}";
            }
            var arr = typeAlarm?.StatusMapDescription?.Split(',');
            if (arr == null || arr.Length == 0) return null;
            foreach (var item in arr)
            {
                if (item.StartsWith(deviceAlarm.Status))
                {
                    return string.Format(template, region?.RegionName, device.EquipNum, item.Substring(1));
                }
            }
            return null;
        }

    }
}
