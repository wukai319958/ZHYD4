using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    public class DeviceExtension
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropName { get; set; }
        /// <summary>
        /// 属性显示文本
        /// </summary>
        public string PorpText { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string PropValue { get; set; }
        /// <summary>
        /// 属性值类型
        /// </summary>
        public PropType PropType { get; set; }
        /// <summary>
        /// 属性值格式化样式
        /// </summary>
        public string Formatter { get; set; }

    }

    public enum PropType
    {
        String = 0,
        Number = 1,
        Date = 2,
        Object = 4
    }
}
