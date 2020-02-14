using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 路由器
    /// </summary>
    public class Router : Device
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Device Parent { get; set; }

        public void SetParentId(string parentId)
        {
            ParentId = parentId;
        }

    }
}
