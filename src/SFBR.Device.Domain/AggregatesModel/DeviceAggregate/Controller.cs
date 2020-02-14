using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 控制器（允许远程控制，只能主机控制的不算）
    /// </summary>
    public class Controller :SeedWork.Entity
    {
        protected Controller()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Controller(string deviceId, string controllerCode, int? portNumber = null, string buttons = null , bool? enabled = null,string controllerStatus = null, string description = null)
            :this()
        {
            DeviceId = deviceId;
            ControllerCode = controllerCode;
            PortNumber = portNumber;
            Buttons = buttons;
            Enabled = enabled;
            ControllerStatus = controllerStatus;
            Description = description;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceId { get;private set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string ControllerCode { get;private set; }
        /// <summary>
        /// 控制器的端口(-1表示主机的控制器)
        /// </summary>
        public int? PortNumber { get; private set; }
        /// <summary>
        /// 按钮（英文逗号隔开）
        /// </summary>
        [StringLength(350)]
        public string Buttons { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? Enabled { get;private set; }
        /// <summary>
        /// 控制器当前状态
        /// </summary>
        [StringLength(50)]
        public string ControllerStatus { get;private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get;private set; }

        public void SetControllerStatus(string controllerStatus)
        {
            ControllerStatus = controllerStatus;
        }

        public void SetEnable(bool enable)
        {
            Enabled = enable;
        }
    }
    
}
