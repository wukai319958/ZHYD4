using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate
{
    /// <summary>
    /// 设备对应的警报（静态数据）
    /// </summary>
    public class DeviceTypeAlarm:SeedWork.Entity
    {
        protected DeviceTypeAlarm()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceTypeAlarm(string deviceTypeId, string alarmCode, string alarmName, AlarmFrom alarmFrom,string targetCode , string groupName = null, int alarmType = 0, int alarmLevel = 0, bool enabled = true, string normalValue="0,5", string alarmingDescription = null, string alarmedDescription = null, bool sendAlarmingMessage = false, bool sendAlarmedMessage = false, bool callAlarmingPhone = false, bool callAlarmedPhone = false, bool sendAlarmingEmail = false, bool sendAlarmedEmail = false, bool autoSendOrder = false, bool isStatistics = true, double repairTime = 0,string statusMapDescription = null)
            :this()
        {
            DeviceTypeId = deviceTypeId ?? throw new ArgumentNullException(nameof(deviceTypeId));
            AlarmCode = alarmCode ?? throw new ArgumentNullException(nameof(alarmCode));
            AlarmName = alarmName ?? throw new ArgumentNullException(nameof(alarmName));
            GroupName = groupName;
            AlarmType = alarmType;
            AlarmFrom = alarmFrom;
            TargetCode = targetCode;
            AlarmLevel = alarmLevel;
            Enabled = enabled;
            //ProtocolIndex = protocolIndex;
            //ProtocolLength = protocolLength;
            NormalValue = normalValue;
            AlarmingDescription = alarmingDescription;
            AlarmedDescription = alarmedDescription;
            SendAlarmingMessage = sendAlarmingMessage;
            SendAlarmedMessage = sendAlarmedMessage;
            CallAlarmingPhone = callAlarmingPhone;
            CallAlarmedPhone = callAlarmedPhone;
            SendAlarmingEmail = sendAlarmingEmail;
            SendAlarmedEmail = sendAlarmedEmail;
            AutoSendOrder = autoSendOrder;
            IsStatistics = isStatistics;
            RepairTime = repairTime;
            StatusMapDescription = statusMapDescription;
        }

      

        #region 基本属性
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceTypeId { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DeviceType DeviceType { get; private set; }
        /// <summary>
        /// 警报代码（协议中的位置）
        /// </summary>
        [StringLength(50)]
        [Required]
        public string AlarmCode { get; private set; }
        /// <summary>
        /// 警报名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string AlarmName { get; private set; }
        /// <summary>
        /// 警报分组
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 警报类型：持续性警报（温度过高）、瞬时警报（打火）
        /// </summary>
        public int AlarmType { get; private set; }
        /// <summary>
        /// 警报来源：0 监控终端；1 回路；2 负载(一般只有离线、在线状态)；3 配件 ...
        /// 不同的设备警报的来源可能不一样，比如监控终端
        /// </summary>
        public AlarmFrom AlarmFrom { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string TargetCode { get; set; }
        /// <summary>
        /// 警报级别
        /// </summary>
        public int AlarmLevel { get; private set; }
        /// <summary>
        /// 是否启用该警报
        /// </summary>
        public bool Enabled { get; private set; }
        ///// <summary>
        ///// 协议中的位置
        ///// </summary>
        //public int ProtocolIndex { get; private set; }
        ///// <summary>
        ///// 协议中的位置
        ///// </summary>
        //public int ProtocolLength { get; private set; }
        /// <summary>
        /// 正常状态
        /// </summary>
        [StringLength(50)]
        [Required]
        public string NormalValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string StatusMapDescription { get; set; }
        #endregion
        #region 业务属性
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
        #region 任务单
        /// <summary>
        /// 自动派单
        /// </summary>
        public bool AutoSendOrder { get; private set; }
        /// <summary>
        /// 是否参与统计
        /// </summary>
        public bool IsStatistics { get; private set; }
        /// <summary>
        /// 默认修复时长
        /// </summary>
        public double RepairTime { get; private set; }  
        #endregion
        #endregion
    }

    public enum AlarmFrom
    {
        /// <summary>
        /// 主机
        /// </summary>
        Master = 0,
        /// <summary>
        /// 负载
        /// </summary>
        Load = 1,
        /// <summary>
        /// 传感器
        /// </summary>
        Sensor =2 ,
        /// <summary>
        /// 配件
        /// </summary>
        Part =3
    }
}
