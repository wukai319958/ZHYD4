using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Repair.Api.Model
{
    /// <summary>
    /// 维修人员
    /// </summary>
    public class Employee
    {
        public string Id { get; set; }
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
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 公司id
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Company Company { get; set; }
    }
}
