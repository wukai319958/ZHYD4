using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    public class SetChannelEnabledCommand:IRequest<bool>
    {
        /// <summary>
        /// 站点id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 回路编号
        /// </summary>
        public int PortNumber { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }
}
