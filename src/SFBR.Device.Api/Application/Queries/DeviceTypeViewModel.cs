using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    /// <summary>
    /// 某类设备的公共详情
    /// </summary>
    public class DeviceType
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 编号（协议中的设备编号）
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 型号（协议的版本号）
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分组key
        /// </summary>
        public string GroupKey { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 自动创建
        /// </summary>
        public bool AutoCreate { get; set; }

        #region 默认运维信息
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
        #endregion
        /// <summary>
        /// 该类设备拥有的警报
        /// </summary>
        public List<DeviceTypeAlarm> Alarms { get; set; }
        /// <summary>
        /// 该类型设备拥有的功能
        /// </summary>
        public List<DeviceTypeFunction> Functions { get; set; }
        /// <summary>
        /// 默认回路
        /// </summary>
        public List<DeviceTypeChannel> Channels { get; set; }
        /// <summary>
        /// 默认配件
        /// </summary>
        public List<DeviceTypePart> Parts { get; set; }
        /// <summary>
        /// 默认控制器
        /// </summary>
        public List<DeviceTypeController> Controllers { get; set; }
        /// <summary>
        /// 默认传感器
        /// </summary>
        public List<DeviceTypeSensor> Sensors { get; set; }
    }
  
    /// <summary>
    /// 该类型设备可能产生的警报
    /// </summary>
    public class DeviceTypeAlarm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        #region 基本属性
        /// <summary>
        /// 
        /// </summary>
        public string DeviceTypeId { get;set; }        
        /// <summary>
        /// 警报代码（协议解析的位置）
        /// </summary>
        public string AlarmCode { get; set; }
        /// <summary>
        /// 警报名称
        /// </summary>
        public string AlarmName { get; set; }
        /// <summary>
        /// 警报类型：持续性警报（温度过高）、瞬时警报（打火）
        /// </summary>
        public int AlarmType { get;set; }
        /// <summary>
        /// 警报来源：0 监控终端；1 回路；2 负载(一般只有离线、在线状态)；3 配件 ...
        /// </summary>
        public int AlarmFrom { get;set; }
        /// <summary>
        /// 警报级别
        /// </summary>
        public int AlarmLevel { get;set; }
        /// <summary>
        /// 警报分组（温度警报、电压警报、电流警报等等）
        /// </summary>
        public string GroupName { get;set; }
        /// <summary>
        /// 是否启用该警报
        /// </summary>
        public bool Enabled { get;set; }
        /// <summary>
        /// 当前值
        /// </summary>
        public string Status { get;set; }
        /// <summary>
        /// 正常状态
        /// </summary>
        public string NormalValue { get;set; }
        /// <summary>
        /// 
        /// </summary>
        public string TargetCode { get; set; }
        #endregion
        #region 业务属性
        /// <summary>
        /// 发生警报时发送的模板
        /// </summary>
        public string AlarmingDescription { get;set; }
        /// <summary>
        /// 解除警报时发送的模板
        /// </summary>
        public string AlarmedDescription { get;set; }
        /// <summary>
        /// 报警时发送短信
        /// </summary>
        public bool SendAlarmingMessage { get;set; }
        /// <summary>
        /// 解除警报时发送短信
        /// </summary>
        public bool SendAlarmedMessage { get;set; }
        /// <summary>
        /// 报警时拨打语音电话
        /// </summary>
        public bool CallAlarmingPhone { get;set; }
        /// <summary>
        /// 解除报警时拨打语音电话
        /// </summary>
        public bool CallAlarmedPhone { get;set; }
        /// <summary>
        /// 报警时发送邮件
        /// </summary>
        public bool SendAlarmingEmail { get;set; }
        /// <summary>
        /// 解除警报时发送邮件
        /// </summary>
        public bool SendAlarmedEmail { get;set; }
        /// <summary>
        /// 自动派单
        /// </summary>
        public bool AutoSendOrder { get;set; }
        /// <summary>
        /// 是否参与统计
        /// </summary>
        public bool IsStatistics { get;set; }
        /// <summary>
        /// 默认修复时长
        /// </summary>
        public double RepairTime { get;set; }
        #endregion
    }
    /// <summary>
    /// 该类型设备的功能
    /// </summary>
    public class DeviceTypeFunction
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeId { get;set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get;set; }
        /// <summary>
        /// 功能编号（内部自定义）
        /// </summary>
        public string FunctionCode { get;set; }
        ///// <summary>
        ///// 读取/写入
        ///// </summary>
        public Domain.AggregatesModel.DeviceTypeAggregate.FunctionType FunctionType { get;set; }//功能模块
        /// <summary>
        /// 备注描述（帮助描述）
        /// </summary>
        public string Description { get;set; }
        /// <summary>
        /// 默认设置的值
        /// </summary>
        public string Setting { get; private set; }
        /// <summary>
        /// 值的类型
        /// </summary>
        public string SettingTypeName { get; set; }
        /// <summary>
        /// 一个功能会有多个命令码
        /// </summary>
        public string CallbackCodes { get; set; }
    }
    /// <summary>
    /// 该类型设备拥有的回路
    /// </summary>
    public class DeviceTypeChannel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeId { get;set; }
        /// <summary>
        /// 回路编号
        /// </summary>
        public int PortNumber { get;set; }
        /// <summary>
        /// 输出端口默认名称
        /// </summary>
        public string PortDefaultName { get;set; }
        /// <summary>
        /// 端口类型。0：电源 1：电源兼网口 2：网口 
        /// </summary>
        public Domain.AggregatesModel.DeviceTypeAggregate.PortType PortType { get;set; }
        /// <summary>
        /// 输出类型：AC 交流电；DC 直流电
        /// </summary>
        public Domain.AggregatesModel.DeviceTypeAggregate.OutPutType OutputType { get;set; }
        /// <summary>
        /// 三相输出
        /// </summary>
        public bool OutputThreePhase { get;set; }
        /// <summary>
        /// 输出电压
        /// </summary>
        public double OutputValue { get;set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get;set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }
    }
    /// <summary>
    /// 该类型设备可能拥有的各类配件
    /// </summary>
    public class DeviceTypePart
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeId { get;set; }
        /// <summary>
        /// 控制器的端口(-1表示主机的控制器)
        /// </summary>
        public int PortNumber { get;set; }
        /// <summary>
        /// 配件代码 
        /// </summary>
        public string PartCode { get;set; }
        /// <summary>
        /// 配件名称
        /// </summary>
        public string PartName { get;set; }
        /// <summary>
        /// 配件类型
        /// </summary>
        public Domain.AggregatesModel.DeviceTypeAggregate.PartType PartType { get;set; }//配件类型由输出端口决定
        /// <summary>
        /// 拥有状态
        /// </summary>
        public bool HasStatus { get; set; }
        /// <summary>
        /// 协议中的下标
        /// </summary>
        public int StatusIndex { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get;set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get;set; }
        #region 默认运维信息
        /// <summary>
        /// 运维单位
        /// </summary>
        public string CompanyId { get;set; }
        /// <summary>
        /// 运维人员
        /// </summary>
        public string OprationId { get;set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandId { get;set; }
        /// <summary>
        /// 质保期
        /// </summary>
        public double Warranty { get;set; }
        #endregion
    }
    /// <summary>
    /// 该类型设备拥有的控制器
    /// </summary>
    public class DeviceTypeController
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeId { get;set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string ControllerCode { get;set; }
        /// <summary>
        /// 控制器的端口(-1表示主机的控制器)
        /// </summary>
        public int PortNumber { get;set; }
        /// <summary>
        /// 控制器类型
        /// </summary>
        public Domain.AggregatesModel.DeviceTypeAggregate.ControllerType ControllerType { get;set; }
        /// <summary>
        /// 按钮（英文逗号隔开）
        /// </summary>
        public string Buttons { get;set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get;set; }
        /// <summary>
        /// 控制器默认状态
        /// </summary>
        public string ControllerStatus { get;set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
    /// <summary>
    /// 该类型设备拥有的传感器
    /// </summary>
    public class DeviceTypeSensor
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeId { get;set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string SensorCode { get;set; }
        /// <summary>
        /// 控制器的端口(-1表示主机的控制器)
        /// </summary>
        public int PortNumber { get;set; }
        /// <summary>
        /// 传感器名称
        /// </summary>
        public string SensorName { get;set; }
        /// <summary>
        /// 配件代码 
        /// </summary>
        public Domain.AggregatesModel.DeviceTypeAggregate.SensorType SensorType { get;set; }
        /// <summary>
        /// 模拟量上限
        /// </summary>
        public double? UpperValue { get;set; }
        /// <summary>
        /// 模拟量下限
        /// </summary>
        public double? LowerValue { get;set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get;set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get;set; }
    }
}
