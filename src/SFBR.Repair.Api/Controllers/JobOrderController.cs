using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFBR.Repair.Api.ViewModel;

namespace SFBR.Repair.Api.Controllers
{
    /// <summary>
    /// 任务单维护服务
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    [ApiController]
    public class JobOrderController : ControllerBase
    {

        /// <summary>
        /// 分页获取任务单
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<JobOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery]int page, [FromQuery]int rows)
        {

            return await Task.FromResult((IActionResult)null);
        }

        /// <summary>
        /// 获取任务单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(JobOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {

            return await Task.FromResult((IActionResult)null);
        }

        /// <summary>
        /// 关闭任务单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Finish(string id)
        {

            return await Task.FromResult((IActionResult)null);
        }
        /// <summary>
        /// 创建任务单
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(JobOrder), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] JobOrder jobOrder)
        {

            return await Task.FromResult((IActionResult)null);
        }
        /// <summary>
        /// 删除任务单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {

            return await Task.FromResult((IActionResult)null);
        }
        /// <summary>
        /// 添加审核记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditMessage"></param>
        /// <returns></returns>
        [Route("{id}/audit")]
        [HttpPost]
        [ProducesResponseType(typeof(AuditMessage), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Audit(string id, [FromBody] AuditMessage auditMessage)
        {

            return await Task.FromResult((IActionResult)null);
        }
        /// <summary>
        /// 分页获取某个任务单审核记录
        /// </summary>
        /// <param name="id">任务单编号</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [Route("{id}/audit")]
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<AuditMessage>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAudits([FromRoute]string id, [FromQuery]int page, [FromQuery]int rows)
        {

            return await Task.FromResult((IActionResult)null);
        }
    }
}