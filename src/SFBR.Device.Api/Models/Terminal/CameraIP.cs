using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Models.Terminal
{
    /// <summary>
    /// 
    /// </summary>
    public class CameraIP
    {
        /// <summary>
        /// 摄像机编号
        /// </summary>
        //public int CameraIndex { get; set; }
        /// <summary>
        /// 摄像机IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }
}
