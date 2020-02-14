using SFBR.Device.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 设备警报
    /// </summary>
    public class DeviceAlarm:SeedWork.Entity
    {
        protected DeviceAlarm()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceAlarm(string deviceId, string alarmCode,string targetCode ,string normalValue, string status = "0", DateTime? alarmTime = null, double? repairTime = null, bool? enabled = null)
        {
            DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
            AlarmCode = alarmCode ?? throw new ArgumentNullException(nameof(alarmCode));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            TargetCode = targetCode ?? throw new ArgumentNullException(nameof(targetCode));
            NormalValue = normalValue;
            AlarmTime = alarmTime ;
            RepairTime = repairTime ;
            Enabled = enabled ;
        }

        //public DeviceAlarm(string deviceId, string deviceTypeAlarmId, AlarmStatus alarmStatus = AlarmStatus.Alarmed, DateTime? alarmTime = null, double? repairTime = null, bool? enabled = null)
        //    :this()
        //{

        //    DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
        //    DeviceTypeAlarmId = deviceTypeAlarmId ?? throw new ArgumentNullException(nameof(deviceTypeAlarmId));
        //    AlarmStatus = alarmStatus;
        //    AlarmTime = alarmTime;
        //    RepairTime = repairTime;
        //    Enabled = enabled;
        //}

        [StringLength(50)]
        [Required]
        public string DeviceId { get; private set; }
        /// <summary>
        /// 警报代码（协议中的位置）
        /// </summary>
        [StringLength(50)]
        [Required]
        public string AlarmCode { get; private set; }
        /// <summary>
        /// 当前警报值
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Status { get; private set; }
        /// <summary>
        /// 警报对象唯一标识
        /// </summary>
        [StringLength(50)]
        [Required]
        public string TargetCode { get; set; }
        /// <summary>
        /// 正常值
        /// </summary>
        [StringLength(50)]
        public string NormalValue { get; private set; }
        ///// <summary>
        ///// 警报状态
        ///// </summary>
        //[NotMapped]
        //public AlarmStatus AlarmStatus => Status == NormalValue ? AlarmStatus.Alarmed : AlarmStatus.Alarming;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AlarmTime { get; private set; }
        /// <summary>
        /// 修复时长
        /// </summary>
        public double? RepairTime { get; private set; }
        /// <summary>
        /// 是否启用该警报
        /// </summary>
        public bool? Enabled { get; private set; }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="alarmStatus"></param>
        //public void SetAlarmStatus(AlarmStatus alarmStatus)
        //{
        //    if (AlarmStatus == alarmStatus) return;
        //    //TODO:需要注意不可持续的报警直接持久化，一般该类警报不会在状态信息而是在警报信息推送直接持久化即可
        //    switch (alarmStatus)
        //    {
        //        case AlarmStatus.Alarmed:
        //            //TODO:发布警报解除事件
        //            AddDomainEvent(null);
        //            break;
        //        case AlarmStatus.Alarming:
        //            //TODO:发布警报事件
        //            AddDomainEvent(null);
        //            break;
        //        default:
        //            break;
        //    }
        //}
        public void SetStatus(string status)
        {
            Status = status;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }
    }
}
