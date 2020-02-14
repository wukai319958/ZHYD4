using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    public class DevicePower:SeedWork.Entity
    {
        protected DevicePower()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DevicePower(string deviceId, string accountId) 
            : this()
        {
            DeviceId = deviceId;
            AccountId = accountId;
        }

        [Required]
        [StringLength(50)]
        public string DeviceId { get; set; }
        /// <summary>
        /// 账号（手机号码不可以作为账号，修改号码会让账号失效）
        /// </summary>
        [Required]
        [StringLength(50)]
        public string AccountId { get; set; }
        /// <summary>
        /// 需要屏蔽的警报，英文逗号隔开
        /// </summary>
        public string AlarmBlackList { get; set; }
        /// <summary>
        /// 需要屏蔽的功能，英文逗号隔开
        /// </summary>
        public string FunctionBlackList { get; set; }
        #region 发生警报时的通知方式，可以同时配置多个
        /// <summary>
        /// 短信通知
        /// </summary>
        public bool? SendSMS { get; set; }
        /// <summary>
        /// 邮件通知
        /// </summary>
        public bool? SendEmail { get; set; }
        /// <summary>
        /// 打电话通知
        /// </summary>
        public bool? CallPhone { get; set; }
        #endregion
        #region 警报解除时的通知方式
        /// <summary>
        /// 短信通知
        /// </summary>
        public bool? Alarmed_SendSMS { get; set; }
        /// <summary>
        /// 邮件通知
        /// </summary>
        public bool? Alarmed_SendEmail { get; set; }
        /// <summary>
        /// 打电话通知
        /// </summary>
        public bool? Alarmed_CallPhone { get; set; }
        #endregion

    }
}
