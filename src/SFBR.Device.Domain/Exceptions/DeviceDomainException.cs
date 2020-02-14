using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.Exceptions
{
    public class DeviceDomainException:DomainException
    {
        public DeviceDomainException()
        { }

        public DeviceDomainException(string message)
            : base(message)
        { }

        public DeviceDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
