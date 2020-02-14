using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SFBR.Device.Api.Application.Queries
{

    public class BrandQueries : IBrandQueries, IDisposable
    {
        private readonly IDbConnection _connection;

        public BrandQueries(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State == ConnectionState.Closed) _connection.Open();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<BrandViewModel>> GetBrandList()
        {
            string sqltext = "SELECT * FROM Brand";
            var brandList = await _connection.QueryAsync<BrandViewModel>(sqltext);
            return brandList.ToList();
        }



        #region 垃圾回收（回收链接）

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        ~BrandQueries()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                // 清理托管资源
                _connection.Dispose();
            }
            //让类型知道自己已经被释放
            disposed = true;
        }
        #endregion
    }
}
