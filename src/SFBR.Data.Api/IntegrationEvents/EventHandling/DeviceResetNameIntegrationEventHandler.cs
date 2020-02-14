using SFBR.Data.Api.IntegrationEvents.Events;
using SFBR.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.IntegrationEvents.EventHandling
{
    public class DeviceResetNameIntegrationEventHandler : IIntegrationEventHandler<DeviceResetNameIntegrationEvent>
    {
        public Task Handle(DeviceResetNameIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
