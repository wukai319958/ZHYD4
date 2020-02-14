using Microsoft.EntityFrameworkCore;
using SFBR.EventBus.Abstractions;
using SFBR.Log.Api.Infrastructure;
using SFBR.Log.Api.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.IntegrationEvents.EventHandling
{
    public class DeviceOffLineIntegrationEventHandler : IIntegrationEventHandler<DeviceOffLineIntegrationEvent>
    {
        private readonly LogContext _db;

        public DeviceOffLineIntegrationEventHandler(LogContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task Handle(DeviceOffLineIntegrationEvent @event)
        {
#if DEBUG
            Console.WriteLine("收到警报消息：{0}", @event.Alarm.Description);
#endif
            _db.AlarmLogs.Add(new Model.AlarmLog()
            {
                Id = @event.Alarm.LogId,
                DeviceId = @event.DeviceId,
                EquipNum = @event.EquipNum,
                AlarmCode = @event.Alarm.AlarmCode,
                AlarmName = @event.Alarm.AlarmName,
                GroupName = @event.Alarm.GroupName,
                AlarmType = @event.Alarm.AlarmType,
                AlarmFrom = @event.Alarm.AlarmFrom,
                TargetCode = @event.Alarm.TargetCode,
                AlarmLevel = @event.Alarm.AlarmLevel,
                AlarmStatus = @event.Alarm.AlarmStatus,
                AlarmingDescription = @event.Alarm.Description,
                RepairTime = @event.Alarm.RepairTime,
                AlarmTime = @event.Alarm.AlarmTime,
                IsClear = false,
                CreationTime = @event.CreationDate,
                IsStatistics = @event.Alarm.IsStatistics,
                RealData = @event.Alarm.RealData,
                UpperLimit = @event.Alarm.UpperLimit,
                LowerLimit = @event.Alarm.LowerLimit
            });
            var device = await _db.Devices.FirstOrDefaultAsync(where => where.EquipNum == @event.EquipNum);
            if (device == null)
            {
                _db.Devices.Add(new Model.Device()
                {
                    Id = @event.DeviceId,
                    DeviceName = @event.DeviceName,
                    DeviceTypeCode = @event.DeviceTypeCode,
                    ModelCode = @event.ModelCode,
                    EquipNum = @event.EquipNum,
                    RegionId = @event.RegionId,
                    RegionCode = @event.RegionCode,
                    RegionName = @event.RegionName,
                    TentantId = @event.TentantId,
                    TentantName = @event.TentantName,
                    ParentId = @event.ParentId
                });
            }
            else
            {
                device.Id = @event.DeviceId;
                device.DeviceName = @event.DeviceName;
                device.DeviceTypeCode = @event.DeviceTypeCode;
                device.ModelCode = @event.ModelCode;
                device.EquipNum = @event.EquipNum;
                device.RegionId = @event.RegionId;
                device.RegionCode = @event.RegionCode;
                device.RegionName = @event.RegionName;
                device.TentantId = @event.TentantId;
                device.TentantName = @event.TentantName;
                device.ParentId = @event.ParentId;
            }

            await _db.SaveChangesAsync();
        }
    }
}
