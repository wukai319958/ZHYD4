using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.BrandAggregate
{
    /// <summary>
    /// 品牌维护
    /// </summary>
    public class Brand:SeedWork.Entity, SeedWork.IAggregateRoot
    {
        public Brand()
        {
            Id = Guid.NewGuid().ToString();
            CreationTime = DateTime.UtcNow;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandName"></param>
        /// <param name="groupKey"></param>
        /// <param name="tentantId"></param>
        /// <param name="description"></param>
        public Brand(string brandName, string groupKey = null, string tentantId= null, string description = null)
            :this()
        {
            BrandName = brandName ?? throw new ArgumentNullException(nameof(brandName));
            GroupKey = groupKey ;
            TentantId = tentantId ;
            Description = description ;
        }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string BrandName { get;private set; }
        /// <summary>
        /// 分组关键字，比如：摄像机、路由器、交换机等
        /// </summary>
        [StringLength(50)]
        public string GroupKey { get;private set; }
        /// <summary>
        /// 所属租户
        /// </summary>
        [StringLength(50)]
        public string TentantId { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; private set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get; private set; }

    }
}
