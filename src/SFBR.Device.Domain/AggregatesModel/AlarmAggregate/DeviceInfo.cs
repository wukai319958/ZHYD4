using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.AlarmAggregate
{
    /// <summary>
    /// 缓存设备基础信息
    /// </summary>
    public class DeviceInfo:SeedWork.Entity
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
        /// 站点编号
        /// </summary>
        public string EquipNum { get; protected set; }
        /// <summary>
        /// 站点分配的区域
        /// </summary>
        public string RegionId { get; private set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public string TentantId { get; set; }
    }
}
