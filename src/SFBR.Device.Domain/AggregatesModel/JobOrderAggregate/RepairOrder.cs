using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.JobOrderAggregate
{
    /// <summary>
    /// 维修任务单
    /// </summary>
    public class RepairOrder:JobOrder
    {
        /// <summary>
        /// 区域id
        /// </summary>
        [StringLength(50)]
        public string RegionId { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 设备编号（站点编号）
        /// </summary>
        [StringLength(50)]
        public string EquipNum { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        [StringLength(50)]
        public string DevceId { get; set; }
        /// <summary>
        /// 设备名称（站点名称）
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 维修类型：智维终端、配件、负载、传感器、控制器、输出回路
        /// </summary>
        public string RepairType { get; set; }
        ///// <summary>
        ///// 负载id
        ///// </summary>
        //public string LoadId { get; set; }
        ///// <summary>
        ///// 负载名称
        ///// </summary>
        //public string LoadName { get; set; }
        ///// <summary>
        ///// 警报端口
        ///// </summary>
        //public int PortNumber { get; set; }
        ///// <summary>
        ///// 端口名称
        ///// </summary>
        //public string PortDefaultName { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string BrandId { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string BrandName { get; set; }
        ///// <summary>
        ///// 关联的警报id
        ///// </summary>
        //public string AlarmId { get; set; }
        /// <summary>
        /// 故障发生时间
        /// </summary>
        public DateTime? FaultTime { get; set; }
        /// <summary>
        /// 故障原因
        /// </summary>
        public string FaultReason { get; set; }
        ///// <summary>
        ///// 故障描述
        ///// </summary>
        //public string FaultDescription { get; set; }

    }
}
