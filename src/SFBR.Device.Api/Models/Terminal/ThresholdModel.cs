using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Models.Terminal
{
    public class ThresholdModel
    {
        /// <summary>
        /// 电压上限
        /// </summary>
        public double Upper_V { get; set; }
        /// <summary>
        /// 电压下限
        /// </summary>
        public double Lower_V { get; set; }
        /// <summary>
        /// 电流上限
        /// </summary>
        public double Upper_A { get; set; }
        /// <summary>
        /// 电流下限
        /// </summary>
        public double Lower_A { get; set; }
        /// <summary>
        /// 温度上限
        /// </summary>
        public double Upper_T { get; set; }
        /// <summary>
        /// 温度下限
        /// </summary>
        public double Lower_T { get; set; }
        /// <summary>
        /// 湿度上限
        /// </summary>
        public double Upper_H { get; set; }
    }
}
