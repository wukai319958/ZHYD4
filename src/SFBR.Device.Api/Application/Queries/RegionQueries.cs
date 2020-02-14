using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;

namespace SFBR.Device.Api.Application.Queries
{
    public class RegionQueries : IRegionQueries, IDisposable
    {
        private readonly IDbConnection _connection;

        public RegionQueries(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State == ConnectionState.Closed) _connection.Open();

        }
        public async Task<List<RegionModel>> GetChildren(string id)
        {
            string sqltext = "SELECT * FROM Regions WHERE ParentId=@id";
            var children = await _connection.QueryAsync<RegionModel>(sqltext, new { id });
            return children.ToList();
        }


        public async Task<RegionModel> GetParent(string id)
        {
            string sqltext = "SELECT * FROM Regions WHERE Id=@id";
            var parent = await _connection.QueryFirstAsync<RegionModel>(sqltext, new { id });
            return parent;
        }

        public async Task<List<RegionModel>> GetRegionList()
        {
            string sqltext = "SELECT * FROM Regions";
            var regions = await _connection.QueryAsync<RegionModel>(sqltext);
            return regions.ToList();
        }

        public async Task<List<TerminalDevice>> GetTerminals(string id)
        {
            string sqltext = "SELECT * FROM Regions Devices WHERE RegionId=@Id";
            var result = await _connection.QueryAsync<TerminalDevice>(sqltext,new { Id=id});
            return result.ToList();
        }

        #region GetTree
        public async Task<List<RegionTreeModel>> GetTree()
        {
            List<RegionTreeModel> result = GetRoots();
            foreach (var item in result)
            {
                CreateTheTree(item.Id, item);
            }
            return await Task.FromResult(result);
        }

        public void CreateTheTree(string parentId, RegionTreeModel model)
        {
            string sqltext = "SELECT * FROM Regions WHERE ParentId=@parentId";
            var childern = _connection.Query<RegionTreeModel>(sqltext, new { parentId = parentId }).ToList();
            if (childern.Count == 0) return;
            foreach (var item in childern)
            {
                model.Children = childern;
                CreateTheTree(item.Id, item);
            }
        }

        public List<RegionTreeModel> GetRoots()
        {
            string sqltext = "SELECT * FROM Regions WHERE ParentId IS NULL OR ParentId=' '";
            var roots = _connection.Query<RegionTreeModel>(sqltext).ToList();
            return roots;
        }
        #endregion


        #region 垃圾回收（回收链接）

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        ~RegionQueries()
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
