using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Models.Terminal
{
    /// <summary>
    /// 
    /// </summary>
    public class CameraPostion
    {
        /// <summary>
        /// 摄像头（其他网络设备）编号
        /// </summary>
        public int CameraIndex { get; set; }
        /// <summary>
        /// 绑定位置
        /// </summary>
        public Postion Postion { get; set; }
     
    }
    /// <summary>
    /// 位置
    /// </summary>
    public enum Postion
    {
        /// <summary>
        /// 无
        /// </summary>
        OutNot,
        /// <summary>
        /// 默认端口
        /// </summary>
        AC220V,
        /// <summary>
        /// 输出端口1
        /// </summary>
        Output1,
        /// <summary>
        /// 输出端口2
        /// </summary>
        Output2
    }
}
