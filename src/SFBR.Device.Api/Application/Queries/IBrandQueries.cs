using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    public interface IBrandQueries : IDisposable
    {
        /// <summary>
        /// 获取品牌集合
        /// </summary>
        /// <returns></returns>
        Task<List<BrandViewModel>> GetBrandList();


    }
}
