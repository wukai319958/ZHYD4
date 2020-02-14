using SFBR.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 读取或者配置设备
    /// </summary>
    public class ReadOrSetDeviceConfigEvent : IntegrationEvent
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipNum { get; set; }
        /// <summary>
        /// 设备IP地址
        /// </summary>
        public string DeviceIP { get; set; }
        /// <summary>
        /// 设备端口
        /// </summary>
        public int DevicePort { get; set; }

    }
}
