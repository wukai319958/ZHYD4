using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFBR.Log.Api.Infrastructure;
using SFBR.Log.Api.ViewModel;
using AutoMapper;
using Microsoft.Extensions.Primitives;
using System.Data.SqlClient;
using SFBR.Log.Api.Queries;

namespace SFBR.Log.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    // [Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    [ApiController]
    public class AlarmController : ControllerBase
    {
        private readonly LogContext _db;
        private readonly IMapper _mapper;
        private readonly IAlarmQueries _alarmQueries;
        public AlarmController(LogContext db, IAlarmQueries alarmQueries)
        {
            _alarmQueries = alarmQueries;
            _db = db;
        }

        /// <summary>
        /// 按警报状态统计警报数量
        /// </summary>
        /// <returns></returns>
        [Route("disposeStatus")]
        [HttpGet]
        [ProducesResponseType(typeof(AlarmClearCount), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAlarmCountByDisposeStatus()
        {
            var result = await _alarmQueries.GetAlarmCountByDisposeStatus();
            return Ok(result);
        }
        /// <summary>
        /// 按警报级别统计
        /// </summary>
        /// <returns></returns>
        [Route("level")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlarmLevelCount>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAlarmCountByLevel()
        {
            var result = await _alarmQueries.GetAlarmCountByLevel();
            return Ok(result);
        }

        /// <summary>
        /// 分页获取警报消息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<AlarmMdoel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAlarms(int page, int rows)
        {
            IEnumerable<KeyValuePair<string, StringValues>> query = Request.Query;
            var result = await _alarmQueries.GetAlarms(query, page, rows);
            return Ok(result);
        }


        /// <summary>
        /// 获得警报处理时长Top5
        /// </summary>
        /// <returns></returns>
        [Route("topTime")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlarmDealTime>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAlarmTopTime()
        {
            var result = await _alarmQueries.GetAlarmTopTime();
            return Ok(result);
        }
    }
}