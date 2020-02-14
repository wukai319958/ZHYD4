using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Device
{
    /// <summary>
    /// 
    /// </summary>
    public class SetDeviceNameCommand:IRequest<bool>
    {
        public SetDeviceNameCommand(string id, string name)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// 设备id
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
    }
}
