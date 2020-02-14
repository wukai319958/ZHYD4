using MediatR;
using SFBR.Device.Common;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Device
{
    public class FreshTerminalConnectionCommandHandler : IRequestHandler<FreshTerminalConnectionCommand>
    {
        private readonly IDeviceRepository _deviceRepository;

        public FreshTerminalConnectionCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<Unit> Handle(FreshTerminalConnectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _deviceRepository.FindByEquipNumAsync(request.UniqueId) as Terminal;
            if (entity != null)
            {
                if (request.CustomEnum == CustomEnum.OnLine)
                {
                    entity.SetConnetion(1);//上线
                }
                else if (request.CustomEnum == CustomEnum.OffLine)
                {
                    entity.SetConnetion(0);
                }
                else if (request.CustomEnum == CustomEnum.IPChange)
                {
                    entity.SetAddress(request.FromIP, request.FromPort);
                }
                await _deviceRepository.UnitOfWork.SaveEntitiesAsync();
            }
            return Unit.Value;
        }
    }
}
