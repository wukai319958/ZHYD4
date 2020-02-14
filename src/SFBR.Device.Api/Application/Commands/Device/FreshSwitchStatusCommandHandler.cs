using MediatR;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Device
{
    public class FreshSwitchStatusCommandHandler : IRequestHandler<FreshSwitchStatusCommand>
    {
        private readonly IDeviceRepository _deviceRepository;

        public FreshSwitchStatusCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<Unit> Handle(FreshSwitchStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _deviceRepository.FindByEquipNumAsync(request.UniqueId) as Terminal;
            if (entity != null)
            {
                foreach (var controller in entity.Controllers)
                {
                    var arr = controller.ControllerCode.Split('_');
                    var code = int.Parse(arr[arr.Length - 1]) - 1;
                    var status = Convert.ToChar(request.Data[code]).ToString();
                    entity.SetControllerStatus(controller.ControllerCode, status);
                    controller.SetEnable("01".IndexOf(status) > -1);
                }
                entity.SetPartStatus($"{nameof(Part)}_1", request.Door.ToString());
                entity.SetPartStatus($"{nameof(Part)}_2", request.PowerSupplyArrester.ToString());
                entity.SetPartStatus($"{nameof(Part)}_3", request.NetworkLightningArrester.ToString());
                await _deviceRepository.UnitOfWork.SaveEntitiesAsync();
            }
            return Unit.Value;
        }
    }
}
