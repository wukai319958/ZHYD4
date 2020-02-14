using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    public class BrandViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 分组关键字，比如：摄像机、路由器、交换机等
        /// </summary>
        public string GroupKey { get; set; }
    }
}
