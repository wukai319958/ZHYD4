using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFBR.Device.Api.Application.Commands.Region;
using SFBR.Device.Api.Application.Queries;
using SFBR.Device.Api.Infrastructure.Services;
using SFBR.Device.Api.Models;

namespace SFBR.Device.Api.Controllers
{

    /// <summary>
    /// 区域服务
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    public class RegionController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IRegionQueries _regionQueries;
        private readonly IIdentityService _identityService;
        private readonly ILogger<RegionController> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="regionQueries"></param>
        /// <param name="identityService"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public RegionController(IMediator mediator, IRegionQueries regionQueries, IIdentityService identityService, ILogger<RegionController> logger, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _regionQueries = regionQueries ?? throw new ArgumentNullException(nameof(regionQueries));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 根据区域获取站点集合
        /// </summary>
        /// <param name="regionId">页码</param>
        /// <returns></returns>    
        [Route("{id}/terminals")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TerminalDevice>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTerminals(string id)
        {
            //该接口定义的位置有问题，
            return await Task.FromResult((IActionResult)null);
        }


        /// <summary>
        /// 获取区域集合
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RegionModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRegionList()
        {

            return Ok(await _regionQueries.GetRegionList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/children")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RegionModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetChildren(string id)
        {

            return Ok(await _regionQueries.GetChildren(id));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/parent")]
        [HttpGet]
        [ProducesResponseType(typeof(RegionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetParent(string id)
        {

            return Ok(await _regionQueries.GetParent(id));
        }

        /// <summary>
        /// 所有区域
        /// </summary>
        /// <returns></returns>
        [Route("tree")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RegionTreeModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTree()
        {

            return Ok(await _regionQueries.GetTree());
        }
        /// <summary>
        /// 创建区域
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(RegionModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddRegion([FromBody]CreateRegionCommand command)
        {
            command.TenTantId = _identityService.GetTentantId();
            var result = await _mediator.Send(command);
            if (result == null) return BadRequest();
            return Created("/api/region/" + result.Id, result);
        }

        /// <summary>
        /// 修改区域名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/name")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> SetRegionName(string id, [FromBody]Simple<string> name)
        {
            var result = await _mediator.Send(new SetRegionNameCommand(id, name.Data));
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        public async Task<IActionResult> RemoveRegion(string id)
        {
            var result = await _mediator.Send(new RemoveRegionCommand(id));
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