using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFBR.Device.Api.Application.Commands.Device;
using Microsoft.Extensions.Logging;

namespace SFBR.Device.Api.Application.Validations
{
    public class CreateDeviceValidator: AbstractValidator<CreateDeviceCommand>
    {
        public CreateDeviceValidator(ILogger<CreateDeviceValidator> logger)
        {
            RuleFor(cmd => cmd.DeviceName).NotEmpty().MaximumLength(12).WithMessage("设备名称不可以为空，且不可以超过12个字符");//注意全球化
            RuleFor(cmd => cmd.DeviceTypeCode).NotEmpty().MaximumLength(50).WithMessage("设备类型不可以为空，且不可以超过50个字符");//注意全球化
            RuleFor(cmd => cmd.ModelCode).NotEmpty().MaximumLength(50).WithMessage("设备型号不可以为空，且不可以超过50个字符");//注意全球化
            RuleFor(cmd => cmd.EquipNum).NotEmpty().MaximumLength(50).WithMessage("设备编号不可以为空，且不可以超过50个字符");//注意全球化
            RuleFor(cmd => cmd.DeviceIP).Length(7, 50).WithMessage("设备IP长度必须在8到50个字符之间");//注意全球化
        }
    }
}
