using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Models
{
    public class CreateDeviceModel
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeCode { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string ModelCode { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipNum { get; set; }
        ///// <summary>
        ///// 设备IP地址
        ///// </summary>
        //public string DeviceIP { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string RegionId { get; set; }
    }
}
