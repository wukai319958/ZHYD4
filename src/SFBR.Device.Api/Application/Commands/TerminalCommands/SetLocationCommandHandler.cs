using MediatR;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    public class SetLocationCommandHandler : IRequestHandler<SetLocationCommand, bool>
    {

        private readonly IDeviceRepository _deviceRepository;

        public SetLocationCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<bool> Handle(SetLocationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _deviceRepository.GetAsync(request.Id) as Terminal;
            if (entity == null) return false;
            entity.SetLocation(request.Latitude, request.Longitude, true, request.Province, request.City, request.District);
            return await _deviceRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
