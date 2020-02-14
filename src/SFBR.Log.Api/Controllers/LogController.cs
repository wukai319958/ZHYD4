using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Primitives;
using SFBR.Log.Api.Queries;

namespace SFBR.Log.Api.Controllers
{


    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {

        private readonly IActionQueries _actionLogQueries;
        public LogController(IActionQueries actionLogQueries)
        {
            _actionLogQueries = actionLogQueries;
        }
        /// <summary>
        /// 获取对应设备操作记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet("actionlog")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActionPages([FromQuery]int page, [FromQuery]int rows)
        {
            IEnumerable<KeyValuePair<string, StringValues>> query = Request.Query;
            var result = await _actionLogQueries.GetActionLog(query, page, rows);
            return Ok(result);
        }
    }

}