using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// UPS
    /// （该UPS为外接设备，与设备内部的UPS完全不一样，设备内部仅仅是个大电容
    /// 该UPS的作用是给智维终端以及整个站点供电，需要对接SNMP协议，拓扑图上只需在市电输入和电源防雷器之间增加一个UPS图标）
    /// </summary>
    public class UPS:Device
    {
        public virtual Device Parent { get; set; }
        public void SetParentId(string parentId)
        {
            ParentId = parentId;
        }
    }
}
