using System;
using System.Collections.Generic;
using System.Text;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;

namespace SFBR.Device.Domain.Events
{
    public class TerminalResetRegionDomainEvent : MediatR.INotification
    {
        public Device.Domain.AggregatesModel.DeviceAggregate.Device Device { get; private set; }

        public TerminalResetRegionDomainEvent(AggregatesModel.DeviceAggregate.Device device)
        {
            Device = device ?? throw new ArgumentNullException(nameof(device));
        }
    }
}
