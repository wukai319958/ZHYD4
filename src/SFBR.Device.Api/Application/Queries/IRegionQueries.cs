using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    public interface IRegionQueries : IDisposable
    {
        /// <summary>
        /// 获取区域集合
        /// </summary>
        /// <returns></returns>
        Task<List<RegionModel>> GetRegionList();
        /// <summary>
        ///  获取该区域下的下一级子区域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<RegionModel>> GetChildren(string id);
        /// <summary>
        /// 获取父区域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RegionModel> GetParent(string id);
        /// <summary>
        /// 获得该区域下的所有子区域（递归）
        /// </summary>
        /// <returns></returns>
        Task<List<RegionTreeModel>> GetTree();

        Task<List<TerminalDevice>> GetTerminals(string id);


    }
}
