using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 网络交换机
    /// </summary>
    public class NetSwitch : Device
    {

        public virtual Device Parent { get; set; }
        public void SetParentId(string parentId)
        {
            ParentId = parentId;
        }
    }
}
