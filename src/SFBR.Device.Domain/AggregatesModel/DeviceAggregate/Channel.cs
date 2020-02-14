using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 主机的输出端口
    /// </summary>
    public class Channel : SeedWork.Entity//每款设备基本都有固定的回路，小型断路器除外
    {
        protected Channel()
        {
            Id = Guid.NewGuid().ToString();
            //AlarmStaus = "0";
        }

        public Channel(string deviceId, int portNumber, DeviceTypeAggregate.PortType portType, string portDefaultName = null, DeviceTypeAggregate.OutPutType? outputType =null,bool ? outputThreePhase = null,double? outputVoltage = null, bool enabled = true,string description = "",string sort =  null) 
            :this()
        {
            this.DeviceId = deviceId;
            //this.DeviceTypeChannelId = deviceTypeChannelId;
            this.PortNumber = portNumber;
            this.PortDefaultName = portDefaultName;
            PortType = portType;
            //this.ChannelName = channelName;
            this.OutputType = outputType;
            this.OutputThreePhase = outputThreePhase;
            this.OutputValue = outputVoltage;
            this.Enabled = enabled;
            this.Description = description;
            Sort = sort;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceId { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public int PortNumber { get; private set; }
        /// <summary>
        /// 端口类型
        /// </summary>
        public DeviceTypeAggregate.PortType PortType { get; private set; }
        /// <summary>
        /// 输出端口默认名称（不允许用户修改，设备上写的是什么就是什么。用户自定义的是ChannelName）
        /// </summary>
        [StringLength(50)]
        public string PortDefaultName { get; private set; }
        /// <summary>
        /// 输出类型：AC 交流电；DC 直流电
        /// </summary>
        public DeviceTypeAggregate.OutPutType? OutputType { get; private set; }
        /// <summary>
        /// 三相输出
        /// </summary>
        public bool? OutputThreePhase { get; private set; }
        /// <summary>
        /// 输出电压
        /// </summary>
        public double? OutputValue { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get;private set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get;private set; }
        /// <summary>
        /// 排序
        /// </summary>
        [StringLength(50)]
        public string Sort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        public void SetName(string name)
        {
            PortDefaultName = name;
        }

        public void SetOutPutValue(double value)
        {
            OutputValue = value;
        }
    }

   
}
