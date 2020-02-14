using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Device
{
    public class FreshSwitchStatusCommand : Common.ConfigModel.SkynetTerminal.Models.ChannelStatusResultDto, IRequest
    {

    }
}
