using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    /// <summary>
    /// 修改回路默认名称
    /// </summary>
    public class SetChannelNameCommand : IRequest<bool>
    {
        /// <summary>
        /// 回路名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PortNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
    }
}
