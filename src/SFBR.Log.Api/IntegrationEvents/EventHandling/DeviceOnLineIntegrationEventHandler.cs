using SFBR.EventBus.Abstractions;
using SFBR.Log.Api.Infrastructure;
using SFBR.Log.Api.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.IntegrationEvents.EventHandling
{
    public class DeviceOnLineIntegrationEventHandler : IIntegrationEventHandler<DeviceOnLineIntegrationEvent>
    {

        private readonly LogContext _db;

        public DeviceOnLineIntegrationEventHandler(LogContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }


        public async Task Handle(DeviceOnLineIntegrationEvent @event)
        {
            var sameAlarms = _db.AlarmLogs.Where(w => w.EquipNum == @event.EquipNum && w.IsClear == false && w.AlarmCode == @event.AlarmCode).ToList();
            if (sameAlarms != null && sameAlarms.Count > 0)
            {
                foreach (var alarm in sameAlarms)
                {
                    alarm.ClearTime = @event.ClearTime;
                    alarm.ClearReason = @event.ClearReason;
                    alarm.CreationTime = @event.CreationDate;
                    alarm.AlarmedDescription = @event.Description;
                }
                await _db.SaveChangesAsync();
            }
        }
    }
}
