using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceTypeChannel : SeedWork.Entity
    {
        protected DeviceTypeChannel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceTypeChannel(string deviceTypeId, int portNumber, string portDefaultName, PortType portType, OutPutType outputType, bool outputThreePhase, double outputValue, bool enabled,string sort = null)
            : this()
        {
            DeviceTypeId = deviceTypeId ?? throw new ArgumentNullException(nameof(deviceTypeId));
            PortNumber = portNumber;
            PortDefaultName = portDefaultName ?? throw new ArgumentNullException(nameof(portDefaultName));
            PortType = portType;
            OutputType = outputType;
            OutputThreePhase = outputThreePhase;
            OutputValue = outputValue;
            Enabled = enabled;
            Sort = sort;
        }


        /// <summary>
        /// 设备类型
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceTypeId { get; private set; }
        public virtual DeviceType DeviceType { get; private set; }
        /// <summary>
        /// 回路编号
        /// </summary>
        public int PortNumber { get; private set; }
        /// <summary>
        /// 输出端口默认名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string PortDefaultName { get; private set; }
        /// <summary>
        /// 端口类型。0：电源 1：电源兼网口 2：网口 
        /// </summary>
        public PortType PortType { get; private set; }
        /// <summary>
        /// 输出类型：AC 交流电；DC 直流电
        /// </summary>
        public OutPutType OutputType { get; private set; }
        /// <summary>
        /// 三相输出
        /// </summary>
        public bool OutputThreePhase { get; private set; }
        /// <summary>
        /// 输出电压
        /// </summary>
        public double OutputValue { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; private set; }
        /// <summary>
        /// 排序
        /// </summary>
        [StringLength(50)]
        public string Sort { get; set; }

    }

    /// <summary>
    /// 电流类型
    /// </summary>
    public enum OutPutType
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// 交流
        /// </summary>
        AC = 1,
        /// <summary>
        /// 直流
        /// </summary>
        DC = 2
    }
    /// <summary>
    /// 
    /// </summary>
    public enum PortType
    {
        /// <summary>
        /// 视频通道
        /// </summary>
        Vedio = 0,
        /// <summary>
        /// 光端机
        /// </summary>
        Optical = 1,
        /// <summary>
        /// 补光灯
        /// </summary>
        LED = 2,
        /// <summary>
        /// 加热设备
        /// </summary>
        Heating = 3,
        /// <summary>
        /// 风扇
        /// </summary>
        Fan = 4,
        /// <summary>
        /// 
        /// </summary>
        Other = 5
    }
}
