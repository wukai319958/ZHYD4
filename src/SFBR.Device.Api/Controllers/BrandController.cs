using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFBR.Device.Api.Application.Commands.Brand;
using SFBR.Device.Api.Application.Queries;
using SFBR.Device.Api.Infrastructure.Services;
using SFBR.Device.Api.Models;

namespace SFBR.Device.Api.Controllers
{
    /// <summary>
    /// 品牌管理服务
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    public class BrandController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IBrandQueries _brandQueries;
        private readonly ILogger<BrandController> _logger;
        private readonly IIdentityService _identityService;

        public BrandController(IMediator mediator, IBrandQueries brandQueries, ILogger<BrandController> logger, IIdentityService identityService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _brandQueries = brandQueries ?? throw new ArgumentNullException(nameof(brandQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }


        //TODO:品牌的增删改查


        /// <summary>
        /// 获取品牌集合
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BrandViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBrandList()
        {
            return Ok(await _brandQueries.GetBrandList());
        }

        /// <summary>
        /// 创建品牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BrandViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddBrand([FromBody]CreateBrandCommand command)
        {
            command.TentantId = _identityService.GetTentantId();
            var result = await _mediator.Send(command);
            if (result == null) return BadRequest();
            return Created("/api/brand/" + result.Id, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        public async Task<IActionResult> RemoveBrand(string id)
        {

            var result = await _mediator.Send(new RemoveBrandCommand { Id = id });
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}