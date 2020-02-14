using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SFBR.Device.Api.Application.IntegrationEvents;
using SFBR.Device.Api.Application.Queries;
using SFBR.Device.Api.Infrastructure.Services;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate;
using SFBR.Device.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Device
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, Queries.DeviceModel>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IMediator _mediator;
        private readonly IDeviceIntegrationEventService _deviceIntegrationEventService;
        private readonly ILogger<CreateDeviceCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateDeviceCommandHandler(IDeviceRepository deviceRepository, IDeviceTypeRepository deviceTypeRepository, IIdentityService identityService, IMediator mediator, IDeviceIntegrationEventService deviceIntegrationEventService, ILogger<CreateDeviceCommandHandler> logger, IMapper mapper)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _deviceTypeRepository = deviceTypeRepository ?? throw new ArgumentNullException(nameof(deviceTypeRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deviceIntegrationEventService = deviceIntegrationEventService ?? throw new ArgumentNullException(nameof(deviceIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Queries.DeviceModel> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            var deviceTypeInfo = await _deviceTypeRepository.FindAsync(request.DeviceTypeCode, request.ModelCode);
            if (deviceTypeInfo == null)
            {
                throw new DeviceDomainException("不支持的设备类型");
            }

            

            if (await _deviceRepository.ExistsByEquipNumAsync(request.EquipNum))
            {
                throw new DeviceDomainException($"编号为{request.EquipNum}的设备已存在！");
            }

            var entity = CreateDevice(request, deviceTypeInfo);
            _deviceRepository.Add(entity);
            var result = await _deviceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result) return null;
            return _mapper.Map<TerminalDevice>(entity);
           
        }
        private Domain.AggregatesModel.DeviceAggregate.Device CreateDevice(CreateDeviceCommand request, Domain.AggregatesModel.DeviceTypeAggregate.DeviceType deviceType)
        {
            if (nameof(Terminal).Equals(deviceType.Code, StringComparison.InvariantCultureIgnoreCase))
            {
                Terminal terminal = new Terminal(request.TentantId, request.DeviceName, request.EquipNum, request.Enabled, request.ModelCode, request.DeviceIP, request.DevicePort, request.ServerIP, request.ServerPort, request.Description, null, request.Connection, request.RegionId, null);
                if (deviceType.Channels != null)
                {
                    foreach (var channel in deviceType.Channels)
                    {
                        //添加回路
                        terminal.AddChannel(terminal.Id, channel.PortNumber,channel.PortType);
                        if (channel.PortNumber < 6)
                        {
                            //添加设备定时任务
                            for (int taskId = 1; taskId <= 3; taskId++)
                            {
                                terminal.AddTimedTask(terminal.Id, channel.PortNumber, false, taskId.ToString(), execAction: ExecAction.On);
                                terminal.AddTimedTask(terminal.Id, channel.PortNumber, false, taskId.ToString(), execAction: ExecAction.Off);
                            }
                        }

                    }
                }
                //添加控制器
                if (deviceType.Controllers != null)
                {
                    foreach (var controller in deviceType.Controllers)
                    {
                        terminal.AddController(terminal.Id, controller.ControllerCode, controllerStatus: controller.ControllerStatus, description: controller.Description);
                    } 
                }
                //添加传感器
                if (deviceType.Sensors != null)
                {
                    foreach (var sensor in deviceType.Sensors)
                    {
                        terminal.AddSensor(terminal.Id, sensor.SensorCode);
                    } 
                }
                //添加配件
                if (deviceType.Parts != null)
                {
                    foreach (var part in deviceType.Parts)
                    {
                        terminal.AddPart(terminal.Id, part.PartCode,enabled:part.Enabled);
                    } 
                }
                
                //添加设备功能
                if (deviceType.Functions != null)
                {
                    foreach (var function in deviceType.Functions)
                    {
                        terminal.AddFunction(terminal.Id, function.FunctionCode,function.CallbackCodes,function.SettingTypeName, setting:function.Setting);
                    } 
                }
                //TODO:添加负载
                terminal.AddLoad(terminal.TentantId, "补光灯", $"{terminal.EquipNum}_{nameof(DeviceCode.FillLight)}_1", nameof(DeviceCode.FillLight), 1, parentId: terminal.Id);
                for (int channelIndex = 1; channelIndex < 9; channelIndex++)
                {
                    string key = $"{terminal.EquipNum}_{nameof(DeviceCode.Camera)}_{channelIndex}";
                    terminal.AddLoad(terminal.TentantId, "摄像机"+ channelIndex, $"{terminal.EquipNum}_{nameof(DeviceCode.Camera)}_{channelIndex}", nameof(DeviceCode.Camera), 2, parentId: terminal.Id);
                }
                terminal.AddLoad(terminal.TentantId, "光通", $"{terminal.EquipNum}_{nameof(DeviceCode.Optical)}_1", nameof(DeviceCode.Optical), 3, parentId: terminal.Id);
                terminal.AddLoad(terminal.TentantId, "加热电源", $"{terminal.EquipNum}_{nameof(DeviceCode.Heater)}_1", nameof(DeviceCode.Heater), 4, parentId: terminal.Id);
                terminal.AddLoad(terminal.TentantId, "风扇", $"{terminal.EquipNum}_{nameof(DeviceCode.Fan)}_1", nameof(DeviceCode.Fan), 5, parentId: terminal.Id);
                //添加警报器
                if (deviceType.Alarms != null)
                {
                    foreach (var alarm in deviceType.Alarms)
                    {
                        terminal.AddAlarm(terminal.Id, alarm.AlarmCode, alarm.TargetCode, alarm.NormalValue);
                    }
                }
                //TODO:设置默认位置
                terminal.SetLocation(0, 0, false);
                return terminal;
            }
            else
            {
                throw new DeviceDomainException("不支持的设备类型");
            }
        }
    }
}
