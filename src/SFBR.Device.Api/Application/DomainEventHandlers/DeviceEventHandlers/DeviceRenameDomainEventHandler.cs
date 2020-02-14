using MediatR;
using SFBR.Device.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.DomainEventHandlers.DeviceEventHandlers
{
    public class DeviceRenameDomainEventHandler : INotificationHandler<DeviceRenameDomainEvent>
    {

        public Task Handle(DeviceRenameDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
