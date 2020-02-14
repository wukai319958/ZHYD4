using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.TerminalCommands
{
    /// <summary>
    /// 站点位置设置
    /// </summary>
    public class SetLocationCommand : IRequest<bool>
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 纬度（南纬为负数）
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 经度（西经为负数）
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }
    }
}
