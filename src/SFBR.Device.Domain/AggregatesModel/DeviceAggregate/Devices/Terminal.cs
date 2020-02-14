using SFBR.Device.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 智维终端（主设备当做一个站点）
    /// </summary>
    public class Terminal : Device
    {
        /******************
         * 1.回路也好/功能信息也好，根据具体的设备来扩展。device只有基本的设备信息，比如ip，端口，编号等
         * 2.配件默认都是简单的，只有完善信息后才能看到详情
         * 3.终端是否支持4G/Wifi/GPS，可以从DevFunction的协议中读取
         * 4.刷新实时数据时根据
         * ******************/
        ///// <summary>
        ///// 运维信息
        ///// </summary>
        //[StringLength(50)]
        //public string DeviceAOMId { get;private set; }
        /// <summary>
        /// 自定义分组
        /// </summary>
        [StringLength(50)]
        public string RegionId { get; private set; }

        //public virtual Region Region { get; set; }
        /// <summary>
        /// 位置信息（可能有多个主机放在同一个位置组成一个站点）
        /// </summary>
        [StringLength(50)]
        public string LocationId { get; private set; }
        /// <summary>
        /// 子型号 default simple
        /// </summary>
        [StringLength(50)]
        public TerminalCode TerminalCode { get; set; }

        public virtual Location Location { get; private set; }

        /// <summary>
        /// 输出回路
        /// </summary>
        private readonly List<Channel> _channels;
        /// <summary>
        /// 配件
        /// </summary>
        private readonly List<Part> _parts;
        /// <summary>
        /// 功能配置
        /// </summary>
        private readonly List<DeviceFunction> _deviceFunctions;
        /// <summary>
        /// 定时任务（有开关才会有定时任务）
        /// </summary>
        private readonly List<TimedTask> _timedTasks;
        /// <summary>
        /// 控制器（一般指开关）
        /// </summary>
        private readonly List<Controller> _controllers;
        /// <summary>
        /// 传感器（模拟量）
        /// </summary>
        private readonly List<Sensor> _sensors;
        /// <summary>
        /// 警报状态
        /// </summary>
        private readonly List<DeviceAlarm> _deviceAlarms;
        /// <summary>
        /// 负载集合
        /// </summary>
        private readonly List<Load> _loads;



        /// <summary>
        /// 传感器（抽象的并非实际存在，比如用电量等）
        /// </summary>
        public virtual IReadOnlyCollection<Part> Parts => _parts;

        /// <summary>
        /// 控制开关
        /// </summary>
        public virtual IReadOnlyCollection<DeviceFunction> DevFunctions => _deviceFunctions;

        /// <summary>
        /// 定时任务
        /// </summary>
        public virtual IReadOnlyCollection<TimedTask> TimedTasks => _timedTasks;
        /// <summary>
        /// 输出回路
        /// </summary>
        public virtual IReadOnlyCollection<Channel> Channels => _channels;
        /// <summary>
        /// 控制器
        /// </summary>
        public virtual IReadOnlyCollection<Controller> Controllers => _controllers;
        /// <summary>
        /// 传感器
        /// </summary>
        public virtual IReadOnlyCollection<Sensor> Sensors => _sensors;
        /// <summary>
        /// 警报器
        /// </summary>
        public virtual IReadOnlyCollection<DeviceAlarm> DeviceAlarms => _deviceAlarms;

        public virtual IReadOnlyCollection<Load> Loads => _loads;

        public Terminal(string tentantId, string deviceName, string equipNum, bool enabled = false, string modelCode = null, string deviceIP = null, int devicePort = 0, string serverIP = null, int serverPort = 0, string description = null, string parentId = null, int connection = 0, string regionId = null, string locationId = null)
            : base(tentantId, deviceName, equipNum, nameof(Terminal), enabled, modelCode, deviceIP, devicePort, serverIP, serverPort, description, parentId, connection, null)
        {
            _channels = new List<Channel>();
            _deviceFunctions = new List<DeviceFunction>();
            _parts = new List<Part>();
            _timedTasks = new List<TimedTask>();
            _sensors = new List<Sensor>();
            _controllers = new List<Controller>();
            _deviceAlarms = new List<DeviceAlarm>();
            _loads = new List<Load>();

            RegionId = regionId;
            LocationId = locationId;
            //添加设备事件，1.用于移除被发现的设备列表中缓存；2.添加负载
            AddDomainEvent(new CreateDeviceDomainEvent(this));
        }
        public override void SetConnetion(int connection)
        {
            if (Connection != connection)
            {
                Connection = connection;
                if (connection == 0)
                {
                    AddDomainEvent(new DeviceOffLineDomainEvent(this));
                }
                else
                {
                    AddDomainEvent(new DeviceOnlineDomainEvent(this));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetTerminalDefaultCode()
        {
            TerminalCode = TerminalCode.Default;
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetTerminalSimpleCode()
        {
            TerminalCode = TerminalCode.Simple;
        }
        /// <summary>
        /// 添加回路(自动识别不会手动添加)
        /// </summary>
        public void AddChannel(string deviceId, int portNumber, DeviceTypeAggregate.PortType portType, string portDefaultName = null, DeviceTypeAggregate.OutPutType? outputType = null, bool? outputThreePhase = null, double? outputVoltage = null, bool enabled = true, string description = "", string sort = null)
        {
            var channel = _channels.Where(where => where.PortNumber == portNumber).SingleOrDefault();
            if (channel == null)
            {
                _channels.Add(new Channel(deviceId, portNumber, portType, portDefaultName, outputType, outputThreePhase, outputVoltage, enabled, description, sort));
            }
        }

        public void AddController(string deviceId, string controllerCode, int? portNumber = null, string buttons = null, bool? enabled = null, string controllerStatus = null, string description = null)
        {
            var controller = Controllers.Where(where => where.ControllerCode == controllerCode).SingleOrDefault();
            if (controller == null)
            {
                _controllers.Add(new Controller(deviceId, controllerCode, portNumber, buttons, enabled, controllerStatus, description));
            }
        }

        /// <summary>
        /// 添加传感器
        /// </summary>
        public void AddSensor(string deviceId, string sensorCode, string alarmStatus = "0", int? portNumber = null, double? upperValue = null, double? lowerValue = null, double? realValue = null, bool? enabled = null, string description = "")
        {
            var sensor = _sensors.Where(where => where.SensorCode == sensorCode).SingleOrDefault();
            if (sensor == null)
            {
                _sensors.Add(new Sensor(deviceId, sensorCode, alarmStatus, portNumber, upperValue, lowerValue, realValue, enabled, description));
            }
        }
        /// <summary>
        /// 配件
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="partCode"></param>
        /// <param name="portNumber"></param>
        /// <param name="status"></param>
        /// <param name="alarmStatus"></param>
        /// <param name="enabled"></param>
        public void AddPart(string deviceId, string partCode, int? portNumber = null, string status = null, string alarmStatus = null, bool enabled = false)
        {
            var part = _parts.Where(where => where.PartCode == partCode).SingleOrDefault();
            if (part == null)
            {
                _parts.Add(new Part(deviceId, partCode, portNumber, status, alarmStatus, enabled));
            }
        }

        public void AddAlarm(string deviceId, string alarmCode, string targetCode, string normalValue, string status = "0", DateTime? alarmTime = null, double? repairTime = null, bool? enabled = null)
        {
            var alarm = _deviceAlarms.Where(where => where.AlarmCode == alarmCode).SingleOrDefault();
            if (alarm == null)
            {
                _deviceAlarms.Add(new DeviceAlarm(deviceId, alarmCode, targetCode, normalValue, status, alarmTime, repairTime, enabled));
            }
        }
        /// <summary>
        /// 添加设备功能
        /// </summary>
        public void AddFunction(string deviceId, string functionCode, string callbackCodes, string settingTypeName, int? portNumber = null, string setting = null, bool? enabled = null, bool lockSetting = false)
        {
            var function = _deviceFunctions.Where(where => where.FunctionCode == functionCode).SingleOrDefault();
            if (function == null)
            {
                _deviceFunctions.Add(new DeviceFunction(deviceId, functionCode, callbackCodes, settingTypeName, portNumber, setting, enabled, lockSetting));
            }
        }
        /// <summary>
        /// 添加定时任务
        /// </summary>
        public void AddTimedTask(string deviceId, int portNumber, bool enable, string taskId, ExecType execType = ExecType.Loop, ExecAction execAction = ExecAction.On, int executed = 0, LoopType loopType = LoopType.Day, Moment moment = null, string loopMonent = null)
        {
            if (moment == null) moment = new Moment(0, 0, 0, 0, 0);
            _timedTasks.Add(new TimedTask(deviceId, portNumber, enable, taskId, execType, execAction, executed, loopType, moment, loopMonent))
;
        }
        /// <summary>
        /// 添加负载
        /// </summary>
        public void AddLoad(string tentantId, string deviceName, string equipNum, string deviceTypeCode, int? portNumber = null, bool enabled = false, string modelCode = null, string deviceIP = null, int devicePort = 0, string serverIP = null, int serverPort = 0, string description = null, string parentId = null, int connection = 0)
        {
            if (deviceTypeCode == nameof(DeviceCode.Camera))
            {
                _loads.Add(new Camera(tentantId, deviceName, equipNum, enabled, modelCode, deviceIP, devicePort, serverIP, serverPort, description, parentId, connection));
            }
            else
            {
                _loads.Add(new Load(tentantId, deviceName, equipNum, deviceTypeCode, portNumber, enabled, modelCode, deviceIP, devicePort, serverIP, serverPort, description, parentId, connection));
            }
        }



        /// <summary>
        /// 设置经纬度同时自动根据经纬度添加区域
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="enabled"></param>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <param name="district"></param>
        public void SetLocation(double latitude, double longitude, bool enabled, string province, string city, string district)
        {
            if (Location != null)
            {
                Location.SetLatitude(latitude);
                Location.SetLongitude(longitude);
                Location.Regionalism(province, city, district);
            }
            else
            {
                //TODO:发布位置变更事件
                Location = new Location(0, latitude, longitude, province: province, city: city, district: district, null, null, true, null);
                LocationId = Location.Id;
            }

        }
        /// <summary>
        /// 设置警报状态
        /// </summary>
        /// <param name="alarmCode"></param>
        /// <param name="status"></param>
        /// <param name="targetCode">报警对象编号</param>
        public void SetAlarmStatus(string alarmCode, string status, string targetCode)
        {
            var alarmItem = _deviceAlarms.FirstOrDefault(where => where.AlarmCode == alarmCode);
            if (alarmItem == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的警报不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }

            if (alarmItem.Status != status)
            {
                string oldStatus = alarmItem.Status;
                alarmItem.SetStatus(status);
                AddDomainEvent(new DeviceAlarmStatusChangeDomainEvent(this, alarmCode, oldStatus, targetCode));
            }
        }

        /// <summary>
        /// 设置开关状态
        /// </summary>
        /// <param name="controllerCode"></param>
        /// <param name="status"></param>
        public void SetControllerStatus(string controllerCode, string status)
        {
            var controller = _controllers.FirstOrDefault(where => where.ControllerCode == controllerCode);
            if (controller == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的开关不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (controller.ControllerStatus != status)
            {
                controller.SetControllerStatus(status);
            }
        }

        /// <summary>
        /// 功能配置（注意，定时任务是独立的）
        /// </summary>
        /// <param name="functionCode"></param>
        /// <param name="setting"></param>
        public void SetFunctionSetting(string functionCode, string setting)
        {
            var function = _deviceFunctions.FirstOrDefault(where => where.FunctionCode == functionCode);
            if (function == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的功能不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (function.Setting != setting)
            {
                function.SetSetting(setting);
            }
        }

        public void SetFunctionLockSetting(string functionCode, bool setting)
        {
            var function = _deviceFunctions.FirstOrDefault(where => where.FunctionCode == functionCode);
            if (function == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的功能不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (function.LockSetting != setting)
            {
                function.SetLockSetting(setting);
            }
        }


        public void SetPartStatus(string partCode, string status)
        {
            var part = _parts.FirstOrDefault(where => where.PartCode == partCode);
            if (part == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的配件不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (part.Status != status)
            {
                part.SetStatus(status);
            }
        }

        public void SetPartEnabled(string partCode, bool enabled)
        {
            var part = _parts.FirstOrDefault(where => where.PartCode == partCode);
            if (part == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的配件不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (part.Enabled != enabled)
            {
                part.SetEnabled(enabled);
            }
        }

        public void SetChannelEnabled(int portNumber, bool enabled)
        {
            var channel = _channels.FirstOrDefault(where => where.PortNumber == portNumber);
            if (channel == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的回路不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (channel.Enabled != enabled)
            {
                channel.SetEnabled(enabled);
            }
        }

        public void SetChannelName(int portNumber, string name)
        {
            var channel = _channels.FirstOrDefault(where => where.PortNumber == portNumber);
            if (channel == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的回路不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (channel.PortDefaultName != name)
            {
                channel.SetName(name);
            }
        }

        public void SetChannelVoltage(int portNumber, double voltage)
        {
            var channel = _channels.FirstOrDefault(where => where.PortNumber == portNumber);
            if (channel == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的回路不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (channel.OutputValue != voltage)
            {
                channel.SetOutPutValue(voltage);
            }
        }


        public void SetPartAlarmStatus(string partCode, string alarmStatus)
        {
            var part = _parts.FirstOrDefault(where => where.PartCode == partCode);
            if (part == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的配件不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (part.AlarmStatus != alarmStatus)
            {
                part.SetAlarmStatus(alarmStatus);
            }
        }

        public void SetSensorAlarmStatus(string sensorCode, string alarmStatus)
        {
            var sensor = _sensors.FirstOrDefault(where => where.SensorCode == sensorCode);
            if (sensor == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的传感器不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (sensor.AlarmStatus != alarmStatus)
            {
                sensor.SetAlarmStatus(alarmStatus);
            }
        }

        public void SetCameraMountPort(int cameraIndex, int portNumber)
        {
            var camera = _loads.FirstOrDefault(where => where.DeviceTypeCode == nameof(Camera) && where.EquipNum.EndsWith(cameraIndex.ToString()));
            if (camera == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的设备不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (camera.PortNumber != portNumber)
            {
                camera.SetPortNumber(portNumber);
            }
        }
        public void SetCameraIP(int cameraIndex, string ip)
        {
            var camera = _loads.FirstOrDefault(where => where.DeviceTypeCode == nameof(Camera) && where.EquipNum.EndsWith(cameraIndex.ToString()));
            if (camera == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的设备不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (camera.DeviceIP != ip)
            {
                camera.SetAddress(ip, 0);
            }
        }
        public void SetCameraEnabled(int cameraIndex, bool enabled)
        {
            var camera = _loads.FirstOrDefault(where => where.DeviceTypeCode == nameof(Camera) && where.EquipNum.EndsWith(cameraIndex.ToString()));
            if (camera == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的设备不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (camera.Enabled != enabled)
            {
                camera.SetEnabled(enabled);
            }
        }

        public void SetSensorUpper(string sensorCode, double? value)
        {
            var sensor = _sensors.FirstOrDefault(where => where.SensorCode == sensorCode);
            if (sensor == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的传感器不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (sensor.UpperValue != value)
            {
                sensor.SetUpperValue(value);
            }
        }

        public void SetSensorLower(string sensorCode, double? value)
        {
            var sensor = _sensors.FirstOrDefault(where => where.SensorCode == sensorCode);
            if (sensor == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的传感器不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (sensor.LowerValue != value)
            {
                sensor.SetLowerValue(value);
            }
        }

        public void SetLoadName(string loadId, string name)
        {
            var load = _loads.FirstOrDefault(where => where.Id == loadId);
            if (load == null)
            {
                throw new Exceptions.DeviceDomainException("该编号的传感器不存在！");//FIXME:应该调用资源文件中的配置，用以全球化
            }
            if (load.DeviceName != name)
            {
                load.SetName(name);
            }
        }

        public void SetRegionId(string regionId)
        {
            if (RegionId != regionId)
            {
                RegionId = regionId;
            }
        }

    }
    /// <summary>
    /// 子型号
    /// </summary>
    public enum TerminalCode
    {
        /// <summary>
        /// 标准版
        /// </summary>
        Default = 0,
        /// <summary>
        /// 简配版
        /// </summary>
        Simple = 1
    }
}
