using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceTypeController : SeedWork.Entity
    {
        protected DeviceTypeController()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceTypeController(string deviceTypeId, int portNumber,string controllerCode, ControllerType controllerType = ControllerType.Switch, string buttons = "开,关", bool enabled=true, string controllerStatus="0", string description = null)
            : this()
        {
            DeviceTypeId = deviceTypeId ?? throw new ArgumentNullException(nameof(deviceTypeId));
            PortNumber = portNumber;
            ControllerCode = controllerCode ?? throw new ArgumentNullException(nameof(controllerCode));
            ControllerType = controllerType;
            Buttons = buttons;
            Enabled = enabled;
            ControllerStatus = controllerStatus ?? throw new ArgumentNullException(nameof(controllerStatus));
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
        /// 控制器编号
        /// </summary>
        [StringLength(50)]
        [Required]
        public string ControllerCode { get; set; }
        /// <summary>
        /// 控制器类型
        /// </summary>
        public ControllerType ControllerType { get; private set; }
        /// <summary>
        /// 按钮（英文逗号隔开）
        /// </summary>
        public string Buttons { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; private set; }
        /// <summary>
        /// 控制器默认状态
        /// </summary>
        [StringLength(50)]
        public string ControllerStatus { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
    /// <summary>
    /// 控制器
    /// </summary>
    public enum ControllerType
    {
        /// <summary>
        ///开关
        /// </summary>
        Switch = 0
    }
}
