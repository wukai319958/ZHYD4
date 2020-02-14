using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFBR.Data.Api.Infrastructure;
using SFBR.Data.Api.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace SFBR.Data.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    public class DataController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        public DataController(DataContext db, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _db = db ?? throw new ArgumentNullException(nameof(db));

        }


        /// <summary>
        /// 获取最新的实时数据（模拟量和状态量）
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DeviceActor), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            if (TerminalConsumer.RealDataCaches.TryGetValue(id, out DeviceActor deviceActor))
            {
                return Ok(await Task.FromResult(deviceActor));
            }
            try
            {
                string sqltext = @"SELECT EquipNum
                                  ,AlarmStatus
                                  ,[Current]
                                  ,Humidity
                                  ,SwitchStatus
                                  ,Temperature
                                  ,Voltage
                                  ,(SELECT MAX(LastCreationTime)
                                    FROM (VALUES ([A_CreationTime]),([C_CretionTime]),([H_CreationTime]),([S_CreationTime]),([T_CreationTime]),([V_CreationTime])) AS CreationTime(LastCreationTime)) AS LastUpdate
                                  FROM
                                  (SELECT A.EquipNum
                                  ,A.Id
                                  ,A.Value AS [AlarmStatus] 
                                  ,A.CreationTime AS [A_CreationTime]
                                  ,C.Value AS [Current]
                                  ,c.CreationTime AS [C_CretionTime]
                                  ,H.Value AS [Humidity]
                                  ,H.CreationTime AS [H_CreationTime]
                                  ,S.Value AS [SwitchStatus]
                                  ,S.CreationTime AS [S_CreationTime]
                                  ,T.Value AS [Temperature]
                                  ,T.CreationTime AS [T_CreationTime]
                                  ,V.Value AS [Voltage]
                                  ,V.CreationTime AS [V_CreationTime]
                                  FROM 
                                  dbo.AlarmStatuses AS A JOIN dbo.Currents AS C ON A.Id=C.Id 
                                  JOIN dbo.Humidities AS H ON H.Id=A.Id
                                  JOIN dbo.SwitchStatuses AS S ON S.Id=A.Id
                                  JOIN dbo.Temperatures AS T ON T.Id=A.Id
                                  JOIN dbo.Voltages AS V ON V.Id=A.Id) AS TempTable
                                  WHERE TempTable.Id=@Id";
                var result = _db.Database.SqlQuery<DeviceActor>(sqltext, new[] { new SqlParameter("Id", id) });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// 电压历史数据查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        [HttpGet("{id}/voltage")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<DataModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetVoltage(string id, [FromQuery]DateTime startTime, [FromQuery]DateTime endTime)
        {
            try
            {
                var result = _db.Voltages.Where(w => w.Id == id && w.CreationTime >= startTime && w.CreationTime <= endTime)
                                        .Select(s => new DataModel()
                                        {
                                            Position = s.Position,
                                            Value = s.Value,
                                            CreationTime = s.CreationTime
                                        }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }
        /// <summary>
        /// 电流历史数据查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet("{id}/current")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<DataModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrent(string id, [FromQuery]DateTime startTime, [FromQuery]DateTime endTime)
        {
            try
            {
                var result = _db.Currents.Where(w => w.Id == id && w.CreationTime >= startTime && w.CreationTime <= endTime)
                                        .Select(s => new DataModel()
                                        {
                                            Position = s.Position,
                                            Value = s.Value,
                                            CreationTime = s.CreationTime
                                        }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        /// <summary>
        /// 温度历史数据查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet("{id}/temperature")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<DataModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTemperature(string id, [FromQuery]DateTime startTime, [FromQuery]DateTime endTime)
        {
            try
            {
                var result = _db.Temperatures.Where(w => w.Id == id && w.CreationTime >= startTime && w.CreationTime <= endTime)
                                        .Select(s => new DataModel()
                                        {
                                            Position = s.Position,
                                            Value = s.Value,
                                            CreationTime = s.CreationTime
                                        }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        /// <summary>
        /// 湿度历史数据查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet("{id}/humidity")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<DataModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetHumidity(string id, [FromQuery]DateTime startTime, [FromQuery]DateTime endTime)
        {
            try
            {
                var result = _db.Humidities.Where(w => w.Id == id && w.CreationTime >= startTime && w.CreationTime <= endTime)
                                        .Select(s => new DataModel()
                                        {
                                            Position = s.Position,
                                            Value = s.Value,
                                            CreationTime = s.CreationTime
                                        }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// 警报状态历史数据查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet("{id}/alarmstatus")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<StatusModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAlarmStatus(string id, [FromQuery]DateTime startTime, [FromQuery]DateTime endTime)
        {
            try
            {
                var result = _db.AlarmStatuses.Where(w => w.Id == id && w.CreationTime >= startTime && w.CreationTime <= endTime)
                                        .Select(s => new StatusModel()
                                        {
                                            Value = s.Value,
                                            CreationTime = s.CreationTime
                                        }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        /// <summary>
        /// 开关量历史数据查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet("{id}/switchstatus")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<StatusModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSwitchStatus(string id, [FromQuery]DateTime startTime, [FromQuery]DateTime endTime)
        {
            try
            {
                var result = _db.SwitchStatuses.Where(w => w.Id == id && w.CreationTime >= startTime && w.CreationTime <= endTime)
                                        .Select(s => new StatusModel()
                                        {
                                            Value = s.Value,
                                            CreationTime = s.CreationTime
                                        }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
