using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.CatalogAggregate
{
    /// <summary>
    /// 负载目录库
    /// </summary>
    public class CatalogItem:SeedWork.Entity,SeedWork.IAggregateRoot
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string ModelCode { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string PictureFileName { get; set; }
        /// <summary>
        /// 负载类型id
        /// </summary>
        public string CatalogTypeId { get; set; }
        /// <summary>
        /// 负载类型实体
        /// </summary>
        public CatalogType CatalogType { get; set; }
        /// <summary>
        /// 负载品牌id
        /// </summary>
        public string CatalogBrandId { get; set; }
        /// <summary>
        /// 负载品牌实体
        /// </summary>
        public CatalogBrand CatalogBrand { get; set; }
    }
}
