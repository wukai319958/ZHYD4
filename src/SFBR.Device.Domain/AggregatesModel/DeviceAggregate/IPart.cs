using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    public interface IPart
    {
        int PortNumber { get; set; }
    }
}
