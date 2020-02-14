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
    public class DeviceTypePart:SeedWork.Entity
    {
        protected DeviceTypePart()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceTypePart(string deviceTypeId, int portNumber, string partCode, string partName, PartType partType, bool hasStatus, int statusIndex, bool enabled, string description = null, string companyId = null, string oprationId = null, string brandId = null, double warranty = 0)
            : this()
        {
            DeviceTypeId = deviceTypeId ?? throw new ArgumentNullException(nameof(deviceTypeId));
            PortNumber = portNumber;
            PartCode = partCode ?? throw new ArgumentNullException(nameof(partCode));
            PartName = partName ?? throw new ArgumentNullException(nameof(partName));
            PartType = partType;
            HasStatus = hasStatus;
            StatusIndex = statusIndex;
            Enabled = enabled;
            Description = description;
            CompanyId = companyId;
            OprationId = oprationId;
            BrandId = brandId;
            Warranty = warranty;
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
        /// 配件代码 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string PartCode { get; private set; }
        /// <summary>
        /// 配件名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string PartName { get; private set; }
        /// <summary>
        /// 配件类型
        /// </summary>
        public PartType PartType { get; private set; }//配件类型由输出端口决定
        /// <summary>
        /// 拥有状态
        /// </summary>
        public bool HasStatus { get; private set; }
        /// <summary>
        /// 协议中的下标
        /// </summary>
        public int StatusIndex { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; private set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get; private set; }
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
    }
    public enum PartType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = 0,
        /// <summary>
        /// 必要零件
        /// </summary>
        [Description("必要零件")]
        Necessary = 1,
        /// <summary>
        /// 附件(选配的模块)
        /// </summary>
        [Description("附件")]
        Accessory = 2,
        /// <summary>
        /// 扩展（市电/自定义电源等）
        /// </summary>
        [Description("扩展")]
        Extend = 3
    }
}
