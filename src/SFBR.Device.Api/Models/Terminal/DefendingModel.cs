using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Models.Terminal
{
    /// <summary>
    /// 撤防配置
    /// </summary>
    public class DefendingModel
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 开始时间：月日时分（01-12 08:33）
        /// </summary>
        public string Start { get; set; }
        /// <summary>
        /// 结束时间：格式同开始时间
        /// </summary>
        public string End { get; set; }
    }
}
