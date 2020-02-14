using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SFBR.Log.Api.Infrastructure;
using SFBR.Log.Api.ViewModel;

namespace SFBR.Log.Api.Queries
{
    public class ActionQueries : IActionQueries, IDisposable
    {
        private readonly IDbConnection _connection;
        public ActionQueries(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State == ConnectionState.Closed) _connection.Open();
        }
        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public async Task<PageResult<ActionLogModel>> GetActionLog(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows)
        {
            try
            {
                string sqltext = @"SELECT 
        [Id],
       [Account]
      ,[Name]
      ,[DeviceName]
      ,[FunctionName]
      ,[ActionDesciption]
      ,[CreationTime]
      ,[ApplicationContext]
  FROM ActionLogs WHERE 1=1 ";
                var condition = query.GetWhereToParString();
                sqltext += string.IsNullOrEmpty(condition.Item1) ? "" : $"and {condition.Item1}";
                var logs = await _connection.PageingAsync<ActionLogModel>(sqltext, page, rows, param: condition.Item2);
                return logs;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        #region
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        ~ActionQueries()
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
