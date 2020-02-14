using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    public class SetRegionCommand:IRequest<bool>
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 区域id
        /// </summary>
        public string RegionId { get; set; }
    }
}
