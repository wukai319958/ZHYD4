using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    public interface IParameter
    {
        List<Parameter> Parameters { get; }
    }
}
