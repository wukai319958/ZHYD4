using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFBR.Device.Api.Application.Queries;

namespace SFBR.Device.Api.Controllers
{
    /// <summary>
    /// 综合业务数据查询服务
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    public class DataController : ControllerBase
    {
        //TODO:各类统计查询

       
    }
}