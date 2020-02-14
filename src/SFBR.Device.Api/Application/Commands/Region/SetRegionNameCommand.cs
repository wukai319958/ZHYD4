using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Region
{
    public class SetRegionNameCommand : IRequest<bool>
    {
        public SetRegionNameCommand(string id, string regionName)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            RegionName = regionName ?? throw new ArgumentNullException(nameof(regionName));
        }

        /// <summary>
        /// 区域Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }




    }
}
