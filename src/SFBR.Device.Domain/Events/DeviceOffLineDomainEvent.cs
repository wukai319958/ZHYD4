using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceOffLineDomainEvent : MediatR.INotification
    {
        public AggregatesModel.DeviceAggregate.Device Device { get; private set; }

        public DeviceOffLineDomainEvent(AggregatesModel.DeviceAggregate.Device device)
        {
            Device = device;
        }
    }
}
