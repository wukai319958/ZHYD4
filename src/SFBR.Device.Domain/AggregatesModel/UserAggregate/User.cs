using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.UserAggregate
{
    /// <summary>
    /// 用户基本信息（冗余的数据，用户登录的时候新增或者更新数据无删除操作）
    /// </summary>
    public class User:SeedWork.Entity,SeedWork.IAggregateRoot
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 登录账号
        /// </summary>
        [StringLength(150)]
        public string Account { get; set; }
        /// <summary>
        /// 登录密码（加密后的）
        /// </summary>
        [StringLength(150)]
        public string Password { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [StringLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        [StringLength(50)]
        public string TentantId { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(50)]
        public string Phone { get; set; }
        /// <summary>
        /// email
        /// </summary>
        [StringLength(150)]
        public string Email { get; set; }
        /// <summary>
        /// 是否为开发人员
        /// </summary>
        public bool IsDeveloper { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }
    }
}
