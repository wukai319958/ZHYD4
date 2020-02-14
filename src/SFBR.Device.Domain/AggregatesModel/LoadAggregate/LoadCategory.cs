using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.LoadAggregate
{
    /// <summary>
    /// 负载类型
    /// </summary>
    public class LoadCategory : SeedWork.Entity
    {
        protected LoadCategory()
        {
            _loadProps = new List<LoadProp>();
        }
        protected LoadCategory(string id)
            : this()
        {
            Id = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;
        }

        public LoadCategory(int code, string name, string model, string pictureFileName = null, string description = null, string companyId = null, string oprationId = null, string brandId = null, double warranty = 0)
            :this(code.ToString())
        {
            Code = code;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            PictureFileName = pictureFileName;
            Description = description ;
            CompanyId = companyId;
            OprationId = oprationId ;
            BrandId = brandId;
            Warranty = warranty;
        }



        /// <summary>
        /// 负载的设备类型（0：视频 1：光端机 2：补光灯 3：加热设备 4、风扇 5、不支持该通道为兼容其他通道数少的设备）
        /// </summary>
        public int Code { get;private set; }
        /// <summary>
        /// 负载型号
        /// </summary>
        [StringLength(150)]
        public string Model { get; private set; } = "Default";
        /// <summary>
        /// 负载名称
        /// </summary>
        [StringLength(150)]
        public string Name { get;private set; }
        /// <summary>
        /// 负载图片名称
        /// </summary>
        [StringLength(350)]
        public string PictureFileName { get; private set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get;private set; }
        #region 默认运维信息
        /// <summary>
        /// 运维单位
        /// </summary>
        public string CompanyId { get; private set; }
        /// <summary>
        /// 运维人员
        /// </summary>
        public string OprationId { get; private set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandId { get; private set; }
        /// <summary>
        /// 质保期
        /// </summary>
        public double Warranty { get; private set; }
        #endregion
        protected List<LoadProp> _loadProps;
        /// <summary>
        /// 属性
        /// </summary>
        public virtual ICollection<LoadProp> LoadProps => _loadProps;
    }
}
