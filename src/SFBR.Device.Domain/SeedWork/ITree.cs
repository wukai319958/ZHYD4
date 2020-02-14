using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.SeedWork
{
    public interface ITree<T>
    {
        T ParentId { get; set; }
    }
}
