using MediatR;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Device
{
    public class FreshAlarmStatusCommandHandler : IRequestHandler<FreshAlarmStatusCommand>
    {
        private readonly IDeviceRepository _deviceRepository;

        public FreshAlarmStatusCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<Unit> Handle(FreshAlarmStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _deviceRepository.FindByEquipNumAsync(request.UniqueId) as Terminal;
            if (entity != null)
            {
                foreach (var alarm in entity.DeviceAlarms)
                {
                    var arr = alarm.AlarmCode.Split('_');
                    var code = int.Parse(arr[arr.Length - 1]) - 1;
                    var status = Convert.ToChar(request.Data[code]).ToString();
                    entity.SetAlarmStatus(alarm.AlarmCode, status, alarm.TargetCode);
                }
                entity.SetConnetion(1);//修改设备状态
                entity.SetAddress(request.FromIP, request.FromPort);//修改设备地址
                await _deviceRepository.UnitOfWork.SaveEntitiesAsync();
            }
            return Unit.Value;
        }
    }
}
