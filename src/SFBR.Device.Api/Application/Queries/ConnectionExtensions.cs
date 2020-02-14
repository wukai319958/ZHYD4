using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SFBR.Device.Api.Application.Queries
{
    internal static class ConnectionExtensions
    {

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sqltext">查询表达式</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示行数</param>
        /// <param name="pagingSort">分页时的排序方式（该参数不可以前台传入）</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async static Task<PageResult<T>> PageingAsync<T>(this IDbConnection db, string sqltext, int page = 1, int rows = 10, string pagingSort = "order by id", object param = null) where T:class
        {
            page = (page <= 0) ? 1 : page;
            rows = (rows <= 0) ? 20 : rows;
            long count = await db.ExecuteScalarAsync<long>(buildCountString(sqltext), param);
            if (count <= 0) return new PageResult<T>(new List<T>(), 0, 0, rows);
            //计算总页数
            int page_count = (int)Math.Ceiling(count / (double)rows);
            //当页码超过总页数时取最后一页
            if (page > page_count) page = page_count;
            
            var list =await db.QueryAsync<T>(buildPageString(sqltext, page, rows, pagingSort), param) ?? new List<T>();
            return new PageResult<T>(list, count, page_count, rows);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sqltext">查询表达式</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示行数</param>
        /// <param name="pagingSort">分页时的排序方式（该参数不可以前台传入）</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static PageResult<T> Paging<T>(this IDbConnection db, string sqltext, int page = 1, int rows = 10, string pagingSort = "order by id", object param = null) where T : class
        {
            return db.PageingAsync<T>(sqltext, page, rows, pagingSort, param).GetAwaiter().GetResult();
        }


        private static string buildCountString(string sqltext)
        {
            return $"select count(1) total from ({sqltext}) as sfbrcount ";
        }

        private static string buildPageString(string sqltext,int page,int rows,string pagingSort)
        {
            return $"select * from ( select Row_Number() over({pagingSort}) sfbrpagenum, sfbrpaging.* from ({sqltext}) as sfbrpaging) sfbrpaging2 where sfbrpaging2.sfbrpagenum between {(page - 1) * rows} and {page * rows}";
        }
    }
}
