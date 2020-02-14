using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.CatalogAggregate
{
    public class CatalogBrand
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
