using SFBR.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.IntegrationEvents.Events
{
    public class DeviceOffLineIntegrationEvent : IntegrationEvent
    {
        #region 站点信息
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
        /// 站点编号
        /// </summary>
        public string EquipNum { get;  set; }
        /// <summary>
        /// 站点分配的区域
        /// </summary>
        public string RegionId { get;  set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegionCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public string TentantId { get; set; }
        /// <summary>
        /// 租户姓名
        /// </summary>
        public string TentantName { get; set; }
        /// <summary>
        /// 上一级设备
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        #endregion
        /// <summary>
        /// 警报消息
        /// </summary>
        public Alarm Alarm { get; set; }
    }
}
