using SFBR.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.IntegrationEvents.Events
{
    /// <summary>
    /// 发生警报
    /// </summary>
    public class TerminalRaiseAlarmIntegrationEvent : IntegrationEvent
    {
        #region 站点信息
        /// <summary>
        /// 站点id
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeCode { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string ModelCode { get; set; }
        /// <summary>
        /// 站点编号
        /// </summary>
        public string EquipNum { get; set; }
        /// <summary>
        /// 站点分配的区域
        /// </summary>
        public string RegionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegionCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public string TentantId { get; set; }
        /// <summary>
        /// 租户姓名
        /// </summary>
        public string TentantName { get; set; }
        /// <summary>
        /// 上一级设备
        /// </summary>
        public string ParentId { get; set; }
        #endregion

        /// <summary>
        /// 警报消息
        /// </summary>
        public Alarm Alarm { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Alarm
    {
        #region 警报信息
        /// <summary>
        /// 日志编号
        /// </summary>
        public string LogId { get; set; }
        /// <summary>
        /// 警报编号
        /// </summary>
        public string AlarmCode { get; set; }
        /// <summary>
        /// 报警名称
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
        /// 传感器、负载编号或者其他划分的子设备编号
        /// </summary>
        public string TargetCode { get; set; }
        /// <summary>
        /// 警报级别
        /// </summary>
        public int AlarmLevel { get; set; }
        /// <summary>
        /// 警报状态
        /// </summary>
        public string AlarmStatus { get; set; }
        /// <summary>
        /// 发生警报时发送的消息
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 规定修复时长
        /// </summary>
        public double RepairTime { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime AlarmTime { get; set; }
        /// <summary>
        /// 是否参与统计
        /// </summary>
        public bool IsStatistics { get; set; }
        /// <summary>
        /// 实时值（不是所有的警报都有）
        /// </summary>
        public double? RealData { get; set; }
        /// <summary>
        /// 报警上限
        /// </summary>
        public double? UpperLimit { get; set; }
        /// <summary>
        /// 报警下限
        /// </summary>
        public double? LowerLimit { get; set; }
        #endregion
    }

}

