using MediatR;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    public class SetChannelNameCommandHandler : IRequestHandler<SetChannelNameCommand, bool>
    {
        private readonly IDeviceRepository _deviceRepository;

        public SetChannelNameCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<bool> Handle(SetChannelNameCommand request, CancellationToken cancellationToken)
        {
            var entity = await _deviceRepository.GetAsync(request.Id) as Terminal;
            if (entity == null) return false;
            entity.SetChannelName(request.PortNumber, request.Name);
            return await _deviceRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
