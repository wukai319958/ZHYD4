using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate
{
    /// <summary>
    /// 设备功能
    /// </summary>
    public class DeviceTypeFunction:SeedWork.Entity
    {
        protected DeviceTypeFunction()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceTypeFunction(string deviceTypeId, bool enabled, string functionName, string functionCode, string sort,string callbackCodes, FunctionType functionType= FunctionType.ReadWrite,string description = null,string setting = null,string settingTypeName = null)
            : this()
        {
            DeviceTypeId = deviceTypeId ?? throw new ArgumentNullException(nameof(deviceTypeId));
            Enabled = enabled;
            FunctionName = functionName ?? throw new ArgumentNullException(nameof(functionName));
            FunctionCode = functionCode ?? throw new ArgumentNullException(nameof(functionCode));
            Sort = sort;
            FunctionType = functionType;
            Description = description;
            Setting = setting;
            SettingTypeName = settingTypeName;
            CallbackCodes = callbackCodes;
        }

        /// <summary>
        ///功能模块的粒度一般不会细化到端口
        /// </summary>
        public int PortNumber { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        [StringLength(50)]
        public string DeviceTypeId { get;private set; }
        public virtual DeviceType DeviceType { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        [StringLength(150)]
        [Required]
        public string FunctionName { get; private set; }
        /// <summary>
        /// 功能编号（内部自定义）
        /// </summary>
        [StringLength(100)]
        public string FunctionCode { get;private set; }
        /// <summary>
        /// 功能排序
        /// </summary>
        public string Sort { get;private set; }
        ///// <summary>
        ///// 读取/写入
        ///// </summary>
        public FunctionType FunctionType { get;private set; }//功能模块
        /// <summary>
        /// 备注描述（帮助描述）
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// 默认设置的值
        /// </summary>
        public string Setting { get; private set; }
        /// <summary>
        /// 值的类型
        /// </summary>
        [StringLength(2000)]//泛型会非常长
        public string SettingTypeName { get; set; }
        /// <summary>
        /// 一个功能会有多个命令码
        /// </summary>
        [StringLength(150)]
        public string CallbackCodes { get; set; }
    }

    /// <summary>
    /// 写入状态
    /// </summary>
    public enum FunctionType
    {
        /// <summary>
        /// 只读
        /// </summary>
        ReadOnly,
        /// <summary>
        /// 读写
        /// </summary>
        ReadWrite
    }
}
