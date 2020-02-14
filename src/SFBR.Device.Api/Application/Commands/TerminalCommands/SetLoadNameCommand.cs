using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    public class SetLoadNameCommand:IRequest<bool>
    {
        /// <summary>
        /// 站点id(设备id)
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 负载id
        /// </summary>
        public string LoadId { get; set; }
        /// <summary>
        /// 负载名称
        /// </summary>
        public string Name { get; set; }
    }
}
