using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Brand
{
    public class CreateBrandCommand : IRequest<Application.Queries.BrandViewModel>
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 分组关键字，比如：摄像机、路由器、交换机等
        /// </summary>
        public string GroupKey { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string TentantId { get; set; }
    }
}
