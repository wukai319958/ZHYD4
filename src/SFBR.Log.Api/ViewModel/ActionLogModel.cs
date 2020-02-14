using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.ViewModel
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class ActionLogModel
    {
        public string Id { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string ActionDesciption { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作员账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreationTime { get;  set; } 
    }
}
