using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class RegionModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionCode { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; private set; }
        /// <summary>
        /// 上级区域
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 租户
        /// </summary>
        public string TentantId { get; protected set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get; private set; }


    }
    /// <summary>
    /// 树形结构数据
    /// </summary>
    public class RegionTreeModel : RegionModel
    {
        /// <summary>
        /// 子区域
        /// </summary>
        public List<RegionTreeModel> Children { get; set; }

        //public RegionModel Parent { get; set; }
    }



}
