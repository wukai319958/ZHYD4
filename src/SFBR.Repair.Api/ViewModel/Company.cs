﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Repair.Api.ViewModel
{
    public class Company
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; private set; }
        /// <summary>
        /// 单位地址
        /// </summary>
        public string Address { get; private set; }
        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string LeaderName { get; private set; }
        /// <summary>
        /// 座机号码
        /// </summary>
        public string TelePhone { get; set; }
        /// <summary>
        /// 手机电话
        /// </summary>
        public string MobilePhone { get; private set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; private set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; private set; }
    }
}
