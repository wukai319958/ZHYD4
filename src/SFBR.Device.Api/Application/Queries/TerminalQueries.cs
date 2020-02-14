using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Primitives;
using SFBR.Device.Infrastructure;

namespace SFBR.Device.Api.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class TerminalQueries : ITerminalQueries, IDisposable
    {

        private readonly IDbConnection _connection;

        public TerminalQueries(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State == ConnectionState.Closed) _connection.Open();
        }




        #region 统计
        /// <summary>
        /// 按状态统计站点
        /// </summary>
        /// <returns></returns>
        public async Task<TerminalCountResult> GetTerminalCountByStatus()
        {
            string sqltext = @"SELECT 
                                  SUM(CASE WHEN d.Connection=0 THEN 1 ELSE 0 END)AS [OffLine],
                                  SUM(CASE WHEN d.Connection=1 AND d.AlarmCount>0 THEN 1 ELSE 0 END)AS [Alarm] ,
                                  SUM(CASE WHEN d.Connection=1 AND d.AlarmCount=0 THEN 1 ELSE 0 END)AS [Normal]
                               FROM 
                                  v_terminal as d ";
            var result = await _connection.QueryFirstAsync<TerminalCountResult>(sqltext);
            return result;
        }

        /// <summary>
        /// 按状态统计照相机
        /// </summary>
        /// <returns></returns>
        public async Task<CameraCountResult> GetCameraCountByStatus()
        {
            string sqltext = @"SELECT 
                                  SUM(CASE WHEN c.Connection=0 THEN 1 ELSE 0 END)[OffLine],
                                  SUM(CASE WHEN c.Connection=1 THEN 1 ELSE 0 END)[OnLine]
                               FROM 
                                  v_camera AS c";
            var result = await _connection.QueryFirstAsync<CameraCountResult>(sqltext);
            return result;
        }
        #endregion

        #region 其它

        /// <summary>
        /// 分页获取摄像头列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public async Task<PageResult<Camera>> GetCameras(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows)
        {
            string sqltext = $"SELECT * FROM v_camera WHERE 1=1 ";
            var condition = query.GetWhereToParString();
            sqltext += string.IsNullOrEmpty(condition.Item1) ? "" : $"and {condition.Item1}";
            var result = await _connection.PageingAsync<Camera>(sqltext, page, rows, param: condition.Item2);
            return result;
        }

        /// <summary>
        /// 根据id获取摄像头集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Camera>> GetCameras(string id)
        {
            string sqltext = "SELECT * FROM v_camera WHERE exists(select 1 from v_terminal t where t.Id = c.ParentId and (t.Id = @id or t.EquipNum = @id))";
            var result = await _connection.QueryAsync<Camera>(sqltext, new { id });
            return result;
        }

        /// <summary>
        /// 根据设备id获取配件开关集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns
        public async Task<IEnumerable<Controller>> GetControllers(string id)
        {
            string sqltext = "SELECT * FROM v_Controllers WHERE DeviceId=@id";
            var result = await _connection.QueryAsync<Controller>(sqltext, new { id });
            return result;
        }

        /// <summary>
        /// 根据设备上的id获取负载集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Load>> GetLoads(string id)
        {
            string sqltext = "SELECT * FROM v_Devices WHERE ParentId=@id";
            var result = await _connection.QueryAsync<Load>(sqltext, new { id });
            return result;
        }


        /// <summary>
        /// 根据id获取配件集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Part>> GetParts(string id)
        {
            string sqltext = "SELECT * FROM v_Parts WHERE DeviceId=@id";
            var result = await _connection.QueryAsync<Part>(sqltext, new { id });
            return result;
        }
        /// <summary>
        /// 分页获取站点
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PageResult<TerminalDevice>> GetTerminalPageAsync(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows)
        {
            string sqltext = "SELECT * FROM v_terminal WHERE 1=1 ";
            var condition = query.GetWhereToParString();
            sqltext += string.IsNullOrEmpty(condition.Item1) ? "" : $" and {condition.Item1}";
            var result = await _connection.PageingAsync<TerminalDevice>(sqltext, page, rows, param: condition.Item2);
            foreach (var device in result.Rows)
            {
                var channelSql = "SELECT * FROM v_Channels WHERE DeviceId=@Id";
                var channes = await _connection.QueryAsync<Channel>(channelSql, new { Id = device.Id });
                if (channes != null) device.Channels = channes.ToList();
                var partSql = "SELECT* FROM v_Parts WHERE DeviceId = @Id";
                var parts = await _connection.QueryAsync<Part>(partSql, new { Id = device.Id });
                if (parts != null) device.Parts = parts.ToList();
                var loadSql = "SELECT * FROM v_Devices WHERE  ParentId=@Id";
                var loads = await _connection.QueryAsync<Load>(loadSql, new { Id = device.Id });
                if (loads != null) device.Loads = loads.ToList();
                var sensorsSql = "SELECT* FROM v_Sensors WHERE DeviceId = @Id";
                var sensors = await _connection.QueryAsync<Sensor>(sensorsSql, new { Id = device.Id });
                if (sensors != null) device.Sensors = sensors.ToList();
                var controllerSql = "SELECT* FROM v_Controllers WHERE DeviceId = @Id";
                var controllers = await _connection.QueryAsync<Controller>(controllerSql, new { Id = device.Id });
                if (controllers != null) device.Controllers = controllers.ToList();
                var deviceFunctionSql = "SELECT * FROM DeviceFunctions WHERE DeviceId=@Id";
                var functions = await _connection.QueryAsync<DeviceFunction>(deviceFunctionSql, new { Id = device.Id });
                if (functions != null) device.DeviceFunctions = functions.ToList();

                var timeTaskSql = "SELECT * FROM TimedTasks WHERE DeviceId=@Id";
                var tasks = await _connection.QueryAsync<TimedTask>(timeTaskSql, new { Id = device.Id });
                if (tasks != null) device.TimedTasks = tasks.ToList();

                var deviceAlarmSql = "SELECT * FROM DeviceAlarms WHERE DeviceId=@Id";
                var alarms = await _connection.QueryAsync<DeviceAlarm>(deviceAlarmSql, new { Id = device.Id });
                if (alarms != null) device.DeviceAlarms = alarms.ToList();

                if (device.LocationId != null)
                {
                    var locationSql = "SELECT * FROM Locations WHERE Id=@Id";
                    var location = await _connection.QueryFirstAsync<Location>(locationSql, new { Id = device.LocationId ?? " " });
                    if (location != null) device.Location = location;
                }

                if (device.RegionId != null)
                {
                    var regionSql = "SELECT * FROM Regions WHERE Id=@Id";
                    var region = await _connection.QueryFirstAsync<Region>(regionSql, new { Id = device.RegionId ?? " " });
                    if (region != null) device.Region = region;
                }

            }
            return result;
        }

        /// <summary>
        /// 获取未定位的站点
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TerminalDevice>> GetTerminalsWhereUnsetLocation()
        {
            string sqltext = "SELECT * FROM v_terminal WHERE IsPosition=0";
            var result = await _connection.QueryAsync<TerminalDevice>(sqltext);
            foreach (var device in result)
            {
                var channelSql = "SELECT * FROM v_Channels WHERE DeviceId=@Id";
                var channes = await _connection.QueryAsync<Channel>(channelSql, new { Id = device.Id });
                if (channes != null) device.Channels = channes.ToList();
                var partSql = "SELECT* FROM v_Parts WHERE DeviceId = @Id";
                var parts = await _connection.QueryAsync<Part>(partSql, new { Id = device.Id });
                if (parts != null) device.Parts = parts.ToList();
                var loadSql = "SELECT * FROM v_Devices WHERE  ParentId=@Id";
                var loads = await _connection.QueryAsync<Load>(loadSql, new { Id = device.Id });
                if (loads != null) device.Loads = loads.ToList();
                var sensorsSql = "SELECT* FROM v_Sensors WHERE DeviceId = @Id";
                var sensors = await _connection.QueryAsync<Sensor>(sensorsSql, new { Id = device.Id });
                if (sensors != null) device.Sensors = sensors.ToList();
                var controllerSql = "SELECT* FROM v_Controllers WHERE DeviceId = @Id";
                var controllers = await _connection.QueryAsync<Controller>(controllerSql, new { Id = device.Id });
                if (controllers != null) device.Controllers = controllers.ToList();
                var deviceFunctionSql = "SELECT * FROM DeviceFunctions WHERE DeviceId=@Id";
                var functions = await _connection.QueryAsync<DeviceFunction>(deviceFunctionSql, new { Id = device.Id });
                if (functions != null) device.DeviceFunctions = functions.ToList();

                var timeTaskSql = "SELECT * FROM TimedTasks WHERE DeviceId=@Id";
                var tasks = await _connection.QueryAsync<TimedTask>(timeTaskSql, new { Id = device.Id });
                if (tasks != null) device.TimedTasks = tasks.ToList();

                var deviceAlarmSql = "SELECT * FROM DeviceAlarms WHERE DeviceId=@Id";
                var alarms = await _connection.QueryAsync<DeviceAlarm>(deviceAlarmSql, new { Id = device.Id });
                if (alarms != null) device.DeviceAlarms = alarms.ToList();

                if (device.LocationId != null)
                {
                    var locationSql = "SELECT * FROM Locations WHERE Id=@Id";
                    var location = await _connection.QueryFirstAsync<Location>(locationSql, new { Id = device.LocationId ?? " " });
                    if (location != null) device.Location = location;
                }

                if (device.RegionId != null)
                {
                    var regionSql = "SELECT * FROM Regions WHERE Id=@Id";
                    var region = await _connection.QueryFirstAsync<Region>(regionSql, new { Id = device.RegionId ?? " " });
                    if (region != null) device.Region = region;
                }
            }
            return result;
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

        ~TerminalQueries()
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
