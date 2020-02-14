using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    public class DeviceTypeQueries : IDeviceTypeQueries
    {
        private readonly string _connectionString;

        public DeviceTypeQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public DeviceType GetDeviceType(string id)
        {
            throw new NotImplementedException();
        }

        public DeviceType GetDeviceType(string code, string model)
        {
            throw new NotImplementedException();
        }
    }
}
