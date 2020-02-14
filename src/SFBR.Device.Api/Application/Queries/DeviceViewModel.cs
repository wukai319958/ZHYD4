using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    /// <summary>
    /// 设备详情
    /// </summary>
    public class DeviceModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipNum { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        
        #region 领域字段
        #region 设备上传无需用户输入且不允许修改
        /// <summary>
        /// 设备类型
        /// </summary>
        public  string DeviceTypeCode { get;  set; }
        /// <summary>
        /// 是否为主机（配电监控器会挂载，采集器、遥控器等子设备，这些设备不能简单的划为传感器或者开关）
        /// </summary>
        public bool? IsMaster { get;  set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string ModelCode { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string DeviceIP { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int DevicePort { get; set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServerIP { get; set; }
        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServerPort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 启用（手动输入的设备默认未启用）
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 连接状态: 0 未连接； 1 连接
        /// </summary>
        public int Connection { get; set; }
        #endregion
        #region 用户端输入
        /// <summary>
        /// 本地时间字符串
        /// </summary>
        public string LocalCreationTimeString => CreationTime.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzzz", DateTimeFormatInfo.InvariantInfo);
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 租户
        /// </summary>
        public string TentantId { get; set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get; set; }

        #endregion
        #endregion
        #region 运维信息
        /// <summary>
        /// 运维单位
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 运维人员
        /// </summary>
        public string OprationId { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandId { get; set; }
        /// <summary>
        /// 质保期
        /// </summary>
        public double Warranty { get; set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime? InstallTime { get; set; }
        #endregion

    }

    /// <summary>
    /// 智维终端
    /// </summary>
    public class TerminalDevice : DeviceModel
    {
        /// <summary>
        /// 终端版本 0 标准版 1 简配版
        /// </summary>
        public int TerminalCode { get; set; }
        /// <summary>
        /// 发生警报的数量，大于0即为发生警报
        /// </summary>
        public int AlarmCount { get; set; }
        /// <summary>
        /// 位置信息
        /// </summary>
        public string LocationId { get; set; }
        /// <summary>
        /// 区域id
        /// </summary>
        public string RegionId { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public string RegionCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 纬度（南纬为负数）
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 经度（西经为负数）
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 是否定位
        /// </summary>
        public bool IsPosition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Channel> Channels { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Controller> Controllers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DeviceAlarm> DeviceAlarms { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Part> Parts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Sensor> Sensors { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TimedTask> TimedTasks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Load> Loads { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DeviceFunction> DeviceFunctions { get; set; }
        /// <summary>
        /// 位置信息
        /// </summary>
        public Location Location { get; set; }
        /// <summary>
        /// 区域信息
        /// </summary>
        public Region Region { get; set; }
    }

    /// <summary>
    /// 负载
    /// </summary>
    public class Load:DeviceModel
    {
        /// <summary>
        /// 挂载的端口
        /// </summary>
        public int? PortNumber { get; set; }

    }
    /// <summary>
    /// 摄像机
    /// </summary>
    public class Camera:Load
    {
        /// <summary>
        /// 扩展属性
        /// </summary>
        public List<DeviceProp> DeviceProps { get; set; }
    }
    /// <summary>
    /// 设备回路
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 主机id
        /// </summary>
        public string DeviceId { get; protected set; }
        /// <summary>
        /// 回路编号
        /// </summary>
        public int? PortNumber { get; set; }
        /// <summary>
        /// 输出端口默认名称（不允许用户修改，设备上写的是什么就是什么。用户自定义的是ChannelName）
        /// </summary>
        public string PortDefaultName { get; set; }
        /// <summary>
        /// 端口类型
        /// </summary>
        public PortType PortType { get; private set; }
        /// <summary>
        /// 输出类型：AC 交流电；DC 直流电
        /// </summary>
        public OutPutType? OutputType { get; set; }
        /// <summary>
        /// 三相输出
        /// </summary>
        public bool? OutputThreePhase { get; set; }
        /// <summary>
        /// 输出电压
        /// </summary>
        public double? OutputValue { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? Enabled { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }
    }
    /// <summary>
    /// 设备控制器
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 控制器（开关）编号
        /// </summary>
        public string ControllerCode { get; set; }
        /// <summary>
        /// 控制的回路编号(-1表示主机的控制器)
        /// </summary>
        public int? PortNumber { get; set; }
        /// <summary>
        /// 按钮（英文逗号隔开）
        /// </summary>
        public string Buttons { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? Enabled { get; set; }
        /// <summary>
        /// 控制器当前状态
        /// </summary>
        public string ControllerStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
    /// <summary>
    /// 设备警报及状态
    /// </summary>
    public class DeviceAlarm
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 警报代码（协议中的位置）
        /// </summary>
        public string AlarmCode { get; set; }
        /// <summary>
        /// 警报名称
        /// </summary>
        public string AlarmName { get; set; }
        /// <summary>
        /// 警报分组
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 警报类型：持续性警报（温度过高）、瞬时警报（打火）
        /// </summary>
        public int AlarmType { get; set; }
        /// <summary>
        /// 警报来源：0 监控终端；1 回路；2 负载(一般只有离线、在线状态)；3 配件 ...
        /// 不同的设备警报的来源可能不一样，比如监控终端
        /// </summary>
        public int AlarmFrom { get; set; }
        /// <summary>
        /// 警报级别
        /// </summary>
        public int AlarmLevel { get; set; }
        /// <summary>
        /// 是否启用该警报
        /// </summary>
        public bool Enabled { get; set; }
      
        /// <summary>
        /// 当前状态值
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 正常值
        /// </summary>
        public string NormalValue { get; set; }
        /// <summary>
        /// 是否发生警报
        /// </summary>
        public bool IsAlarm => !((NormalValue??"").Split(',').Contains(Status));
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AlarmTime { get; set; }
        /// <summary>
        /// 修复时长
        /// </summary>
        public double? RepairTime { get; set; }
        /// <summary>
        /// 警报对象编号
        /// </summary>
        public string TargetCode { get; set; }
        #region 发送信息
        /// <summary>
        /// 发生警报时发送的模板
        /// </summary>
        public string AlarmingDescription { get; private set; }
        /// <summary>
        /// 解除警报时发送的模板
        /// </summary>
        public string AlarmedDescription { get; private set; }
        /// <summary>
        /// 报警时发送短信
        /// </summary>
        public bool SendAlarmingMessage { get; private set; }
        /// <summary>
        /// 解除警报时发送短信
        /// </summary>
        public bool SendAlarmedMessage { get; private set; }
        /// <summary>
        /// 报警时拨打语音电话
        /// </summary>
        public bool CallAlarmingPhone { get; private set; }
        /// <summary>
        /// 解除报警时拨打语音电话
        /// </summary>
        public bool CallAlarmedPhone { get; private set; }
        /// <summary>
        /// 报警时发送邮件
        /// </summary>
        public bool SendAlarmingEmail { get; private set; }
        /// <summary>
        /// 解除警报时发送邮件
        /// </summary>
        public bool SendAlarmedEmail { get; private set; }
        #endregion
    }
    /// <summary>
    /// 设备功能及配置内容
    /// </summary>
    public class DeviceFunction
    {
        
        /// <summary>
        /// 
        /// </summary>
        public string DeviceId { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string FunctionCode { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; private set; }
        /// <summary>
        /// 指令接收对象(-1表示主机)
        /// </summary>
        public int? PortNumber { get; private set; }
        /// <summary>
        /// 设置的值
        /// </summary>
        public string Setting { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? Enabled { get; private set; }
        /// <summary>
        /// 锁定配置，防止全局配置时覆盖值
        /// </summary>
        public bool LockSetting { get; private set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; private set; }
    }
    /// <summary>
    /// 设备配件
    /// </summary>
    public class Part
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        public string DeviceId { get; private set; }
        /// <summary>
        /// 回路地址（-1表示主机）
        /// </summary>
        public int? PortNumber { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string PartCode { get; private set; }
        /// <summary>
        /// 配件名称
        /// </summary>
        public string PartName { get; private set; }
        /// <summary>
        /// 配件类型
        /// </summary>
        public PartType PartType { get; private set; }//配件类型由输出端口决定
        /// <summary>
        /// 拥有状态
        /// </summary>
        public bool HasStatus { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>                                   
        public bool? Enabled { get; private set; }
        /// <summary>
        /// 开关量状态
        /// </summary>
        public string Status { get; private set; }
        /// <summary>
        /// 警报状态
        /// </summary>
        public string AlarmStatus { get; private set; }
        #region 运维信息
        /// <summary>
        /// 运维单位
        /// </summary>
        public string CompanyId { get; private set; }
        /// <summary>
        /// 运维人员
        /// </summary>
        public string OprationId { get; private set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandId { get; private set; }
        /// <summary>
        /// 质保期
        /// </summary>
        public double? Warranty { get; private set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime? InstallTime { get; private set; }
        #endregion
    }
    /// <summary>
    /// 设备传感器
    /// </summary>
    public class Sensor
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public string DeviceId { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string SensorCode { get; set; }
        /// <summary>
        /// 传感器名称
        /// </summary>
        public string SensorName { get; private set; }
        /// <summary>
        /// 传感器类型 
        /// </summary>
        public SensorType SensorType { get; private set; }
        /// <summary>
        /// 控制器的端口(-1表示主机的控制器)
        /// </summary>
        public int? PortNumber { get; private set; }
        /// <summary>
        /// 警报状态
        /// </summary>
        public string AlarmStatus { get; private set; }
        /// <summary>
        /// 模拟量上限
        /// </summary>
        public double? UpperValue { get; private set; }
        /// <summary>
        /// 模拟量下限
        /// </summary>
        public double? LowerValue { get; private set; }
        /// <summary>
        /// 模拟量
        /// </summary>
        public double? RealValue { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? Enabled { get; private set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }
    }
    /// <summary>
    /// 设备定时任务
    /// </summary>
    public class TimedTask
    {
        /// <summary>
        /// 
        /// </summary>
        public string DeviceId { get; private set; }
        /// <summary>
        /// 回路编号
        /// </summary>
        public int PortNumber { get; private set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        public string TaskId { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 定时类型
        /// </summary>
        public ExecType ExecType { get; private set; }
        /// <summary>
        /// 执行的动作
        /// </summary>
        public ExecAction ExecAction { get; private set; }
        /// <summary>
        /// 是否已执行（只有只执行一次的才有该状态）
        /// </summary>
        public int Executed { get; private set; }
        /// <summary>
        /// 循环周期类型
        /// </summary>
        public LoopType LoopType { get; private set; }
        /// <summary>
        /// 执行的时刻
        /// </summary>
        public Moment Moment { get; private set; }
        /// <summary>
        /// 周期内参与循环的时间（比如，每周一执行或者每月的1号 10 20号执行,多个由英文逗号隔开）
        /// </summary>
        public string LoopMonent { get; private set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; private set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class DeviceProp
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 负载id
        /// </summary>
        public string DeviceId { get; private set; }
        /// <summary>
        /// 属性名称（前端绑定的变量）
        /// </summary>
        public string PropName { get; private set; }
        /// <summary>
        /// 属性显示文本（前端提示文本）
        /// </summary>
        public string PropText { get; private set; }
        /// <summary>
        /// 属性类型（默认字符串）
        /// </summary>
        public string PropType { get; private set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; private set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string PropValue { get; private set; }
        /// <summary>
        /// 是否可以删除（某些固定属性不允许删除）
        /// </summary>
        public bool CanRemove { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Moment 
    {
       /// <summary>
       /// 月
       /// </summary>
        public int Month { get; private set; }
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; private set; }
        /// <summary>
        /// 时
        /// </summary>
        public int Hour { get; private set; }
        /// <summary>
        /// 分
        /// </summary>
        public int Minute { get; private set; }
        /// <summary>
        /// 秒
        /// </summary>
        public int Second { get; private set; }

       
    }
    /// <summary>
    /// 位置信息
    /// </summary>
    public class Location
    {
        /// <summary>
        /// 纬度（南纬为负数）
        /// </summary>
        public double Latitude { get; private set; }
        /// <summary>
        /// 经度（西经为负数）
        /// </summary>
        public double Longitude { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; private set; }
    }
    /// <summary>
    /// 区域
    /// </summary>
    public class Region
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 区域自定义代码
        /// </summary>
        public string RegionCode { get; private set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; private set; }
        /// <summary>
        /// 上级区域
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 租户
        /// </summary>
        public string TentantId { get; protected set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get; private set; }
    }
}
