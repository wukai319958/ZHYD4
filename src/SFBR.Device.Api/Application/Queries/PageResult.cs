using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T> where T : class
    {
        public PageResult()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows">数据集合</param>
        /// <param name="total">总条数</param>
        /// <param name="pages">总页数</param>
        public PageResult(IEnumerable<T> rows,long total,int pages,int pageSize = 10)
        {
            Total = total;
            Pages = pages;
            Rows = rows;
            PageSize = pageSize;
        }

        /// <summary>
        /// 数据集合
        /// </summary>
        public IEnumerable<T> Rows { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Pages { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
