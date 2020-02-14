using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceOnlineDomainEvent:MediatR.INotification
    {
        public AggregatesModel.DeviceAggregate.Device Device { get; private set; }

        public DeviceOnlineDomainEvent(AggregatesModel.DeviceAggregate.Device device)
        {
            Device = device;
        }

    }
}
