using SFBR.Device.Domain.Events;
using SFBR.Device.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 设备
    /// </summary>
    public abstract class Device:Entity,IAggregateRoot//, IParameter, IBrand, ITentant
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [StringLength(50)]
        [Required]
        public string EquipNum { get; protected set; }
        #region 领域字段
        #region 设备上传无需用户输入且不允许修改
        /// <summary>
        /// 设备类型
        /// </summary>
        [StringLength(50)]
        [Required]
        public  string DeviceTypeCode { get; protected set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        [StringLength(50)]
        public string ModelCode { get; protected set; }
        /// <summary>
        /// 是否为主机（配电监控器会挂载，采集器、遥控器等子设备，这些设备不能简单的划为传感器或者开关）
        /// </summary>
        public bool? IsMaster { get; protected set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [StringLength(50)]
        public string DeviceIP { get; protected set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int DevicePort { get; protected set; }
        ///// <summary>
        ///// 传输协议
        ///// </summary>
        //[StringLength(50)]
        //public TransferType TransferType { get; protected set; }
        ///// <summary>
        ///// 通信协议
        ///// </summary>
        //[StringLength(50)]
        //public ProtocolType ProtocolType { get; protected set; }
        ///// <summary>
        ///// 协议版本
        ///// </summary>
        //[StringLength(50)]
        //public string ProtocolVersion { get; protected set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        [StringLength(50)]
        public string ServerIP { get; protected set; }
        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServerPort { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public string ParentId { get; protected set; }
        /// <summary>
        /// 启用（手动输入的设备默认未启用）
        /// </summary>
        public bool Enabled { get; protected set; }
        /// <summary>
        /// 连接状态: 0 未连接； 1 连接
        /// </summary>
        public int Connection { get; protected set; }
        #endregion
        #region 用户端输入
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; protected set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        [StringLength(50)]
        public string DeviceName { get; protected set; }
        /// <summary>
        /// 租户
        /// </summary>
        [StringLength(50)]
        public string TentantId { get; protected set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get; protected set; }

        #endregion
        #endregion
        #region 运维信息
        /// <summary>
        /// 运维单位
        /// </summary>
        [StringLength(50)]
        public string CompanyId { get; protected set; }
        /// <summary>
        /// 运维人员
        /// </summary>
        [StringLength(50)]
        public string OprationId { get; protected set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [StringLength(50)]
        public string BrandId { get; protected set; }
        /// <summary>
        /// 质保期
        /// </summary>
        public double Warranty { get; protected set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime? InstallTime { get; protected set; }
        #endregion


        protected Device()
        {
            Id = Guid.NewGuid().ToString();
            CreationTime = DateTime.UtcNow;//注意全球化
        }

        public Device(string tentantId,string deviceName,string equipNum,string deviceTypeCode, bool enabled=false, string modelCode = null, string deviceIP = null,int devicePort = 0,string serverIP =null,int serverPort = 0,string description = null,string parentId = null,int connection = 0,bool? isMaster = null)
            :this()
        {
            TentantId = tentantId;
            DeviceName = deviceName;
            EquipNum = equipNum;
            DeviceTypeCode = deviceTypeCode;
            ModelCode = modelCode;//协议头，将作为主题发布，网关端将订阅该主题
            IsMaster = isMaster;
            //TransferType = transferType;//传输类型
            //ProtocolType = protocolType;//协议类型
            //ProtocolVersion = protocolVersion;
            ParentId = parentId;
            Connection = connection;

            Enabled = enabled;
            DeviceIP = deviceIP??"0.0.0.0";
            DevicePort = devicePort;
            ServerIP = serverIP??"0.0.0.0";
            ServerPort = ServerPort;
            Description = description;

           
            //TODO:设备授权问题尚未解决，权限配置应该聚合到哪个聚合根

        }

        #region 领域方法
        public virtual void SetName(string deviceName)
        {
            if (DeviceName != deviceName)
            {
                DeviceName = deviceName;
                //TODO:发布事件
            }
        }

        public virtual  void SetModelCode(string code)
        {
            if(ModelCode != code)
            {
                ModelCode = code;
            }
        }

        //public virtual void SetTransferType(TransferType transferType)
        //{
        //    if(TransferType != transferType)
        //    {
        //        TransferType = transferType;
        //    }
        //}

        //public virtual void SetProtocolType(ProtocolType protocolType)
        //{
        //    if(ProtocolType != protocolType)
        //    {
        //        ProtocolType = protocolType;
        //    }
        //}

        //public virtual void SetProtocolVersion(string version)
        //{
        //    if(ProtocolVersion != version)
        //    {
        //        ProtocolVersion = version;
        //    }
        //}

        public virtual void SetAddress(string ip,int port)
        {
            if(DeviceIP != ip || DevicePort != port)
            {
                DeviceIP = ip;
                DevicePort = port;
            }
        }

        public virtual void SetServerAddress(string ip, int port)
        {
            if (this.ServerIP != ip || this.ServerPort != port)
            {
                ServerIP = ip;
                ServerPort = port;
            }
        }

        public virtual void SetEnabled(bool enabled)
        {
            if(Enabled  != enabled)
            {
                Enabled = enabled;
            }
        }
        /// <summary>
        /// 设置连接状态
        /// </summary>
        /// <param name="connection"></param>
        public virtual void SetConnetion(int connection)
        {
            if (Connection != connection)
            {
                Connection = connection;
            }
        }

        //public virtual void SetParentId(string parentId)
        //{
        //    if(ParentId != parentId)
        //    {
        //        ParentId = parentId;
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tentantId"></param>
        public virtual void SetTentantId(string tentantId)
        {
            if(TentantId != tentantId)
            {
                TentantId = tentantId;
            }
        }

        /// <summary>
        /// 设置维保信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="oprationId"></param>
        /// <param name="brandId"></param>
        /// <param name="warranty"></param>
        /// <param name="installTime"></param>
        public virtual void SetOAMInfo(string companyId,string oprationId,string brandId,double warranty,DateTime? installTime)
        {
      
        }

        #endregion
    }


    public enum DeviceCode
    {
        Terminal = 0,
        Camera =1,
        FillLight = 2,
        Fan =3,
        Heater =4,
        Optical =5,
        NetSwitch = 6,
        Router = 7,
        UPS = 8

    }
    
}
