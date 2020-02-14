using MediatR;
using Microsoft.Extensions.Logging;
using SFBR.Device.Api.Application.IntegrationEvents;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Device
{
    public class SetDeviceNameCommandHandler : IRequestHandler<SetDeviceNameCommand, bool>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMediator _mediator;
        private readonly IDeviceIntegrationEventService _deviceIntegrationEventService;
        private readonly ILogger<SetDeviceNameCommandHandler> _logger;

        public SetDeviceNameCommandHandler(IDeviceRepository deviceRepository, IMediator mediator, IDeviceIntegrationEventService deviceIntegrationEventService, ILogger<SetDeviceNameCommandHandler> logger)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deviceIntegrationEventService = deviceIntegrationEventService ?? throw new ArgumentNullException(nameof(deviceIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(SetDeviceNameCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetAsync(request.Id);
            if (device == null) return false;
            device.SetName(request.Name);
            _deviceRepository.Patch(device);
            return await _deviceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
