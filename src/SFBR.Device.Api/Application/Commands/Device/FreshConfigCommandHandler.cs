using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFBR.Device.Common.ConfigModel.SkynetTerminal.Models;
using SFBR.Device.Common.Tool;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;

namespace SFBR.Device.Api.Application.Commands.Device
{
    public class FreshConfigCommandHandler : IRequestHandler<FreshConfigCommand>
    {
        private readonly IDeviceRepository _deviceRepository;

        public FreshConfigCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<Unit> Handle(FreshConfigCommand request, CancellationToken cancellationToken)
        {
            var value = request.Value;
            var entity = await _deviceRepository.FindByEquipNumAsync(value.UniqueId) as Terminal;
            if (entity != null && entity.DevFunctions != null)
            {
                var function = entity.DevFunctions.FirstOrDefault(where => where.CallbackCodes.Contains(value.CmdName));
                if (function == null) return Unit.Value;
                if (function.FunctionCode == "task")//定时任务
                {
                    var tasks = function.Setting.ToObj<List<ChannelTaskPlanResultDto>>();
                    var temp = value as ChannelTaskPlanResultDto;
                    var target = tasks.FirstOrDefault(where => where.ChannelType == temp.ChannelType);
                    if (target != null) target = temp;
                    entity.SetFunctionSetting(function.FunctionCode, tasks.ToJson());
                }
                else if (function.FunctionCode == "deviceip")//设备IP配置
                {
                    var tasks = function.Setting.ToObj<List<CameraIPResultDto>>();
                    var temp = value as CameraIPResultDto;
                    var target = tasks.FirstOrDefault(where => where.CameraIP.Number == temp.CameraIP.Number);
                    if (target == null) tasks.Add(temp); else target = temp;
                    entity.SetCameraIP(temp.CameraIP.Number, temp.CameraIP.IP);
                    entity.SetCameraEnabled(temp.CameraIP.Number, temp.CameraIP.Enable);
                    entity.SetFunctionSetting(function.FunctionCode, tasks.ToJson());
                }
                else if (function.FunctionCode == "model")//工作模式
                {
                    var tasks = function.Setting.ToObj<List<ChannelModeResultDto>>();
                    var temp = value as ChannelModeResultDto;
                    var target = tasks.FirstOrDefault(where => where.ChannelType == temp.ChannelType);
                    if (target == null) tasks.Add(temp); else target = temp;
                    entity.SetFunctionSetting(function.FunctionCode, tasks.ToJson());
                }
                else if (function.FunctionCode == "mountPort")//视频分配
                {
                    var tasks = function.Setting.ToObj<List<VedioChannelAssignResultDto>>();
                    var temp = value as VedioChannelAssignResultDto;
                    var target = tasks.FirstOrDefault(where => where.CameraChannel == temp.CameraChannel);
                    if (target == null) tasks.Add(temp); else target = temp;
                    switch (temp.VedioChannelType)
                    {
                        case Common.ConfigModel.SkynetTerminal.Enums.VedioChannelTypeEnum.AC220V:
                            entity.SetCameraMountPort(temp.CameraChannel, 2);
                            break;
                        case Common.ConfigModel.SkynetTerminal.Enums.VedioChannelTypeEnum.Output1:
                            entity.SetCameraMountPort(temp.CameraChannel, 6);
                            break;
                        case Common.ConfigModel.SkynetTerminal.Enums.VedioChannelTypeEnum.Output2:
                            entity.SetCameraMountPort(temp.CameraChannel, 7);
                            break;
                        default:
                            break;
                    }
                    entity.SetFunctionSetting(function.FunctionCode, tasks.ToJson());
                }
                else if (function.FunctionCode == "deviceinfo")//终端信息
                {
                    var temp = (value as DeviceInfoResultDto)?.DeviceInfo;
                    if (temp == null) return Unit.Value;
                    entity.SetPartEnabled($"{nameof(Part)}_1", temp.EnableElectromagneticDoor);
                    entity.SetPartEnabled($"{nameof(Part)}_2", temp.EnablePowerSupplyArrester);
                    entity.SetPartEnabled($"{nameof(Part)}_3", temp.EnableNetworkLightningArrester);
                    entity.SetPartEnabled($"{nameof(Part)}_4", temp.EnableAutomaticReclosing);
                    entity.SetPartEnabled($"{nameof(Part)}_5", temp.EnableWifi);
                    entity.SetPartEnabled($"{nameof(Part)}_6", temp.Enable4G);
                    entity.SetPartEnabled($"{nameof(Part)}_7", temp.GPS.Enable);
                    entity.SetChannelEnabled(1, temp.EnableLEDChannel);
                    entity.SetChannelEnabled(2, temp.EnableVedioChannel);
                    entity.SetChannelEnabled(3, temp.EnableOpticalChannel);
                    entity.SetChannelEnabled(4, temp.EnableHeatingChannel);
                    entity.SetChannelEnabled(5, temp.EnableFanChannel);
                    var num = entity.Channels.Count(t => t.Enabled == true && t.PortNumber < 6);
                    if(num == 1)
                    {
                        entity.SetChannelEnabled(6, false);
                        entity.SetChannelEnabled(7, false);
                        entity.SetTerminalSimpleCode();
                    }
                    else
                    {
                        entity.SetTerminalDefaultCode();
                    }
                    entity.SetModelCode($"{Convert.ToChar(value.Data[4])}.{Convert.ToChar(value.Data[5])}");

                    if (temp.GPS.Enable)
                    {
                        entity.SetLocation(temp.GPS.Latitude, temp.GPS.Longitude, temp.GPS.Enable);
                    }
                    entity.SetFunctionSetting(function.FunctionCode, value.ToJson());
                }
                else if (function.FunctionCode == "threshold")//报警阈值
                {
                    var tasks = function.Setting.ToObj<VATHLimitResultDto>();
                    var temp = value as VATHLimitResultDto;
                    entity.SetSensorUpper($"{nameof(Sensor)}_1", temp.Limit.UpperV);
                    entity.SetSensorUpper($"{nameof(Sensor)}_2", temp.Limit.UpperA);
                    entity.SetSensorUpper($"{nameof(Sensor)}_3", temp.Limit.UpperT);
                    entity.SetSensorUpper($"{nameof(Sensor)}_4", temp.Limit.UpperH);

                    entity.SetSensorLower($"{nameof(Sensor)}_1", temp.Limit.LowerV);
                    entity.SetSensorLower($"{nameof(Sensor)}_2", temp.Limit.LowerA);
                    entity.SetSensorLower($"{nameof(Sensor)}_3", temp.Limit.LowerT);

                    entity.SetFunctionSetting(function.FunctionCode, value.ToJson());
                }
                else if(function.FunctionCode == "position")
                {
                    var tasks = function.Setting.ToObj<LatitudeAndLongitudeResultDto>();
                    var temp = value as LatitudeAndLongitudeResultDto;
                    entity.SetLocation(temp.LatitudeAndLongitude.Latitude, temp.LatitudeAndLongitude.Longitude, true);
                    entity.SetFunctionSetting(function.FunctionCode, value.ToJson());
                }
                else
                {
                    entity.SetFunctionSetting(function.FunctionCode, value.ToJson());
                }
                await _deviceRepository.UnitOfWork.SaveEntitiesAsync();
            }
            return Unit.Value;
        }
    }
}
