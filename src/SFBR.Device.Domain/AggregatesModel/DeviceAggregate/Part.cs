using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 配件：除允许用户远程控制的部件外都是配件
    /// </summary>
    public class Part :SeedWork.Entity//, IPart,IParameter,IBrand
    {
        protected Part()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Part(string deviceId, string partCode, int? portNumber = null, string status =null,string alarmStatus = null, bool? enabled = null) 
            :this()
        {
            DeviceId = deviceId;
            PortNumber = portNumber;
            PartCode = partCode;
            Status = status;
            AlarmStatus = alarmStatus;
            Enabled = enabled;
        }
        /// <summary>
        /// 设备id
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceId { get;private set; }
        /// <summary>
        /// 回路地址（-1表示主机）
        /// </summary>
        public int? PortNumber { get;private set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string PartCode { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>                                   
        public bool? Enabled { get;private set; }
        /// <summary>
        /// 开关量状态
        /// </summary>
        [StringLength(50)]
        public string Status { get; private set; }
        /// <summary>
        /// 警报状态
        /// </summary>
        [StringLength(50)]
        public string AlarmStatus { get; private set; }
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
        public double? Warranty { get; private set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime? InstallTime { get; private set; }
        #endregion

        public void SetStatus(string status)
        {
            Status = status;
        }
        public void SetAlarmStatus(string alarmStatus)
        {
            AlarmStatus = alarmStatus;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

    }
    /// <summary>
    /// 相位类型
    /// </summary>
    //public enum Phase
    //{
    //    /// <summary>
    //    /// 未知
    //    /// </summary>
    //    UnKonw = 0,
    //    /// <summary>
    //    /// A相
    //    /// </summary>
    //    A = 1,
    //    /// <summary>
    //    /// B相
    //    /// </summary>
    //    B = 2,
    //    /// <summary>
    //    /// C相
    //    /// </summary>
    //    C = 3,
    //    /// <summary>
    //    /// 三相
    //    /// </summary>
    //    ABC = 4
    //}


}
