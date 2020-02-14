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
using SFBR.Device.Api.Application.Commands.Device;
using SFBR.Device.Api.Application.Queries;
using SFBR.Device.Api.Infrastructure.Services;
using SFBR.Device.Api.Models;

namespace SFBR.Device.Api.Controllers
{
    /// <summary>
    /// 设备服务（公共服务）
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    public class DeviceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDeviceQueries _deviceQueries;
        private readonly IIdentityService _identityService;
        private readonly ILogger<DeviceController> _logger;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="deviceQueries"></param>
        /// <param name="identityService"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public DeviceController(IMediator mediator, IDeviceQueries deviceQueries,IIdentityService identityService, ILogger<DeviceController> logger,IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deviceQueries = deviceQueries ?? throw new ArgumentNullException(nameof(deviceQueries));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 手动添加设备
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DeviceModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddDevice([FromBody]Models.CreateDeviceModel command)
        {
            var temp = _mapper.Map<CreateDeviceCommand>(command);
            temp.TentantId = _identityService.GetTentantId();
            temp.DeviceIP = "0.0.0.0";
            var device = await _mediator.Send(temp);
            if (device == null) return BadRequest();
            return Created("/api/device/" + device.Id, device);
        }

        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DeviceModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(Domain.AggregatesModel.DeviceAggregate.Device), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDeviceDetailAsync(string id)
        {
            return Ok(await _deviceQueries.GetDeviceAsync(id));
        }

        /// <summary>
        /// 修改终端名称
        /// </summary>
        /// <param name="id">设备id</param>
        /// <param name="command">名称</param>
        /// <returns></returns>
        [Route("{id}/name")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> SetDeviceName(string id,[FromBody]Simple<string> command)
        {
            var result = await _mediator.Send(new SetDeviceNameCommand(id, command.Data));
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
        /// 设置运维信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/OAMInfo")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> SetOAMInfo(string id, [FromBody]Simple<string> command)
        {

            return await Task.FromResult((IActionResult)null);
        }
        /// <summary>
        /// 获取发现的设备
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns></returns>
        [Route("discovery")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<DeviceModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDiscoveryAsync(string id)
        {
            return Ok(await _deviceQueries.GetDeviceAsync(id));
        }

        //TODO:
    }
}