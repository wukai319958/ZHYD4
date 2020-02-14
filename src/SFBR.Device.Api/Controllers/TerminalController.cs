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
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using SFBR.Device.Api.Application.Commands.TerminalCommands;
using SFBR.Device.Api.Application.Queries;
using SFBR.Device.Api.Infrastructure.Services;
using SFBR.Device.Api.Models;
using SFBR.Device.Api.Models.Terminal;
using SFBR.Device.Common.ConfigModel.SkynetTerminal.Enums;
using SFBR.Device.Common.Interface;

namespace SFBR.Device.Api.Controllers
{
    /// <summary>
    /// 智维终端
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]//权限需要重写，开发和管理员 必须与普通用户分开
    public class TerminalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDeviceQueries _deviceQueries;
        private readonly IIdentityService _identityService;
        private readonly ILogger<DeviceController> _logger;
        private readonly IMapper _mapper;
        private readonly ISkynetTerminalClient _skynetTerminalClient;
        private readonly ITerminalQueries _terminalQueries;
        private readonly IMqttClient _mqttClient;
        private readonly IOptions<DeviceSettings> _options;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="deviceQueries"></param>
        /// <param name="identityService"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="skynetTerminalClient"></param>
        public TerminalController(IMediator mediator, IDeviceQueries deviceQueries, IIdentityService identityService, ILogger<DeviceController> logger, IMapper mapper, ISkynetTerminalClient skynetTerminalClient, ITerminalQueries terminalQueries, IMqttClient mqttClient, IOptions<DeviceSettings> options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deviceQueries = deviceQueries ?? throw new ArgumentNullException(nameof(deviceQueries));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _skynetTerminalClient = skynetTerminalClient ?? throw new ArgumentNullException(nameof(skynetTerminalClient));
            _terminalQueries = terminalQueries ?? throw new ArgumentException(nameof(terminalQueries));
            _mqttClient = mqttClient ?? throw new ArgumentNullException(nameof(mqttClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
        //TODO:1 设备控制、配置、读取相关操作接口；2 智维终端特有的管理性接口，比如配置经纬度、网络设备绑定端口等

        #region 查询接口
        #region 统计类接口
        /// <summary>
        /// 按状态统计站点
        /// </summary>
        /// <returns></returns>
        [Route("statistics/status")]
        [HttpGet]
        [ProducesResponseType(typeof(TerminalCountResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTerminalCountByStatus()
        {
            var result = await _terminalQueries.GetTerminalCountByStatus();
            return Ok(result);
        }

        /// <summary>
        /// 按状态统计照相机
        /// </summary>
        /// <returns></returns>
        [Route("statistics/camera/status")]
        [HttpGet]
        [ProducesResponseType(typeof(CameraCountResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCameraCountByStatus()
        {
            var result = await _terminalQueries.GetCameraCountByStatus();
            return Ok(result);
        }

        #endregion
        /// <summary>
        /// 分页获取站点列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页条数</param>
        /// <returns></returns>    
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<TerminalDevice>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTerminals([FromQuery]int page, [FromQuery]int rows)
        {
            var pageData = await _terminalQueries.GetTerminalPageAsync(Request.Query, page, rows);
            return Ok(pageData);
        }

        /// <summary>
        /// 获取未定位站点列表
        /// </summary>
        /// <returns></returns>    
        [Route("unpositioned")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TerminalDevice>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTerminalsWhereUnsetLocation()
        {
            var result = await _terminalQueries.GetTerminalsWhereUnsetLocation();
            return Ok(result);
        }


        /// <summary>
        ///分页获取摄像头列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>   
        [Route("camera")]
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<Camera>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCameras([FromQuery]int page, [FromQuery]int rows)
        {

            var result = await _terminalQueries.GetCameras(Request.Query, page, rows);
            return Ok(result);
        }
        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TerminalDevice), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDeviceDetailAsync(string id)
        {
            return Ok(await _deviceQueries.GetDeviceAsync(id));
        }
        /// <summary>
        ///根据设备id获取负载集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   
        [Route("{id}/load")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Load>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetLoads([FromRoute]string id)
        {
            var result = await _terminalQueries.GetLoads(id);
            return Ok(result);
        }

        /// <summary>
        ///根据设备id获取摄像头集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   
        [Route("{id}/camera")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Camera>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCameras([FromRoute]string id)
        {

            var result = await _terminalQueries.GetCameras(id);
            return Ok(result);
        }

        /// <summary>
        ///根据设备id获取配件集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   
        [Route("{id}/part")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Part>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetParts([FromRoute]string id)
        {

            var result = await _terminalQueries.GetParts(id);
            return Ok(result);
        }


        /// <summary>
        ///根据设备id获取配件开关集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   
        [Route("{id}/switch")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Application.Queries.Controller>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetControllers([FromRoute]string id)
        {

            var result = await _terminalQueries.GetControllers(id);
            return Ok(result);
        }

        #endregion

        #region 管理接口：设备名称修改，负载名称修改等等
        /// <summary>
        /// 站点定位
        /// </summary>
        /// <param name="id">设备id</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/location")]
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetLocation([FromRoute]string id, [FromBody]SetLocationCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (result) return Ok();
            return BadRequest();
        }
        /// <summary>
        /// 分配区域
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/region")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> SetDeviceRegion(string id, [FromBody]SetRegionCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (result) return Ok();
            return BadRequest();
        }
        /// <summary>
        /// 锁定配置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="functionCode">功能编号</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/function/{functionCode}/lockStatus")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> LockSetting(string id, string functionCode, [FromBody]LockSettingCommand command)
        {
            command.Id = id;
            command.FunctionCode = functionCode;
            var result = await _mediator.Send(command);
            if (result) return Ok();
            return BadRequest();
        }
        /// <summary>
        /// 设置回路名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber">回路编号</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/channel/{portNumber:int:range(1,7)}/name")]//自定义电源的电压可以自己修改
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> SetChannelName(string id, [FromRoute]int portNumber, [FromBody]SetChannelNameCommand command)
        {
            command.Id = id;
            command.PortNumber = portNumber;
            var result = await _mediator.Send(command);
            if (result) return Ok();
            return BadRequest();
        }
        /// <summary>
        /// 设置回路输出电压
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber">回路编号</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/channel/{portNumber:int:range(1,7)}/voltage")]//自定义电源的电压可以自己修改
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> SetChannelVoltage(string id, [FromRoute]int portNumber, [FromBody]SetChannelVoltageCommand command)
        {
            command.Id = id;
            command.PortNumber = portNumber;
            var result = await _mediator.Send(command);
            if (result) return Ok();
            return BadRequest();
        }
        /// <summary>
        /// 设置回路启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber">回路编号</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/channel/{portNumber:int:range(1,7)}/enabled")]//自定义电源的电压可以自己修改
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> SetChannelEnabled(string id, [FromRoute]int portNumber, [FromBody]SetChannelEnabledCommand command)
        {
            command.Id = id;
            command.PortNumber = portNumber;
            var result = await _mediator.Send(command);
            if (result) return Ok();
            return BadRequest();
        }

        /// <summary>
        /// 设置负载名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loadid">负载id</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/load/{loadid}/name")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public async Task<IActionResult> SetLoadName(string id, [FromRoute]string loadid, [FromBody]SetLoadNameCommand command)
        {
            command.Id = id;
            command.LoadId = loadid;
            var result = await _mediator.Send(command);
            if (result) return Ok();
            return BadRequest();
        }

        /// <summary>
        /// 修改负载扩展属性值
        /// （目前只有摄像机才有扩展属性，暂不开放添加和删除自定义属性）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loadid">负载id</param>
        /// <param name="propid">负载id</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/load/{loadid}/prop/{propid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPatch]
        public Task<IActionResult> SetLoadPropValue(string id, [FromRoute]string loadid, [FromRoute]string propid, [FromBody]SetChannelNameCommand command)
        {

            return null;
        }

        /// <summary>
        /// 添加负载扩展属性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loadid">负载id</param>
        /// <param name="propid">负载id</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/load/{loadid}/prop")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public Task<IActionResult> AddLoadProp(string id, [FromRoute]string loadid, [FromRoute]string propid, [FromBody]SetChannelNameCommand command)
        {

            return null;
        }
        /// <summary>
        /// 移除负载扩展属性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loadid">负载id</param>
        /// <param name="propid">负载id</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/load/{loadid}/prop")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        public Task<IActionResult> RemoveLoadProp(string id, [FromRoute]string loadid, [FromRoute]string propid, [FromBody]SetChannelNameCommand command)
        {

            return null;
        }
        /// <summary>
        /// 添加负载
        /// 简易版本只有一个电源输出端口，端口拥有哪些设备都是用户自己定义
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/load")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public Task<IActionResult> AddLoad(string id, [FromBody]SetChannelNameCommand command)
        {

            return null;
        }

        /// <summary>
        /// 移除负载
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("{id}/load")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        public Task<IActionResult> RemoveLoad(string id, [FromBody]SetChannelNameCommand command)
        {

            return null;
        }

        #endregion

        #region 功能接口：校时，设置阈值（批量设置时，功能配置已经锁定的当个设备需要过滤）
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="func"></param>
        /// <param name="functionCode">功能编号</param>
        /// <returns></returns>
        private async Task<IActionResult> ExecFunction(string id, Func<DeviceModel, Common.ResponseModel> func, string functionCode)
        {
            //TODO:写操作日志，丢入消息队列
            var device = await _deviceQueries.GetSimpleDeviceAsync(id);
            if (device == null) return NotFound();
            var function = await _deviceQueries.GetFunctionAsync(id, functionCode);
            if ("post".Equals(Request.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                if (function.LockSetting) return BadRequest(new LogicModel(false, message: "该功能已经锁定，请解锁后再配置"));
            }
            var result = func(device);
            await SaveActionLog(device, function, result);
            if (result.Success)
            {
                return Ok(_mapper.Map<LogicModel>(result));
            }
            else
            {
                return BadRequest(_mapper.Map<LogicModel>(result));
            }
        }

        private async Task SaveActionLog(DeviceModel device, DeviceFunction function, Common.ResponseModel result)
        {
            try
            {
                string actionName = "读取";
                if ("post".Equals(Request.Method, StringComparison.InvariantCultureIgnoreCase))
                {
                    actionName = "配置";
                }
                if (Request.Body != null && Request.Body.CanSeek)
                    Request.Body.Seek(0, System.IO.SeekOrigin.Begin);
                string description = result.Success ? ($"{actionName}{function.FunctionName}成功") : ($"{actionName}{function.FunctionName}失败。失败原因:{result.Message ?? "未知"}");
                using (var sr = new System.IO.StreamReader(Request.Body ?? new System.IO.MemoryStream()))
                {
                    await _mqttClient.PublishAsync
                        (
                        new MQTTnet.MqttApplicationMessageBuilder()
                        .WithExactlyOnceQoS()
                        .WithTopic("log/devaction")
                        .WithPayload(new
                        {
                            Account = _identityService?.GetUserName(),
                            Name = _identityService?.GetName(),
                            DeviceCode = device.EquipNum,
                            device.DeviceName,
                            function.FunctionName,
                            ActionDesciption = description,
                            ApiUrl = Request.Path.Value + Request.QueryString.Value,
                            Paramaters = sr.ReadToEnd(),
                            Request.ContentType,
                            ApplicationContext = Program.AppName,
                            ActionTime = DateTime.UtcNow
                        }.ToJson())
                        .Build()
                        );
                }
            }
            catch { }
        }
        #region 设备时间

        /// <summary>
        /// 读取设备时间
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns></returns>
        [Route("{id}/deviceTime")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReadDeviceTime([FromRoute]string id)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.RdDeviceTime(device.EquipNum), "deviceTime");
        }
        /// <summary>
        /// 单个校时
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/deviceTime")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetDeviceTime([FromRoute]string id)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.WdDeviceTime(device.EquipNum), "deviceTime");
        }
        /// <summary>
        /// 批量校时
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("deviceTime")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SetDeviceTimeBatch()
        {

            return null;
        }

        #endregion

        #region 摄像头分配
        /// <summary>
        /// 获取摄像头位置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index">摄像头编号：1-8</param>
        /// <returns></returns>
        [Route("{id}/camera/{index:int:range(1,8)}/mountPort")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCameraPosition([FromRoute]string id, [FromRoute]int index)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.RdVedioAssign(index, device.EquipNum), "mountPort");
        }

        /// <summary>
        /// 设置摄像头位置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index">摄像头编号：1-8</param>
        /// <param name="position"></param>
        /// <returns></returns>
        [Route("{id}/camera/{index:int:range(1,8)}/mountPort")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCameraPosition([FromRoute]string id, [FromRoute]int index, [FromBody]CameraPostion position)
        {
       
            position.CameraIndex = index;

            VedioChannelTypeEnum type = VedioChannelTypeEnum.OutNot;

            switch (position.Postion)
            {

                case Postion.OutNot:
                default:
                    type = VedioChannelTypeEnum.OutNot;
                    break;
                case Postion.Output1:
                    type = VedioChannelTypeEnum.Output1;
                    break;
                case Postion.Output2:
                    type = VedioChannelTypeEnum.Output2;
                    break;
                case Postion.AC220V:
                    type = VedioChannelTypeEnum.AC220V;
                    break;
            }
            return await ExecFunction(id, device => _skynetTerminalClient.WdVedioAssign(position.CameraIndex, type, device.EquipNum), "mountPort");

        }

        /// <summary>
        /// 批量设置摄像头位置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        [Route("camera/mountPort")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SetCameraPositionBatch([FromBody]CameraPostion position)
        {

            return null;
        }

        #endregion

        #region 摄像头IP
        /// <summary>
        /// 获取摄像头ip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index">摄像机编号1-8</param>
        /// <returns></returns>
        [Route("{id}/camera/{index:int:range(1,8)}/ip")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCameraIP([FromRoute]string id, [FromRoute]int index)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.RdCameraIP(index, device.EquipNum), "deviceip");
        }

        /// <summary>
        /// 设置摄像头ip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index">摄像机编号1-8</param>
        /// <param name="cameraIP"></param>
        /// <returns></returns>
        [Route("{id}/camera/{index:int:range(1,8)}/ip")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCameraIP([FromRoute]string id, [FromRoute]int index, [FromBody]CameraIP cameraIP)
        {
            Common.ConfigModel.SkynetTerminal.CameraIPDto ip = new Common.ConfigModel.SkynetTerminal.CameraIPDto()
            {
                Number = index,
                IP = cameraIP.IP,
                Enable = cameraIP.Enabled
            };
            return await ExecFunction(id, device => _skynetTerminalClient.WdCameraIP(ip, device.EquipNum), "deviceip");
        }

        /// <summary>
        /// 批量设置摄像头ip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cameraIP"></param>
        /// <returns></returns>
        [Route("camera/{index:int:range(1,8)}/ip")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SetCameraIPBatch([FromBody]CameraIP cameraIP)
        {

            return null;
        }
        #endregion

        #region 摄像头监测频率
        /// <summary>
        /// 获取摄像头监测频率
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/camera/checkfrequency")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCameraCheckFrequency([FromRoute]string id)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.RdCameraFaultCheckTime(id), "checkfrequency");
        }

        /// <summary>
        /// 设置摄像头监测频率
        /// </summary>
        /// <param name="id"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        [Route("{id}/camera/checkfrequency")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCameraCheckFrequency([FromRoute]string id, [FromBody]Simple<int> frequency)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.WdCameraFaultCheckTime(frequency.Data, device.EquipNum), "checkfrequency");
        }

        /// <summary>
        /// 批量设置摄像头监测频率
        /// </summary>
        /// <param name="id"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        [Route("camera/checkfrequency")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SetCameraCheckFrequencyBatch([FromBody]CameraPostion frequency)
        {

            return null;
        }

        #endregion

        #region 门磁撤防
        /// <summary>
        /// 读取撤防时长
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/defending")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDefending([FromRoute]string id)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.RdDisarmControl(device.EquipNum), "defending");
        }
        /// <summary>
        /// 设置撤防时长
        /// </summary>
        /// <param name="id"></param>
        /// <param name="defending"></param>
        /// <returns></returns>
        [Route("{id}/defending")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetDefending([FromRoute]string id, [FromBody]DefendingModel defending)
        {
            DateTime start = Convert.ToDateTime(defending.Start);
            DateTime end = Convert.ToDateTime(defending.End);
            return await ExecFunction(id, device => _skynetTerminalClient.WdDisarmControl(defending.Enable, start, end, device.EquipNum), "defending");
        }
        /// <summary>
        /// 批量设置撤防时长
        /// </summary>
        /// <param name="id"></param>
        /// <param name="defending"></param>
        /// <returns></returns>
        [Route("defending")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SetDefendingBatcg([FromBody]DefendingModel defending)
        {

            return null;
        }
        #endregion

        #region 报警阈值
        /// <summary>
        /// 读取报警阈值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/threshold")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetThreshold([FromRoute]string id)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.RdVATHLimit(device.EquipNum), "threshold");
        }
        /// <summary>
        /// 设置报警阈值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="thresholdModel"></param>
        /// <returns></returns>
        [Route("{id}/threshold")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetThreshold([FromRoute]string id, [FromBody]ThresholdModel thresholdModel)
        {
            Common.ConfigModel.SkynetTerminal.VATHLimitDto limit = new Common.ConfigModel.SkynetTerminal.VATHLimitDto()
            {
                UpperV = thresholdModel.Upper_V,
                LowerV = thresholdModel.Lower_V,
                UpperA = thresholdModel.Upper_A,
                LowerA = thresholdModel.Lower_A,
                UpperT = thresholdModel.Upper_T,
                LowerT = thresholdModel.Lower_T,
                UpperH = Convert.ToInt32(thresholdModel.Upper_H)
            };
            return await ExecFunction(id, device => _skynetTerminalClient.WdVATHLimit(limit, device.EquipNum), "threshold");
        }
        /// <summary>
        /// 批量设置报警阈值
        /// </summary>
        /// <param name="thresholdModel"></param>
        /// <returns></returns>
        [Route("threshold")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SetThresholdBatch([FromBody]ThresholdModel thresholdModel)
        {

            return null;
        }
        #endregion

        #region 回路模式
        /// <summary>
        /// 读取回路模式
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber"></param>
        /// <returns></returns>
        [Route("{id}/channel/{portNumber:int:range(1,5)}/model")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetChannelModel([FromRoute]string id, [FromRoute]int portNumber)
        {
            return await ExecFunction(id, device =>
            {
                var chanel = _deviceQueries.GetChannelAsync(device.Id, portNumber).Result;
                ChannelTypeEnum type = (ChannelTypeEnum)chanel.PortType;
                return _skynetTerminalClient.RdChannelMode(type, device.EquipNum);
            }, "model");
        }
        /// <summary>
        /// 设置回路模式
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber">回路端口</param>
        /// <param name="setting">0 禁止设备主动控制；1 允许设备主动控制（比如温度过高时自动启动风扇）</param>
        /// <returns></returns>
        [Route("{id}/channel/{portNumber:int:range(1,5)}/model")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetChannelModel([FromRoute]string id, [FromRoute]int portNumber, [FromBody]Simple<int> setting)
        {
            return await ExecFunction(id, device =>
            {
                var chanel = _deviceQueries.GetChannelAsync(device.Id, portNumber).Result;
                Common.ConfigModel.ChannelValueDto<ChannelTypeEnum, int> model = new Common.ConfigModel.ChannelValueDto<ChannelTypeEnum, int>() { Channel = (ChannelTypeEnum)chanel.PortType, Value = setting.Data };
                return _skynetTerminalClient.ChannelIsAutoControl(model, device.EquipNum);
            }, "model");
        }
        /// <summary>
        /// 批量设置回路模式
        /// </summary>
        /// <param name="portNumber">回路端口</param>
        /// <param name="setting">0 禁止设备主动控制；1 允许设备主动控制（比如温度过高时自动启动风扇）</param>
        /// <returns></returns>
        [Route("channel/{portNumber:int:range(1,5)}/model")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SetChannelModelBatch([FromRoute]int portNumber, [FromBody]Simple<int> setting)
        {

            return null;
        }
        #endregion

        #region 回路定时
        /// <summary>
        /// 读取回路定时任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber"></param>
        /// <returns></returns>
        [Route("{id}/channel/{portNumber:int:range(1,5)}/task")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetChannelTask([FromRoute]string id, [FromRoute]int portNumber)
        {
            return await ExecFunction(id, device =>
            {
                var chanel = _deviceQueries.GetChannelAsync(device.Id, portNumber).Result;
                ChannelTypeEnum type = (ChannelTypeEnum)chanel.PortType;
                return _skynetTerminalClient.RdChannelTask(type, device.EquipNum);
            }, "task");
        }
        /// <summary>
        /// 设置回路定时任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber">回路端口</param>
        /// <param name="setting">0 禁止设备主动控制；1 允许设备主动控制（比如温度过高时自动启动风扇）</param>
        /// <returns></returns>
        [Route("{id}/channel/{portNumber:int:range(1,5)}/task")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetChannelTask([FromRoute]string id, [FromRoute]int portNumber, [FromBody]List<ChannelTask> setting)
        {
            //return await ExecFunction(id, device => _skynetTerminalClient.WdChannelTask(type, taskPlan, device.EquipNum), "task");

            return await ExecFunction(id, device =>
            {
                List<Common.ConfigModel.SkynetTerminal.SkynetTerminalTaskplanDto> taskPlan = new List<Common.ConfigModel.SkynetTerminal.SkynetTerminalTaskplanDto>();
                foreach (var oo in setting)
                {
                    Common.ConfigModel.SkynetTerminal.SkynetTerminalTaskplanDto task = new Common.ConfigModel.SkynetTerminal.SkynetTerminalTaskplanDto()
                    {
                        Number = oo.Index,
                        Enable = oo.Enable,
                        TaskType = oo.Onece ? Common.ConfigModel.Enums.TaskPlanTypeEnum.Once : Common.ConfigModel.Enums.TaskPlanTypeEnum.Evenyday,
                        StartTime = Convert.ToDateTime(oo.Start),
                        EndTime = Convert.ToDateTime(oo.End)
                    };
                    taskPlan.Add(task);
                }
                var chanel = _deviceQueries.GetChannelAsync(device.Id, portNumber).Result;
                ChannelTypeEnum type = (ChannelTypeEnum)chanel.PortType;
                return _skynetTerminalClient.WdChannelTask(type, taskPlan, device.EquipNum);
            }, "task");
        }
        /// <summary>
        /// 批量设置回路定时任务
        /// </summary>
        /// <param name="portNumber">回路端口</param>
        /// <param name="setting"></param>
        /// <returns></returns>
        [Route("channel/{portNumber:int:range(1,5)}/task")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SetChannelTaskBatch([FromRoute]int portNumber, [FromBody]List<ChannelTask> setting)
        {
            return null;
        }
        #endregion

        #region 回路控制
        /// <summary>
        /// 读取开关状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber"></param>
        /// <returns></returns>
        [Route("{id}/channel/{portNumber:int:range(1,5)}/SwitchStatus")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetChannelSwitchStatus([FromRoute]string id, [FromRoute]int portNumber)
        {
            return await Task.FromResult(Ok());
        }
        /// <summary>
        /// 设置开关状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portNumber">回路端口</param>
        /// <param name="switchStatuse">1 开，0 关</param>
        /// <returns></returns>
        [Route("{id}/channel/{portNumber:int:range(1,5)}/SwitchStatus")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetChannelSwitchStatus([FromRoute]string id, [FromRoute]int portNumber, [FromBody]Simple<int> switchStatuse)
        {
            return await ExecFunction(id, device =>
            {
                var channel = _deviceQueries.GetChannelAsync(device.Id, portNumber).Result;
                var request = new Common.ConfigModel.ChannelValueDto<Common.ConfigModel.SkynetTerminal.Enums.ChannelTypeEnum, int>
                {
                    Channel = (Common.ConfigModel.SkynetTerminal.Enums.ChannelTypeEnum)channel.PortType,
                    Value = switchStatuse.Data
                };
                return _skynetTerminalClient.ChannelStatusControl(request, device.EquipNum);
            }, "onoff");


        }

        /// <summary>
        /// 批量设置开关状态
        /// </summary>
        /// <param name="portNumber">回路端口</param>
        /// <param name="switchStatuses">on 开，off 关</param>
        /// <returns></returns>
        [Route("channel/{portNumber:int:range(1,5)}/SwitchStatus")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> SetChannelSwitchStatusBatch([FromRoute]int portNumber, [FromBody]List<SwitchStatus> switchStatuses)
        {

            return null;
        }
        #endregion

        #region GPS定位
        /// <summary>
        /// 读取定位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/position")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPosition([FromRoute]string id)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.RdLatitudeAndLongitude(device.EquipNum), "position");
        }
        #endregion
        #region 终端信息
        /// <summary>
        /// 读取设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/deviceinfo")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInfoFromDevice([FromRoute]string id)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.RdDeviceInfo(device.EquipNum), "deviceinfo");
        }
        #endregion
        #endregion

        #region 摄像机视频流

        /// <summary>
        /// 获取摄像头视频流
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cameraIndex"></param>
        /// <returns></returns>
        [Route("{id}/camera/{cameraIndex:int:range(1,8)}/vedio")]
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCameraVedioUrl([FromRoute]string id, [FromRoute]int cameraIndex)
        {
            var device = await _deviceQueries.GetDeviceAsync(id) as TerminalDevice;
            if (device == null) return BadRequest("站点不存在");
            var camera = device.Loads?.FirstOrDefault(x => x.EquipNum.EndsWith(cameraIndex.ToString())) as Camera;
            if (camera == null) return BadRequest("摄像机不存在");
            string account = camera.DeviceProps.Where(x => x.PropName == "account").FirstOrDefault()?.PropValue;
            string password = camera.DeviceProps.Where(x => x.PropName == "password").FirstOrDefault()?.PropValue;
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password)) return BadRequest("摄像机信息错误");
            string url = CustomExtensionsMethods.GetCameraVedioUrl(account, password, camera.DeviceIP, localIP: _options.Value.LocalIP, vedioResalution: _options.Value.VedioResalution);
            return Ok(url);
        }

        /// <summary>
        /// 获取网络视频流
        /// </summary>
        /// <param name="sourceurl"></param>
        /// <returns></returns>
        [Route("{sourceurl}/camera/vedio")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVedioUrl([FromRoute]string sourceurl)
        {
            sourceurl = sourceurl.Replace("%2F", "/");
            string url = CustomExtensionsMethods.GetCameraVedioUrl(sourceurl, localIP: _options.Value.LocalIP, vedioResalution: _options.Value.VedioResalution);
            return await Task.FromResult( Ok(url));
        }


        /// <summary>
        /// 通道重启
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <param name="number">通道类型：0视频通道 1光端机 2补光灯 3加热设备 4风扇 5其他设备</param>
        [Route("{id}/ResetDevice/{number:int:range(0,5)}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetDevice([FromRoute]string id,int number)
        {
            return await ExecFunction(id, device => _skynetTerminalClient.ChannelRestartControl((ChannelTypeEnum)number, id), "ResetDevice");
        }

        #endregion
    }
}