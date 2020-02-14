using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 传感器（只有模拟量）
    /// </summary>
    public class Sensor:SeedWork.Entity
    {
        protected Sensor()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Sensor(string deviceId,string sensorCode, string alarmStatus = "0",int? portNumber = null, double? upperValue = null,double? lowerValue = null,double? realValue = null,bool? enabled = null,string description = "")
            : this()
        {
            DeviceId = deviceId;
            SensorCode = sensorCode;
            AlarmStatus = alarmStatus;
            PortNumber = portNumber;
            UpperValue = upperValue;
            LowerValue = lowerValue;
            RealValue = realValue;
            Enabled = enabled;
            Description = description;
        }

        /// <summary>
        /// 设备id
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceId { get;private set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string SensorCode { get; set; }
        /// <summary>
        /// 控制器的端口(-1表示主机的控制器)
        /// </summary>
        public int? PortNumber { get; private set; }
        /// <summary>
        /// 警报状态
        /// </summary>
        [StringLength(50)]
        public string AlarmStatus { get; private set; }
        /// <summary>
        /// 模拟量上限
        /// </summary>
        public double? UpperValue { get;private set; }
        /// <summary>
        /// 模拟量下限
        /// </summary>
        public double? LowerValue { get;private set; }
        /// <summary>
        /// 模拟量
        /// </summary>
        public double? RealValue { get;private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? Enabled { get;private set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get;private set; }

        public void SetAlarmStatus(string alarmStatus)
        {
            AlarmStatus = alarmStatus;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        public void SetUpperValue(double? value)
        {
            UpperValue = value;
        }

        public void SetLowerValue(double? value)
        {
            LowerValue = value;
        }
    }

}
