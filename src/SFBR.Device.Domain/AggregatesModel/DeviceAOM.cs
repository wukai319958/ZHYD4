using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel
{
    /// <summary>
    /// 设备运维信息（负载、主机、配件都可以）
    /// </summary>
    public class DeviceAOM:SeedWork.Entity
    {
        protected DeviceAOM()
        {

        }

        public DeviceAOM(string brandId,string companyId,string oprationId,double warranty,DateTime installTime)
        {
            BrandId = brandId;
            CompanyId = companyId;
            OprationId = oprationId;
            Warranty = warranty;
            InstallTime = installTime;
        }
        #region 运维信息
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
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime? InstallTime { get; private set; }
        #endregion 
    }
}
