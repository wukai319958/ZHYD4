using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.OprationAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class Operation : SeedWork.Entity
    {
        /// <summary>
        /// 维保人员姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 座机号码
        /// </summary>
        public string TelePhone { get; set; }
        /// <summary>
        /// 手机电话
        /// </summary>
        public string MobilePhone { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }

        public string CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
