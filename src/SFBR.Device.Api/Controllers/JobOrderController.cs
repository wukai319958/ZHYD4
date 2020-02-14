using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SFBR.Device.Api.Controllers
{
    /// <summary>
    /// 任务单服务
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    public class JobOrderController : ControllerBase
    {
        //TODO:任务单的增删改及详情查询
    }
}