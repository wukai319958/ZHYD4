using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.Model
{
    public class Device
    {
        /// <summary>
        /// 站点id
        /// </summary>
        public string Id { get; set; }
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
        public string EquipNum { get; protected set; }
        /// <summary>
        /// 站点分配的区域
        /// </summary>
        public string RegionId { get; private set; }
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

    }
}
