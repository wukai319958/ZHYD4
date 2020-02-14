using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFBR.Device.Api.Models;
using SFBR.Device.Domain.AggregatesModel.UserAggregate;
using SFBR.Device.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SFBR.Device.Api.Controllers
{
    /// <summary>
    /// 账户服务
    /// </summary>
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly DeviceContext _db;
        public AccountController(DeviceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        [ProducesResponseType(typeof(LogicModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Login(string username, string password)
        {
            var result = _db.Users.FirstOrDefault(x => x.Account == username);
            if (result == null) return Ok(new LogicModel(false, "用户不存在"));
            if (result.Password.Equals(password))
            {
                result.Token = result.Account;
                return Ok(new LogicModel(true, "验证通过", 0, result));
            }
            else
            {
                return Ok(new LogicModel(false, "密码错误"));
            }
        }
    }
}
