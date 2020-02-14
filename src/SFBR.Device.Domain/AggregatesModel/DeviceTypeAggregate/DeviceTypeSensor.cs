using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceTypeSensor:SeedWork.Entity
    {
        protected DeviceTypeSensor()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceTypeSensor(string deviceTypeId, int portNumber,string sensorCode, string sensorName, SensorType sensorType, double? upperValue, double? lowerValue, bool enabled =true, string description = null)
            : this()
        {
            DeviceTypeId = deviceTypeId ?? throw new ArgumentNullException(nameof(deviceTypeId));
            PortNumber = portNumber;
            SensorCode =sensorCode ?? throw new ArgumentNullException(nameof(sensorCode));
            SensorName = sensorName ?? throw new ArgumentNullException(nameof(sensorName));
            SensorType = sensorType;
            UpperValue = upperValue;
            LowerValue = lowerValue;
            Enabled = enabled;
            Description = description;
        }



        /// <summary>
        /// 设备类型
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceTypeId { get; private set; }
        public virtual DeviceType DeviceType { get; private set; }
        /// <summary>
        /// 控制器的端口(-1表示主机的控制器)
        /// </summary>
        public int PortNumber { get; private set; }
        /// <summary>
        /// 传感器编码
        /// </summary>
        [StringLength(50)]
        [Required]
        public string SensorCode { get; set; }
        /// <summary>
        /// 传感器名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string SensorName { get; private set; }
        /// <summary>
        /// 配件代码 
        /// </summary>
        [StringLength(50)]
        [Required]
        public SensorType SensorType { get; private set; }
        /// <summary>
        /// 模拟量上限
        /// </summary>
        public double? UpperValue { get; private set; }
        /// <summary>
        /// 模拟量下限
        /// </summary>
        public double? LowerValue { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; private set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }
    }


    public enum SensorType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown,
        /// <summary>
        /// 电压传感器
        /// </summary>
        [Description("电压传感器")]
        Voltage,
        /// <summary>
        /// 电流互感器
        /// </summary>
        [Description("电流互感器")]
        Current,
        /// <summary>
        /// 温度传感器
        /// </summary>
        [Description("温度传感器")]
        Temperature,
        /// <summary>
        /// 漏电互感器
        /// </summary>
        [Description("漏电互感器")]
        Leakage,
        /// <summary>
        /// 湿度传感器
        /// </summary>
        [Description("湿度传感器")]
        Humidity
    }
}
