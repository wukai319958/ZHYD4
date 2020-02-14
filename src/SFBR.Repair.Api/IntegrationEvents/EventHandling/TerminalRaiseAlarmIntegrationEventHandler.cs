using SFBR.EventBus.Abstractions;
using SFBR.Repair.Api.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Repair.Api.IntegrationEvents.EventHandling
{
    public class TerminalRaiseAlarmIntegrationEventHandler :
        IIntegrationEventHandler<TerminalRaiseAlarmIntegrationEvent>
    {
        public Task Handle(TerminalRaiseAlarmIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
