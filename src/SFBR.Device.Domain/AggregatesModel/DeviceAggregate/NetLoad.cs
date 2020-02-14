using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 网络负载
    /// </summary>
    public class NetLoad:Load
    {
        /// <summary>
        /// 负载的IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 连接状态
        /// </summary>
        public int Connection { get; set; }
        /// <summary>
        /// 维护信息
        /// </summary>
        public string DeviceAOMId { get; private set; }
        /// <summary>
        /// 运维信息
        /// </summary>
        public DeviceAOM DeviceAOM { get; private set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string NetLoadExtId { get; set; }
        /// <summary>
        /// 摄像机扩展信息
        /// </summary>
        public virtual NetLoadExt NetLoadExt { get; private set; }
    }

    /// <summary>
    /// 摄像机扩展信息
    /// </summary>
    public class NetLoadExt : SeedWork.Entity
    {
        /// <summary>
        /// 摄像机类型：卡口视频，监控视频
        /// </summary>
        public string CameraType { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 摄像机端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 摄像头通道号（一个摄像机主机会有多个通道号，与运维主机类似）
        /// </summary>
        public int ChannelNum { get; set; }
        /// <summary>
        /// 自定义属性（）
        /// </summary>
        public string CustomProp { get; set; }
    }
}
