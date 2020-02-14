using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.Events
{
    public class CreateDeviceDomainEvent:MediatR.INotification
    {
        public AggregatesModel.DeviceAggregate.Device Device { get;private set; }
        public CreateDeviceDomainEvent(AggregatesModel.DeviceAggregate.Device device)
        {
            Device = device;
        }
    }
}
