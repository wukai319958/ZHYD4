using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api
{
    public class DeviceSettings
    {
        //public bool UseCustomizationData { get; set; }
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        ///// <summary>
        ///// 智维终端命令通道
        ///// </summary>
        //public string TerminalCommand { get; set; }
        ///// <summary>
        ///// 智慧终端实时推送
        ///// </summary>
        //public string TerminalRealPush { get; set; }
        /// <summary>
        /// 消息队列
        /// </summary>
        public string EventBusConnection { get; set; }

        public string LocalIP { get; set; } = "127.0.0.1";
        public string VedioResalution { get; set; } = "640*480";
        //public int CheckUpdateTime { get; set; }
    }
}
