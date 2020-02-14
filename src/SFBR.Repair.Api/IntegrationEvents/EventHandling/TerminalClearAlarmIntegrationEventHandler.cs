using SFBR.EventBus.Abstractions;
using SFBR.Repair.Api.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Repair.Api.IntegrationEvents.EventHandling
{
    public class TerminalClearAlarmIntegrationEventHandler :
        IIntegrationEventHandler<TerminalClearAlarmIntegrationEvent>
    {
        public Task Handle(TerminalClearAlarmIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
