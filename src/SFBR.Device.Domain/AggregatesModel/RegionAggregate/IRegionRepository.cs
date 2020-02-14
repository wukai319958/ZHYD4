using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFBR.Device.Domain.AggregatesModel.RegionAggregate
{
    public interface IRegionRepository : SeedWork.IRepository<Region>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="region"></param>
        void Add(Region region);
        /// <summary>
        /// 全部更新
        /// </summary>
        /// <param name="region"></param>
        void Update(Region region);
        /// <summary>
        /// 只更新修改部分
        /// </summary>
        /// <param name="region"></param>
        void Patch(Region region);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="region"></param>
        void Delete(Region region);
        /// <summary>
        /// 根据主键获取设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Region> GetAsync(string id);
    }
}
