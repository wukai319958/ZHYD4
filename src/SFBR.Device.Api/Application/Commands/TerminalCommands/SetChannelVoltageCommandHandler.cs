using MediatR;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{

    public class SetChannelVoltageCommandHandler : IRequestHandler<SetChannelVoltageCommand, bool>
    {

        private readonly IDeviceRepository _deviceRepository;

        public SetChannelVoltageCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<bool> Handle(SetChannelVoltageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _deviceRepository.GetAsync(request.Id) as Terminal;
            if (entity == null) return false;
            entity.SetChannelVoltage(request.PortNumber, request.Voltage);
            return await _deviceRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
