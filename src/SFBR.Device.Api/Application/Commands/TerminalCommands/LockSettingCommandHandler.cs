using MediatR;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    public class LockSettingCommandHandler : IRequestHandler<LockSettingCommand, bool>
    {
        private readonly IDeviceRepository _deviceRepository;

        public LockSettingCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<bool> Handle(LockSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _deviceRepository.GetAsync(request.Id) as Terminal;
            if (entity == null) return false;
            entity.SetFunctionLockSetting(request.FunctionCode, request.LockStatus);
            return await _deviceRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
