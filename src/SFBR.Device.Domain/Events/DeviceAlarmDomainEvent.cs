using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceAlarmStatusChangeDomainEvent : MediatR.INotification
    {
        public string OldStatus { get; private set; }

        public string AlarmCode { get; private set; }
        public string TargetCode { get; private set; }

        public AggregatesModel.DeviceAggregate.Device Device { get; private set; }

        public DeviceAlarmStatusChangeDomainEvent(AggregatesModel.DeviceAggregate.Device device,string alarmCode,string oldStatus,string targetCode)
        {
            Device = device;
            AlarmCode = alarmCode;
            OldStatus = oldStatus;
            TargetCode = targetCode;
        }

    }
}
