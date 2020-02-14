using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    /// <summary>
    /// 
    /// </summary>
    public class LockSettingCommand:IRequest<bool>
    {
        /// <summary>
        /// 站点id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 功能编号
        /// </summary>
        public string FunctionCode { get; set; }
        /// <summary>
        /// 锁状态
        /// </summary>
        public bool LockStatus { get; set; }
    }
}
