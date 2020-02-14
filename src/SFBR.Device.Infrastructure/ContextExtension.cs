using Microsoft.EntityFrameworkCore;
using SFBR.Device.Common.ConfigModel.Enums;
using SFBR.Device.Common.ConfigModel.SkynetTerminal;
using SFBR.Device.Common.ConfigModel.SkynetTerminal.Enums;
using SFBR.Device.Common.ConfigModel.SkynetTerminal.Models;
using SFBR.Device.Common.Tool;
using SFBR.Device.Domain.AggregatesModel.BrandAggregate;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate;
using SFBR.Device.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SFBR.Device.Infrastructure
{
    internal static class ContextExtension
    {
        /// <summary>
        /// 初始化基础数据
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void InitBaseData(this ModelBuilder modelBuilder)
        {
            //TODO:初始化基础数据
            List<Brand> brands = new List<Brand>
            {
                new Brand("海康"),
                new Brand("大华")
            };
            List<DeviceType> deviceTypes = new List<DeviceType>
            {
                new DeviceType(nameof(DeviceCode.Terminal), "1.0", "智维终端", "智维终端")
            };
            List<DeviceTypeChannel> deviceTypeChannels = new List<DeviceTypeChannel>();
            List<DeviceTypeController> deviceTypeControllers = new List<DeviceTypeController>();
            List<DeviceTypeSensor> deviceTypeSensors = new List<DeviceTypeSensor>();
            List<DeviceTypePart> deviceTypeParts = new List<DeviceTypePart>();
            List<DeviceTypeFunction> deviceTypeFunctions = new List<DeviceTypeFunction>();
            List<DeviceTypeAlarm> deviceTypeAlarms = new List<DeviceTypeAlarm>();
            
            foreach (var d in deviceTypes)
            {
                {
                    #region 默认回路
                    deviceTypeChannels.Add(new DeviceTypeChannel(d.Id, 1, "补光端口", PortType.LED, OutPutType.AC, false, 220, true, "1"));
                    deviceTypeChannels.Add(new DeviceTypeChannel(d.Id, 2, "视频端口", PortType.Vedio, OutPutType.AC, false, 220, true, "2"));
                    deviceTypeChannels.Add(new DeviceTypeChannel(d.Id, 3, "光通端口", PortType.Optical, OutPutType.AC, false, 220, true, "3"));
                    deviceTypeChannels.Add(new DeviceTypeChannel(d.Id, 4, "加热端口", PortType.Heating, OutPutType.AC, false, 220, true, "4"));
                    deviceTypeChannels.Add(new DeviceTypeChannel(d.Id, 5, "风扇端口", PortType.Fan, OutPutType.DC, false, 12, true, "5"));
                    deviceTypeChannels.Add(new DeviceTypeChannel(d.Id, 6, "输出一", PortType.Vedio, OutPutType.AC, false, 220, true, "6"));
                    deviceTypeChannels.Add(new DeviceTypeChannel(d.Id, 7, "输出二", PortType.Vedio, OutPutType.AC, false, 220, true, "7"));
                    #endregion
                    #region 控制器
                    deviceTypeControllers.Add(new DeviceTypeController(d.Id, 1, $"{nameof(Controller)}_23",description:"补光开关"));//补光 编号都是协议中的位置，主要是解决第一次加载时默认状态的更新
                    deviceTypeControllers.Add(new DeviceTypeController(d.Id, 2, $"{nameof(Controller)}_21",description:"视频开关"));//摄像机
                    deviceTypeControllers.Add(new DeviceTypeController(d.Id, 3, $"{nameof(Controller)}_22",description:"光通开关"));//光通
                    deviceTypeControllers.Add(new DeviceTypeController(d.Id, 4, $"{nameof(Controller)}_24",description:"加热开关"));//加热
                    deviceTypeControllers.Add(new DeviceTypeController(d.Id, 5, $"{nameof(Controller)}_25",description:"风扇"));//风扇
                    #endregion
                    #region 传感器
                    deviceTypeSensors.Add(new DeviceTypeSensor(d.Id, -1, $"{nameof(Sensor)}_1", "电压传感器", SensorType.Voltage, 260, 180));
                    deviceTypeSensors.Add(new DeviceTypeSensor(d.Id, -1, $"{nameof(Sensor)}_2", "电流传感器", SensorType.Current, 0, 0));
                    deviceTypeSensors.Add(new DeviceTypeSensor(d.Id, -1, $"{nameof(Sensor)}_3", "温度传感器", SensorType.Temperature, 65, -20));
                    deviceTypeSensors.Add(new DeviceTypeSensor(d.Id, -1, $"{nameof(Sensor)}_4", "湿度传感器", SensorType.Humidity, 90, null));
                    #endregion
                    #region 配件
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_1", "门磁", PartType.Necessary, true, 26, true));
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_2", "电源防雷器", PartType.Necessary, true, 27, true));
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_3", "网络防雷器", PartType.Accessory, true, 28, true));
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_4", "重合闸", PartType.Accessory, true, 0, true));
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_5", "WIFI模块", PartType.Accessory, true, 0, true));
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_6", "4G模块", PartType.Accessory, true, 0, true));
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_7", "GPS模块", PartType.Accessory, true, 0, true));
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_8", "市电", PartType.Extend, true, 0, true));
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_9", "自定义电源", PartType.Extend, false, 0, true));
                    deviceTypeParts.Add(new DeviceTypePart(d.Id, -1, $"{nameof(Part)}_10", "UPS", PartType.Extend, true, 0, true));
                    #endregion
                    #region 功能
                    var tasks = new List<SkynetTerminalTaskplanDto>()
                    {
                        new SkynetTerminalTaskplanDto(){ Number = 1},
                        new SkynetTerminalTaskplanDto(){ Number = 2},
                        new SkynetTerminalTaskplanDto(){ Number = 3}
                    };
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, true, "定时任务", "task", "01", "VedioTask,FanTask,HeatingTask,LEDTask,OpticalTask",setting:new List<ChannelTaskPlanResultDto>
                    {
                        new ChannelTaskPlanResultDto( ChannelTypeEnum.Vedio, OperateTypeEnum.Write,tasks),
                        new ChannelTaskPlanResultDto( ChannelTypeEnum.Optical, OperateTypeEnum.Write,tasks),
                        new ChannelTaskPlanResultDto( ChannelTypeEnum.LED, OperateTypeEnum.Write,tasks),
                        new ChannelTaskPlanResultDto( ChannelTypeEnum.Heating, OperateTypeEnum.Write,tasks),
                        new ChannelTaskPlanResultDto( ChannelTypeEnum.Fan, OperateTypeEnum.Write,tasks),
                    }.ToJson(),settingTypeName:typeof(List<ChannelTaskPlanResultDto>).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, false, "挂载端口", "mountPort", "02", "WdVedioAssign,RdVedioAssign",setting:new List<VedioChannelAssignResultDto>()
                    {
                        new VedioChannelAssignResultDto{CameraChannel =1,VedioChannelType = VedioChannelTypeEnum.AC220V},
                        new VedioChannelAssignResultDto{CameraChannel =2,VedioChannelType = VedioChannelTypeEnum.AC220V},
                        new VedioChannelAssignResultDto{CameraChannel =3,VedioChannelType = VedioChannelTypeEnum.AC220V},
                        new VedioChannelAssignResultDto{CameraChannel =4,VedioChannelType = VedioChannelTypeEnum.AC220V},
                        new VedioChannelAssignResultDto{CameraChannel =5,VedioChannelType = VedioChannelTypeEnum.AC220V},
                        new VedioChannelAssignResultDto{CameraChannel =6,VedioChannelType = VedioChannelTypeEnum.AC220V},
                        new VedioChannelAssignResultDto{CameraChannel =7,VedioChannelType = VedioChannelTypeEnum.AC220V},
                        new VedioChannelAssignResultDto{CameraChannel =8,VedioChannelType = VedioChannelTypeEnum.AC220V}
                    }.ToJson(), settingTypeName: typeof(VedioChannelAssignResultDto).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, true, "撤防时间", "defending", "03", "RdDisarmControl,RdDisarmControl", setting: new DisarmResultDto().ToJson(), settingTypeName: typeof(DisarmResultDto).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, true, "报警阈值", "threshold", "04", "RdVATHLimit,WdVATHLimit", setting: new VATHLimitResultDto().ToJson(), settingTypeName: typeof(VATHLimitResultDto).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, true, "设备IP", "deviceip", "05", "RdCameraIP,WdCameraIP", setting:new List<CameraIPResultDto>
                    {
                        new CameraIPResultDto(1,"0.0.0.0",false),
                        new CameraIPResultDto(2,"0.0.0.0",false),
                        new CameraIPResultDto(3,"0.0.0.0",false),
                        new CameraIPResultDto(4,"0.0.0.0",false),
                        new CameraIPResultDto(5,"0.0.0.0",false),
                        new CameraIPResultDto(6,"0.0.0.0",false),
                        new CameraIPResultDto(7,"0.0.0.0",false),
                        new CameraIPResultDto(8,"0.0.0.0",false),
                    }.ToJson(), settingTypeName: typeof(List<CameraIPResultDto>).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, true, "终端时间", "deviceTime", "06", "DeviceTime", setting: new DeviceTimeResultDto().ToJson(), settingTypeName: typeof(DeviceTimeResultDto).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, true, "检测时长", "checkfrequency", "07", "RdCameraFaultCheckTime,WdCameraFaultCheckTime", setting: new CameraFaultCheckTimeResultDto().ToJson(), settingTypeName: typeof(CameraFaultCheckTimeResultDto).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, true, "工作模式", "model", "08", "RdChannelMode", setting: new List<ChannelModeResultDto>
                    {
                        new ChannelModeResultDto(ChannelTypeEnum.Vedio, AutoControlTypeEnum.Enable),
                        new ChannelModeResultDto(ChannelTypeEnum.Optical, AutoControlTypeEnum.Enable),
                        new ChannelModeResultDto(ChannelTypeEnum.LED, AutoControlTypeEnum.Enable),
                        new ChannelModeResultDto(ChannelTypeEnum.Heating, AutoControlTypeEnum.Enable),
                        new ChannelModeResultDto(ChannelTypeEnum.Fan, AutoControlTypeEnum.Enable)
                    }.ToJson(), settingTypeName: typeof(List<ChannelModeResultDto>).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, false, "终端定位", "position", "09", "RdLatitudeAndLongitude", setting: new LatitudeAndLongitudeResultDto().ToJson(), settingTypeName: typeof(LatitudeAndLongitudeResultDto).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, false, "终端信息", "deviceinfo", "10", "RdDeviceInfo", setting: new DeviceInfoResultDto().ToJson(), settingTypeName: typeof(DeviceInfoResultDto).FullName));
                    deviceTypeFunctions.Add(new DeviceTypeFunction(d.Id, false, "回路开关", "onoff", "11", "ChannelStatus", setting: new ChannelControlDto().ToJson(), settingTypeName: typeof(ChannelControlDto).FullName));
                    #endregion
                    #region 警报状态
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_21", "网络通讯报警状态", AlarmFrom.Master, $"{nameof(Terminal)}_1","网络发生故障", enabled: false, isStatistics: false,statusMapDescription: "0网络故障报警解除,1发生网络故障报警"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_22", "1号位摄像机报警状态", AlarmFrom.Load, $"{nameof(Camera)}_1", "设备离线", enabled: true, isStatistics: true, statusMapDescription: "01号位摄像机恢复正常,11号位摄像机离线"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_23", "2号位摄像机报警状态", AlarmFrom.Load, $"{nameof(Camera)}_2", "设备离线", enabled: true, isStatistics: true, statusMapDescription: "02号位摄像机恢复正常,12号位摄像机离线"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_24", "3号位摄像机报警状态", AlarmFrom.Load, $"{nameof(Camera)}_3", "设备离线", enabled: true, isStatistics: true, statusMapDescription: "03号位摄像机恢复正常,13号位摄像机离线"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_25", "4号位摄像机报警状态", AlarmFrom.Load, $"{nameof(Camera)}_4", "设备离线", enabled: true, isStatistics: true, statusMapDescription: "04号位摄像机恢复正常,14号位摄像机离线"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_26", "5号位摄像机报警状态", AlarmFrom.Load, $"{nameof(Camera)}_5", "设备离线", enabled: true, isStatistics: true, statusMapDescription: "05号位摄像机恢复正常,15号位摄像机离线"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_27", "6号位摄像机报警状态", AlarmFrom.Load, $"{nameof(Camera)}_6", "设备离线", enabled: true, isStatistics: true, statusMapDescription: "06号位摄像机恢复正常,16号位摄像机离线"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_28", "7号位摄像机报警状态", AlarmFrom.Load, $"{nameof(Camera)}_7", "设备离线", enabled: true, isStatistics: true, statusMapDescription: "07号位摄像机恢复正常,17号位摄像机离线"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_29", "8号位摄像机报警状态", AlarmFrom.Load, $"{nameof(Camera)}_8", "设备离线", enabled: true, isStatistics: true, statusMapDescription: "08号位摄像机恢复正常,18号位摄像机离线"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_30", "电压报警状态", AlarmFrom.Sensor, $"{nameof(Sensor)}_1", "过欠压警报", enabled: true, isStatistics: false, statusMapDescription: "0电压恢复正常,1电压超过上限值,2电压低于下限值,3电压值偏高,4电压值偏低"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_31", "电流报警状态", AlarmFrom.Sensor, $"{nameof(Sensor)}_2", "电流警报", enabled: true, isStatistics: false, statusMapDescription: "0电流恢复正常,1电流超过上限值,2电流低于下限值"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_32", "温度报警状态", AlarmFrom.Sensor, $"{nameof(Sensor)}_3", "温度警报", enabled: true, isStatistics: false, statusMapDescription: "0温度恢复正常,1温度超过上限值,2温度低于下限值"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_33", "湿度报警状态", AlarmFrom.Sensor, $"{nameof(Sensor)}_4", "湿度过高警报", enabled: true, isStatistics: false, statusMapDescription: "0湿度恢复正常,1湿度超过上限值"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_34", "停电报警状态", AlarmFrom.Part, $"{nameof(Part)}_8", "市电停电警报", enabled: true, isStatistics: true, statusMapDescription: "0市电恢复正常,1发生停电报警"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_35", "开门报警状态", AlarmFrom.Part, $"{nameof(Part)}_1", "布撤防警报", enabled: true, isStatistics: true, statusMapDescription: "0门已关闭,1门被打开"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_36", "电源防雷器报警状态", AlarmFrom.Part, $"{nameof(Part)}_2", "防雷器警报", enabled: true, isStatistics: false, statusMapDescription: "0电源防雷器恢复正常,1电源防雷器发生警报"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_37", "网络防雷器报警状态", AlarmFrom.Part, $"{nameof(Part)}_3", "防雷器警报", enabled: true, isStatistics: false, statusMapDescription: "0网络防雷器恢复正常,1网络防雷器发生警报"));
                    deviceTypeAlarms.Add(new DeviceTypeAlarm(d.Id, $"{d.Code}_{d.Model}_38", "自复位重合闸报警状态", AlarmFrom.Part, $"{nameof(Part)}_4", "其他类型警报", enabled: true, isStatistics: false, statusMapDescription: "0重合闸恢复正常,1自复位重合闸发生异常"));
                    #endregion
                }
            }
            //var devicetypes = Newtonsoft.Json.JsonConvert.SerializeObject(deviceTypes);
            // devicetypes = Newtonsoft.Json.JsonConvert.SerializeObject(deviceTypeChannels);
            // devicetypes = Newtonsoft.Json.JsonConvert.SerializeObject(deviceTypeControllers);
            // devicetypes = Newtonsoft.Json.JsonConvert.SerializeObject(deviceTypeSensors);
            // devicetypes = Newtonsoft.Json.JsonConvert.SerializeObject(deviceTypeParts);
            // devicetypes = Newtonsoft.Json.JsonConvert.SerializeObject(deviceTypeFunctions);
            // devicetypes = Newtonsoft.Json.JsonConvert.SerializeObject(deviceTypeAlarms);

            modelBuilder.Entity<DeviceType>().HasData(deviceTypes.ToArray());
            modelBuilder.Entity<DeviceTypeChannel>().HasData(deviceTypeChannels.ToArray());
            modelBuilder.Entity<DeviceTypeController>().HasData(deviceTypeControllers.ToArray());
            modelBuilder.Entity<DeviceTypeSensor>().HasData(deviceTypeSensors.ToArray());
            modelBuilder.Entity<DeviceTypePart>().HasData(deviceTypeParts.ToArray());
            modelBuilder.Entity<DeviceTypeFunction>().HasData(deviceTypeFunctions.ToArray());
            modelBuilder.Entity<DeviceTypeAlarm>().HasData(deviceTypeAlarms.ToArray());
            modelBuilder.Entity<Brand>().HasData(brands.ToArray());
            modelBuilder.Entity<User>().HasData(new User { Account = "admin", Password = "123456", IsDeveloper = true, Name = "超级管理员", UserType = 0, TentantId = null });
        }

        /// <summary>
        /// 初始化表信息
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void InitTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.AggregatesModel.DeviceAggregate.Device>()
                .HasDiscriminator(b => b.DeviceTypeCode)
                .HasValue<Terminal>(nameof(DeviceCode.Terminal))
                .HasValue<Camera>(nameof(DeviceCode.Camera))
                .HasValue<Router>(nameof(DeviceCode.Router))//后续接入
                .HasValue<NetSwitch>(nameof(DeviceCode.NetSwitch))
                .HasValue<UPS>(nameof(DeviceCode.UPS))
                .HasValue<Load>(nameof(DeviceCode.Fan))
                .HasValue<Load>(nameof(DeviceCode.FillLight))
                .HasValue<Load>(nameof(DeviceCode.Heater))
                .HasValue<Load>(nameof(DeviceCode.Optical));

            modelBuilder.Entity<Terminal>().HasBaseType<Domain.AggregatesModel.DeviceAggregate.Device>();
            modelBuilder.Entity<Router>().HasBaseType<Domain.AggregatesModel.DeviceAggregate.Device>();
            modelBuilder.Entity<NetSwitch>().HasBaseType<Domain.AggregatesModel.DeviceAggregate.Device>();
            modelBuilder.Entity<UPS>().HasBaseType<Domain.AggregatesModel.DeviceAggregate.Device>();
            modelBuilder.Entity<Load>().HasBaseType<Domain.AggregatesModel.DeviceAggregate.Device>();
            modelBuilder.Entity<Camera>().HasBaseType<Load>();


            modelBuilder.Entity<TimedTask>().OwnsOne(b => b.Moment);
        }
    }
}
