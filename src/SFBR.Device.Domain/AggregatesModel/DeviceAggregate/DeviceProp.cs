using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 负载扩展属性
    /// </summary>
    public class DeviceProp : SeedWork.Entity
    {
        protected DeviceProp()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceProp(string deviceId, string propName, string propText = null, string propType = null,string groupName = null, bool enabled = true, string propValue = null,bool canRemove = true)
            : this()
        {
            DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
            //PropCode = propCode ?? throw new ArgumentNullException(nameof(propCode));
            PropName = propName ?? throw new ArgumentNullException(nameof(propName));
            PropText = propText;
            PropType = propType;
            //PropDefaultValue = propDefaultValue;
            GroupName = groupName;
            Enabled = enabled;
            PropValue = propValue;
            CanRemove = canRemove;
        }
        /// <summary>
        /// 负载id
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceId { get; private set; }
        /// <summary>
        /// 属性名称（前端绑定的变量）
        /// </summary>
        [StringLength(150)]
        [Required]
        public string PropName { get; private set; }
        /// <summary>
        /// 属性显示文本（前端提示文本）
        /// </summary>
        [StringLength(150)]
        public string PropText { get; private set; }
        /// <summary>
        /// 属性类型（默认字符串）
        /// </summary>
        [StringLength(1500)]
        public string PropType { get; private set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(50)]
        public string GroupName { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; private set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string PropValue { get; private set; }
        /// <summary>
        /// 是否可以删除（某些固定属性不允许删除）
        /// </summary>
        public bool CanRemove { get; set; }
    }
}
