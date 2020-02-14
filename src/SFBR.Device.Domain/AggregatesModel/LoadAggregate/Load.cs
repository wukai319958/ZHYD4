using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.LoadAggregate
{
    /// <summary>
    /// 负载类型
    /// </summary>
    public enum LoadType
    {
        /// <summary>
        /// 纯负载
        /// </summary>
        OnlyLoad = 0,
        /// <summary>
        /// 带网络连接的负载；比如：摄像机/交换机/路由器等
        /// </summary>
        NetLoad = 1
    }
    /// <summary>
    /// 负载类型（不要用枚举，这部分需要动态配置）
    /// </summary>
    //public enum LoadCategory
    //{
    //    /// <summary>
    //    /// 未知
    //    /// </summary>
    //    UnKnown = 0,
    //    /// <summary>
    //    /// 摄像机
    //    /// </summary>
    //    Camera = 1,
    //    /// <summary>
    //    /// 路由器
    //    /// </summary>
    //    Router  =2,
    //    /// <summary>
    //    /// 交换机
    //    /// </summary>
    //    Switch =3
    //}
    /// <summary>
    /// 运维的对象，重点运维的是带网络的对象
    /// 负载的地址信息都依赖主机必须聚合在一起
    /// </summary>
    public class Load:SeedWork.Entity,SeedWork.IAggregateRoot
    {
        protected Load()
        {
            Id = Guid.NewGuid().ToString();
            CreationTime = DateTime.UtcNow;
            _loadExtends = new List<DeviceExtend>();
        }
    
        public Load(LoadType loadType, string deviceTypeId, string loadName, int loadNum, string deviceId ,int portNumber, bool enabled = true, string description = null, string companyId = null, string oprationId = null, string brandId = null, double warranty = 0, DateTime? installTime = null, string tags = null)
            :this()
        {
            LoadType = loadType;
            DeviceTypeId = deviceTypeId ?? throw new ArgumentNullException(nameof(deviceTypeId));
            LoadName = loadName ?? throw new ArgumentNullException(nameof(loadName));
            LoadNum = loadNum;
            Enabled = enabled;
            DeviceId = deviceId;
            PortNumber = portNumber;
            Description = description;
            CompanyId = companyId;
            OprationId = oprationId;
            BrandId = brandId;
            Warranty = warranty;
            InstallTime = installTime;
            Tags = tags;
        }

        /// <summary>
        /// 负载类型
        /// </summary>
        [StringLength(50)]
        [Required]
        public LoadType LoadType { get;protected set; }
        /// <summary>
        /// 负载类型
        /// </summary>
        [StringLength(50)]
        public string DeviceTypeId { get;protected set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual LoadCategory LoadCategory { get;private set; }
        /// <summary>
        /// 负载名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string LoadName { get;private set; }
        /// <summary>
        /// 负载地址编号
        /// </summary>
        public int LoadNum { get;protected set; }
        
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; protected set; }
        /// <summary>
        /// 主机Id
        /// </summary>
        [StringLength(50)]
        public string DeviceId { get; protected set; }
        /// <summary>
        /// 挂载的回路
        /// </summary>
        public int PortNumber { get; set; }
        /// <summary>
        /// 详情ID（当负载接入系统后需要手动关联，只有负载类型和设备类型才可以关联）
        /// </summary>
        [StringLength(50)]
        public string DetailId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get;protected set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        [StringLength(450)]
        public string Description { get;protected set; }
        #region 运维信息
        /// <summary>
        /// 运维单位
        /// </summary>
        public string CompanyId { get;protected set; }
        /// <summary>
        /// 运维人员
        /// </summary>
        public string OprationId { get;protected set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandId { get;protected set; }
        /// <summary>
        /// 质保期
        /// </summary>
        public double Warranty { get;protected set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime? InstallTime { get;protected set; }
        #endregion 
        /// <summary>
        /// 租户
        /// </summary>
        [StringLength(50)]
        public string TentantId { get;protected set; }
        protected List<DeviceExtend> _loadExtends;
        /// <summary>
        /// 负载扩展属性
        /// </summary>
        public virtual ICollection<DeviceExtend> LoadExtends => _loadExtends;
        /// <summary>
        /// 标签类型（不同类型的负载应用不同，比如摄像头：卡口视频、监控视频等）
        /// </summary>
        [StringLength(350)]
        public string Tags { get;protected set; }//最优方案是建立标签系统，可以对任意的对象打上分组标签
    }
    
    /// <summary>
    /// 网络负载
    /// </summary>
    public class NetLoad : Load
    {
        protected NetLoad():base()
        {

        }
       

        public NetLoad(string ip, int connection, LoadType loadType, string loadCategoryId, string loadName, int loadNum, string deviceId, int portNumber,bool enabled = true, string description = null, string companyId = null, string oprationId = null, string brandId = null, double warranty = 0, DateTime? installTime = null, string tags = null)
           : base(loadType,loadCategoryId,loadName,loadNum, deviceId, portNumber, enabled, description, companyId,oprationId,brandId,warranty,installTime,tags)
        {
            IP = ip ?? "0.0.0.0";
            Connection = connection;
        }

        /// <summary>
        /// 负载的IP地址
        /// </summary>
        [StringLength(50)]
        public string IP { get;private set; }
        /// <summary>
        /// 连接状态
        /// </summary>
        public int Connection { get;private set; }
    }
}
