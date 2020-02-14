using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.AlarmAggregate
{
    /// <summary>
    /// 警报日志
    /// 需要考虑警报消息接收权限问题
    /// </summary>
    public class Alarm : SeedWork.Entity, SeedWork.IAggregateRoot
    {
        /// <summary>
        /// 警报代码
        /// </summary>
        [StringLength(50)]
        public string AlarmCode { get; set; }
        /// <summary>
        /// 警报名称
        /// </summary>
        [StringLength(50)]
        public string AlarmName { get; set; }
        /// <summary>
        /// 是否参与统计
        /// </summary>
        public bool IsStatistics { get; set; }
        /// <summary>
        /// 警报级别（设备类型不同，警报相同，级别仍可能不同）
        /// </summary>
        public int AlarmLevel { get; set; }
        /// <summary>
        /// 警报类型：持续性警报（温度过高）、瞬时警报（打火）
        /// </summary>
        public int AlarmType { get; private set; }
        /// <summary>
        /// 警报来源：0 监控终端；1 负载(一般只有离线、在线状态)
        /// </summary>
        public int AlarmFrom { get; private set; }
        /// <summary>
        /// 警报分组（温度警报、电压警报、电流警报等等）
        /// </summary>
        [StringLength(50)]
        public string GroupName { get; private set; }
        /// <summary>
        /// 警报状态(未解除、已解除)
        /// </summary>
        public int AlarmStatus { get; set; }
        /// <summary>
        /// 警报解除的原因：1 设备推送解除；2 设备掉线解除；3 人工强制解除。该字段由系统维护
        /// </summary>
        public string ClearType { get; set; }
        /// <summary>
        /// 终端id或者负载id
        /// </summary>
        [StringLength(50)]
        public string DeviceId { get; set; }
        /// <summary>
        /// 设备名称/负载名称
        /// </summary>
        [StringLength(50)]
        public string DeviceName { get; set; }
        /// <summary>
        /// 编号/负载编号
        /// </summary>
        [StringLength(50)]
        public string EquipNum { get; set; }
        /// <summary>
        /// 设备类型/负载类型
        /// </summary>
        [StringLength(50)]
        public string DeviceTypeCode { get; set; }
        /// <summary>
        /// 监控的回路编号/负载的回路编号
        /// </summary>
        public int PortNumber { get; set; }
        /// <summary>
        /// 监控的回路名称/负载的回路名称
        /// </summary>
        [StringLength(50)]
        public string PortDefaultName { get; set; }
        /// <summary>
        /// 主机id
        /// </summary>
        [StringLength(50)]
        public string ParentId { get; set; }
        /// <summary>
        /// 主机编号
        /// </summary>
        [StringLength(50)]
        public int ParentNum { get; set; }
        /// <summary>
        /// 主机名称
        /// </summary>
        [StringLength(50)]
        public string ParentName { get; set; }
        /// <summary>
        /// 区域id
        /// </summary>
        [StringLength(50)]
        public string RegionId { get; set; }//一般固定且数量不大可以在内存中关联
        /// <summary>
        /// 租户
        /// </summary>
        [StringLength(50)]
        public string TentantId { get; private set; }
        /// <summary>
        /// 警报发生时间
        /// </summary>
        public DateTime AlarmTime { get; set; }
        /// <summary>
        /// 警报解除时间
        /// </summary>
        public DateTime ClearTime { get; set; }

    }
}
