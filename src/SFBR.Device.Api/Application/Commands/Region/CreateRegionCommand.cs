using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Region
{
    public class CreateRegionCommand : IRequest<Application.Queries.RegionModel>
    {
        /// <summary>
        /// 父级区域Id
        /// </summary>
        public string ParentId { get; set; }



        /// <summary>
        /// 区域描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 区域码
        /// </summary>
        public string RegionCode { get; set; }
        /// <summary>
        ///  租户Id
        /// </summary>
        public string TenTantId { get; set; }

    }
}
