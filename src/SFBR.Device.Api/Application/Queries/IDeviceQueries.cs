using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    public interface IDeviceQueries:IDisposable 
    {
        /// <summary>
        /// 判断设备是否存在
        /// </summary>
        /// <param name="equipNum"></param>
        /// <returns></returns>
        Task<bool> Exists(string equipNum);
        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DeviceModel> GetDeviceAsync(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DeviceModel> GetSimpleDeviceAsync(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        Task<DeviceFunction> GetFunctionAsync(string id, string functionCode);
        /// <summary>
        /// 获取回路信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber"></param>
        /// <returns></returns>
        Task<Channel> GetChannelAsync(string id, int portNumber);
        /// <summary>
        /// 根据设备编号和型号获取类型轻轻
        /// </summary>
        /// <param name="code"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DeviceType> GetDeviceTypeAsync(string code, string model);

        /// <summary>
        /// 智维终端分页查询
        /// </summary>
        /// <param name="query">动态查询条件</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页条数</param>
        /// <returns></returns>
        Task<PageResult<TerminalDevice>> GetTerminalPageAsync(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows);

        /// <summary>
        /// 分页获取摄像机列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        Task<PageResult<Camera>> GetCameraPageAsync(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows);
    }
}
