
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFBR.Device.Domain.AggregatesModel.BrandAggregate
{
    public interface IBrandRepository : SeedWork.IRepository<Brand>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="brand"></param>
        void Add(Brand brand);
        /// <summary>
        /// 全部更新
        /// </summary>
        /// <param name="brand"></param>
        void Update(Brand brand);
        /// <summary>
        /// 只更新修改部分
        /// </summary>
        /// <param name="brand"></param>
        void Patch(Brand brand);
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="brand"></param>
        void Delete(Brand brand);
        /// <summary>
        /// 根据主键获取设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Brand> GetAsync(string id);
    }
}
