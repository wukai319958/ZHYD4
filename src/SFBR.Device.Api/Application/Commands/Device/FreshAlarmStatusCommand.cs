using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Device
{
    public class FreshAlarmStatusCommand :Common.ConfigModel.SkynetTerminal.Models.DeviceAlarmResultDto, IRequest
    {
    }
}
