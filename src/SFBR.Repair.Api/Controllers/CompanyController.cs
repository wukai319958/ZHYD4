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
    /// 维保单位维护服务
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    [ApiController]
    public class CompanyController : ControllerBase
    {
        
        /// <summary>
        /// 分页获取运维单位
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<Company>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int page,int rows)
        {

            return await Task.FromResult((IActionResult)null);
        }

        /// <summary>
        /// 获取单位详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {

            return await Task.FromResult((IActionResult)null);
        }

        /// <summary>
        /// 获取单位详情
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] Company company)
        {

            return await Task.FromResult((IActionResult)null);
        }


        /// <summary>
        /// 修改单位详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(string id,[FromBody] Company company)
        {

            return await Task.FromResult((IActionResult)null);
        }

        /// <summary>
        /// 删除单位
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
        /// 分页获取运维单位员工
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [Route("Employee")]
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<Employee>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetEmployees(int page, int rows)
        {

            return await Task.FromResult((IActionResult)null);
        }

        /// <summary>
        /// 获取单位员工详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/Employee")]
        [HttpGet]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetEmployee(string id)
        {

            return await Task.FromResult((IActionResult)null);
        }

        /// <summary>
        /// 添加单位员工信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [Route("{id}/Employee")]
        [HttpPost]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostEmployee(string id,[FromBody]Employee employee)
        {

            return await Task.FromResult((IActionResult)null);
        }


        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="id">公司id</param>
        /// <param name="employeeId">员工id</param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [Route("{id}/Employee/{employeeId}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutEmployee(string id, string employeeId, [FromBody] Employee employee)
        {

            return await Task.FromResult((IActionResult)null);
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Route("{id}/Employee/{employeeId}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteEmployee(string id,string employeeId)
        {

            return await Task.FromResult((IActionResult)null);
        }

    }
}