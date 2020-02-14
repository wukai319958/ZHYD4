using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 按钮
    /// </summary>
    public class Button
    {
        /// <summary>
        /// 
        /// </summary>
        public string BtnName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
    }
}
