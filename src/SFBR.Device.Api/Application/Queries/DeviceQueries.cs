using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Primitives;
using SFBR.Device.Infrastructure;

namespace SFBR.Device.Api.Application.Queries
{
    /// <summary>
    /// 查询
    /// </summary>
    public class DeviceQueries: IDeviceQueries
    {
        private readonly IDbConnection _connection;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public DeviceQueries(IDbConnection connection)
        {
            _connection = connection?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State == ConnectionState.Closed) _connection.Open();
        }

        public async Task<bool> Exists(string equipNum)
        {
            return await _connection.ExecuteScalarAsync<bool>("select count(*) from Devices where Id=@equipNum or EquipNum=@equipNum", new { equipNum });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeviceModel> GetDeviceAsync(string id)
        {
            var device = await _connection.QueryFirstOrDefaultAsync<TerminalDevice>("select * from Devices where Id=@id or EquipNum=@id", new { id });
            if(device != null)
            {
                var channels = await _connection.QueryAsync<Channel>("select * from v_Channels where DeviceId=@Id", new { device.Id });
                if (channels != null) device.Channels = channels.ToList();
                var parts = await _connection.QueryAsync<Part>("select * from v_Parts where DeviceId=@Id", new { device.Id });
                if (parts != null) device.Parts = parts.ToList();
                var controllers = await _connection.QueryAsync<Controller>("select * from v_Controllers where DeviceId=@Id", new { device.Id });
                if (controllers != null) device.Controllers = controllers.ToList();
                var sensors = await _connection.QueryAsync<Sensor>("select * from v_Sensors where DeviceId=@Id", new { device.Id });
                if (sensors != null) device.Sensors = sensors.ToList();
                var functions = await _connection.QueryAsync<DeviceFunction>("select * from v_DeviceFunctions where DeviceId=@Id", new { device.Id });
                if (functions != null) device.DeviceFunctions = functions.ToList();
                var alarms = await _connection.QueryAsync<DeviceAlarm>("select * from v_DeviceAlarms where DeviceId=@Id", new { device.Id });
                if (alarms != null) device.DeviceAlarms = alarms.ToList();
                var loads = await _connection.QueryAsync<Camera>("select * from v_camera where ParentId=@Id", new { device.Id });
                if (loads != null)
                {
                    device.Loads = loads.Cast<Load>().ToList();
                    var props = await  _connection.QueryAsync<DeviceProp>("select * from DeviceProp p where exists(select 1 from v_camera c where c.Id = p.CameraId and c.ParentId =@Id)", new { device.Id });
                    foreach (var load in device.Loads)
                    {
                        var camera = load as Camera;
                        camera.DeviceProps = props.Where(t => t.DeviceId == camera.Id).ToList();
                    }
                }
                device.Location = await _connection.QueryFirstOrDefaultAsync<Location>("select * from Locations where Id=@LocationId", new { device.LocationId });
                device.Region = await _connection.QueryFirstOrDefaultAsync<Region>("select * from Regions where Id=@RegionId", new { device.RegionId });
                
            }
            return device;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeviceModel> GetSimpleDeviceAsync(string id)
        {
            var device = await _connection.QueryFirstOrDefaultAsync<TerminalDevice>("select * from Devices where Id=@id or EquipNum=@id", new { id });
            return device;
        }

        public async Task<DeviceFunction> GetFunctionAsync(string id,string functionCode)
        {
            return await _connection.QuerySingleOrDefaultAsync<DeviceFunction>("select * from v_DeviceFunctions f where exists(select 1 from v_terminal d where d.Id = f.DeviceId and (d.Id = @id or d.EquipNum=@id)) and FunctionCode=@functionCode", new { id, functionCode });
        }

        public async Task<Channel> GetChannelAsync(string id, int portNumber)
        {
            return await _connection.QuerySingleOrDefaultAsync<Channel>("select * from v_Channels where DeviceId=@Id and PortNumber=@portNumber", new { id, portNumber });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DeviceType> GetDeviceTypeAsync(string code, string model)
        {
            var deviceType = await _connection.QueryFirstOrDefaultAsync<DeviceType>("select * from DeviceTypes where code=@code and model=@model", new { code, model });
            if(deviceType != null)
            {
                var channels = await _connection.QueryAsync<DeviceTypeChannel>("select * from DeviceTypeChannels where DeviceTypeId=@Id", new { deviceType.Id });
                if (channels != null) deviceType.Channels = channels.ToList();
                var parts = await _connection.QueryAsync<DeviceTypePart>("select * from DeviceTypeParts where DeviceTypeId=@Id", new { deviceType.Id });
                if (parts != null) deviceType.Parts = parts.ToList();
                var controllers = await _connection.QueryAsync<DeviceTypeController>("select * from DeviceTypeControllers where DeviceTypeId=@Id", new { deviceType.Id });
                if (controllers != null) deviceType.Controllers = controllers.ToList();
                var sensors = await _connection.QueryAsync<DeviceTypeSensor>("select * from DeviceTypeSensors where DeviceTypeId=@Id", new { deviceType.Id });
                if (sensors != null) deviceType.Sensors = sensors.ToList();
                var functions = await _connection.QueryAsync<DeviceTypeFunction>("select * from DeviceTypeFunctions where DeviceTypeId=@Id", new { deviceType.Id });
                if (functions != null) deviceType.Functions = functions.ToList();
                var alarms = await _connection.QueryAsync<DeviceTypeAlarm>("select * from DeviceTypeAlarms where DeviceTypeId=@Id", new { deviceType.Id });
                if (alarms != null) deviceType.Alarms = alarms.ToList();
            }
            return deviceType;
        }

        public async Task<PageResult<TerminalDevice>> GetTerminalPageAsync(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows)
        {
            string sqltext = "select * from v_terminal where 1=1 ";
            var condition = query.GetWhereToParString();
            sqltext += string.IsNullOrEmpty(condition.Item1) ? "" : $" and {condition.Item1}";
            return await _connection.PageingAsync<TerminalDevice>(sqltext, page, rows, param: condition.Item2);
        }

        public async Task<PageResult<Camera>> GetCameraPageAsync(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows)
        {
            string sqltext = "select * from v_camera where 1=1 ";
            var condition = query.GetWhereToParString();
            sqltext += string.IsNullOrEmpty(condition.Item1) ? "" : $" and {condition.Item1}";
            return await _connection.PageingAsync<Camera>(sqltext, page, rows, param: condition.Item2);
        }

        #region 垃圾回收（回收链接）

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        ~DeviceQueries()
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
