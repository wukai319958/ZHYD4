using MediatR;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    public class SetChannelEnabledCommandHandler : IRequestHandler<SetChannelEnabledCommand, bool>
    {
        private readonly IDeviceRepository _deviceRepository;

        public SetChannelEnabledCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<bool> Handle(SetChannelEnabledCommand request, CancellationToken cancellationToken)
        {
            var entity = await _deviceRepository.GetAsync(request.Id) as Terminal;
            if (entity == null) return false;
            entity.SetChannelEnabled(request.PortNumber, request.Enabled);
            return await _deviceRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
