using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.ViewModel
{
    public class DataModel
    {
        /// <summary>
        /// 监测位置
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 数值
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 时间标准字符串
        /// </summary>
        public string CreationTimeString {
            get
            {
                if (CreationTime.Kind == DateTimeKind.Utc)
                {
                    return CreationTime.ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
                }
                else
                {
                    return CreationTime.ToString("yyyy-MM-ddTHH:mm:sszzzz", DateTimeFormatInfo.InvariantInfo);
                }
            }
        }
    }

    public class StatusModel
    {
        /// <summary>
        /// 数值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 时间标准字符串
        /// </summary>
        public string CreationTimeString
        {
            get
            {
                if (CreationTime.Kind == DateTimeKind.Utc)
                {
                    return CreationTime.ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
                }
                else
                {
                    return CreationTime.ToString("yyyy-MM-ddTHH:mm:sszzzz", DateTimeFormatInfo.InvariantInfo);
                }
            }
        }
    }
}
