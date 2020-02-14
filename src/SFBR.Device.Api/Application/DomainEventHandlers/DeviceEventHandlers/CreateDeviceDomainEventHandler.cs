using MediatR;
using SFBR.Device.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.DomainEventHandlers.DeviceEventHandlers
{
    public class CreateDeviceDomainEventHandler : INotificationHandler<CreateDeviceDomainEvent>
    {
        public CreateDeviceDomainEventHandler()
        {

        }

        public Task Handle(CreateDeviceDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO:发现的设备中移除（自动添加的设备不会被放入发现的设备）
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
