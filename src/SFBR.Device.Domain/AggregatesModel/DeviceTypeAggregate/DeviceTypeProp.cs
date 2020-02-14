using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate
{
    /// <summary>
    /// 负载扩展信息
    /// </summary>
    public class DeviceTypeProp:SeedWork.Entity
    {
        protected DeviceTypeProp()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceTypeProp(string deviceTypeId, string propCode, string propName, string propText, string propType, string propDefaultValue, string groupName,bool enabled = true)
            : this()
        {
            DeviceTypeId = deviceTypeId ?? throw new ArgumentNullException(nameof(deviceTypeId));
            PropCode = propCode ?? throw new ArgumentNullException(nameof(propCode));
            PropName = propName ?? throw new ArgumentNullException(nameof(propName));
            PropText = propText ?? propName;
            PropType = propType ?? throw new ArgumentNullException(nameof(propType));
            PropDefaultValue = propDefaultValue;
            GroupName = groupName ?? throw new ArgumentNullException(nameof(groupName));
            Enabled = enabled;
        }



        /// <summary>
        /// 负载类型id
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceTypeId { get;protected set; }
        /// <summary>
        /// 属性编号（刷新数据的变量）
        /// </summary>
        [StringLength(150)]
        [Required]
        public string PropCode { get;private set; }
        /// <summary>
        /// 属性名称（前端绑定的变量）
        /// </summary>
        [StringLength(150)]
        [Required]
        public string PropName { get;private set; }
        /// <summary>
        /// 属性显示文本（前端提示文本）
        /// </summary>
        [StringLength(150)]
        public string PropText { get;private set; }
        /// <summary>
        /// 属性类型（默认字符串）
        /// </summary>
        [StringLength(1500)]
        public string PropType { get;private set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string PropDefaultValue { get;private set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(50)]
        public string GroupName { get;private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; private set; }

    }
}
