using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SFBR.Device.Common;
using SFBR.Device.Common.Commands;

namespace SFBR.Device.Api.Application.Commands.Device
{
    public class FreshConfigCommand:IRequest
    {
        public FreshConfigCommand(BaseResultDto<SkynetTerminalCmdEnum> value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Common.BaseResultDto<Common.Commands.SkynetTerminalCmdEnum> Value { get; private set; }
    }
}
