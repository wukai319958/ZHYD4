using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.ViewModel
{
    public class AlarmDealTime
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 实际处理时间
        /// </summary>
        public double ActualTime { get; set; }
        /// <summary>
        /// 设定处理时长
        /// </summary>
        public double RepairTime { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Number { get; set; }

    }
}
