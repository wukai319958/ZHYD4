using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate
{
    /// <summary>
    /// 设备类型（设备类型相关的实体都有基础数据，且只有修改接口无法新增和删除。修改主要为启用禁用功能）
    /// </summary>
    public class DeviceType:SeedWork.Entity, SeedWork.IAggregateRoot
    {
        protected DeviceType()
        {
            Id = Guid.NewGuid().ToString();
            _deviceTypeAlarms = new List<DeviceTypeAlarm>();
            _deviceTypeChannels = new List<DeviceTypeChannel>();
            _deviceTypeControllers = new List<DeviceTypeController>();
            _deviceTypeFunctions = new List<DeviceTypeFunction>();
            _deviceTypeParts = new List<DeviceTypePart>();
            _deviceTypeSensors = new List<DeviceTypeSensor>();
            //_deviceTypeProps = new List<DeviceTypeProp>();
        }

        public DeviceType(string code, string model, string name, string groupKey = null, bool enabled = true, string description = "", string companyId = "", string oprationId = "", string brandId = "", double warranty = 0,bool autoCreate = true,bool isMaster = true, TransferType transferType = TransferType.UDP,ProtocolType protocolType = ProtocolType.ModBus)
            :this()
        {
            Code = code;
            Model = model;
            Name = name;
            GroupKey = groupKey;
            Enabled = enabled;
            Description = description;
            CompanyId = companyId;
            OprationId = oprationId;
            BrandId = brandId;
            Warranty = warranty;
            AutoCreate = autoCreate;
            IsMaster = isMaster;
            ProtocolType = protocolType;
            TransferType = transferType;
        }

        /// <summary>
        /// 编号（协议中的设备编号）
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Code { get; private set; }
        /// <summary>
        /// 型号（协议的版本号）
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Model { get; private set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Name { get; private set; }
        /// <summary>
        /// 分组key
        /// </summary>
        [StringLength(50)]
        public string GroupKey { get; private set; }
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 自动创建
        /// </summary>
        public bool AutoCreate { get; set; }
        /// <summary>
        /// 是否为主机
        /// </summary>
        public bool IsMaster { get; set; }
        /// <summary>
        /// 传输协议
        /// </summary>
        [StringLength(50)]
        public TransferType TransferType { get; protected set; }
        /// <summary>
        /// 通信协议
        /// </summary>
        [StringLength(50)]
        public ProtocolType ProtocolType { get; protected set; }
        /// <summary>
        /// 协议版本
        /// </summary>
        [StringLength(50)]
        public string ProtocolVersion { get; protected set; }
        #region 默认运维信息
        /// <summary>
        /// 运维单位
        /// </summary>
        [StringLength(50)]
        public string CompanyId { get; private set; }
        /// <summary>
        /// 运维人员
        /// </summary>
        [StringLength(50)]
        public string OprationId { get; private set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [StringLength(50)]
        public string BrandId { get; private set; }
        /// <summary>
        /// 质保期
        /// </summary>
        public double Warranty { get; private set; }
        #endregion
        #region 抽象的组件
        private List<DeviceTypeAlarm> _deviceTypeAlarms = new List<DeviceTypeAlarm>();
        /// <summary>
        /// 该类设备拥有的警报
        /// </summary>
        public virtual IReadOnlyCollection<DeviceTypeAlarm> Alarms => _deviceTypeAlarms;
        private List<DeviceTypeFunction> _deviceTypeFunctions;
        /// <summary>
        /// 该类型设备拥有的功能
        /// </summary>
        public virtual IReadOnlyCollection<DeviceTypeFunction> Functions => _deviceTypeFunctions;
        private List<DeviceTypeChannel> _deviceTypeChannels;
        /// <summary>
        /// 默认回路
        /// </summary>
        public virtual IReadOnlyCollection<DeviceTypeChannel> Channels => _deviceTypeChannels;
        private List<DeviceTypePart> _deviceTypeParts;
        /// <summary>
        /// 默认配件
        /// </summary>
        public virtual IReadOnlyCollection<DeviceTypePart> Parts => _deviceTypeParts;
        private List<DeviceTypeController> _deviceTypeControllers;
        /// <summary>
        /// 默认控制器
        /// </summary>
        public virtual IReadOnlyCollection<DeviceTypeController> Controllers => _deviceTypeControllers;
        private List<DeviceTypeSensor> _deviceTypeSensors;
        /// <summary>
        /// 默认传感器
        /// </summary>
        public virtual IReadOnlyCollection<DeviceTypeSensor> Sensors => _deviceTypeSensors;
        //private List<DeviceTypeProp> _deviceTypeProps;
        ///// <summary>
        ///// 扩展属性
        ///// </summary>
        //public virtual IReadOnlyCollection<DeviceTypeProp> Props => _deviceTypeProps;
        /***
         * 随着设备的增加配件种类可能需要增加
         * ***/
        #endregion

        #region 领域方法
        /// <summary>
        /// 启用
        /// </summary>
        public void SetEnabled()
        {
            Enabled = true;
        }
        /// <summary>
        /// 禁用
        /// </summary>
        public void SetDisabled()
        {
            Enabled = false;
        }
        /// <summary>
        /// 设置维保信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="oprationId"></param>
        /// <param name="brandId"></param>
        /// <param name="warranty"></param>
        public void SetWarrantyInformation(string companyId , string oprationId , string brandId , double warranty )
        {
            CompanyId = companyId;
            OprationId = oprationId;
            BrandId = brandId;
            Warranty = warranty;
        }

        #endregion


    }
    #region 枚举
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceTypeCode
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未知")]
        UnKnow = 0,
        /// <summary>
        /// 智维终端控制器(Intelligent terminal)
        /// </summary>
        [Description("智维终端控制器")]
        Terminal = 1
    }
    /// <summary>
    /// 协议类型
    /// </summary>
    public enum ProtocolType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未知")]
        UnKnown = 0,
        /// <summary>
        /// ModBus
        /// </summary>
        [Description("ModBus")]
        ModBus = 1,
        /// <summary>
        /// SNMP
        /// </summary>
        [Description("SNMP")]
        SNMP = 2,
        /// <summary>
        /// MQTT
        /// </summary>
        [Description("MQTT")]
        MQTT = 3
    }

    public enum TransferType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未知")]
        UnKnown = 0,
        /// <summary>
        /// UDP
        /// </summary>
        [Description("UDP")]
        UDP = 1,
        /// <summary>
        /// TCP
        /// </summary>
        [Description("TCP")]
        TCP = 2//,
        ///// <summary>
        ///// 串口
        ///// </summary>
        //[Description("串口")]
        //SerialPort = 3,
        ///// <summary>
        ///// 蓝牙
        ///// </summary>
        //[Description("蓝牙")]
        //Bluetooth = 4,
        ///// <summary>
        ///// Zigbee
        ///// </summary>
        //[Description("Zigbee")]
        //Zigbee = 5,
        ///// <summary>
        ///// 载波
        ///// </summary>
        //[Description("载波")]
        //Carrier = 6
    }

    #endregion
}
