using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.RegionAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class Region : SeedWork.Entity, SeedWork.IAggregateRoot
    {
        protected Region()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Region(string regionCode, string regionName, string parentId, string tentantId, string description = null)
            : this()
        {
            RegionCode = regionCode ?? throw new ArgumentNullException(nameof(regionCode));
            RegionName = regionName ?? throw new ArgumentNullException(nameof(regionName));
            ParentId = parentId ;
            TentantId = tentantId;
            Description = description;
        }


        /// <summary>
        /// 区域名称
        /// </summary>
        [StringLength(150)]
        [Required]
        public string RegionCode { get; private set; }
        [StringLength(50)]
        [Required]
        public string RegionName { get; private set; }
        /// <summary>
        /// 上级区域
        /// </summary>
        [StringLength(50)]
        public string ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Region Parent { get; private set; }
        /// <summary>
        /// 租户
        /// </summary>
        [StringLength(50)]
        public string TentantId { get; protected set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get; private set; }

        #region 领域方法
        public virtual void SetName(string regionName)
        {
            if (RegionName != regionName)
            {
                RegionName = regionName;
            }
        }

        #endregion
    }
}
