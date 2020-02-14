using SFBR.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.IntegrationEvents.Events
{
    /// <summary>
    /// 解除警报
    /// </summary>
    public class TerminalClearAlarmIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// 站点id
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeCode { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string ModelCode { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipNum { get; set; }
        /// <summary>
        /// 子设备编号
        /// </summary>
        public string TargetCode { get; set; }
        /// <summary>
        /// 警报编号
        /// </summary>
        public string AlarmCode { get; set; }
        /// <summary>
        /// 解除报警时间
        /// </summary>
        public DateTime? ClearTime { get; set; }
        /// <summary>
        /// 解除原因：正常解除，主机掉线
        /// </summary>
        public int ClearReason { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
