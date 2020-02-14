using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    public interface ITerminalQueries
    {
        #region 统计
        /// <summary>
        /// 按状态统计站点
        /// </summary>
        /// <returns></returns>
        Task<TerminalCountResult> GetTerminalCountByStatus();

        /// <summary>
        /// 按状态统计照相机
        /// </summary>
        /// <returns></returns>
        Task<CameraCountResult> GetCameraCountByStatus();
        #endregion

        #region 其它
        /// <summary>
        /// 分页获取站点
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        Task<PageResult<TerminalDevice>> GetTerminalPageAsync(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows);


        /// <summary>
        /// 获取未定位的站点
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TerminalDevice>> GetTerminalsWhereUnsetLocation();

        /// <summary>
        /// 分页获取摄像头列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        Task<PageResult<Camera>> GetCameras(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows);


        /// <summary>
        /// 根据设备上的id获取负载集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Load>> GetLoads(string id);

        /// <summary>
        /// 根据id获取摄像头集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Camera>> GetCameras(string id);

        /// <summary>
        /// 根据id获取配件集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Part>> GetParts(string id);

        /// <summary>
        /// 根据设备id获取配件开关集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Controller>> GetControllers(string id);

        #endregion
    }
}
