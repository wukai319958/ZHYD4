using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.Model
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class ActionLog
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get;private set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 操作员账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Paramaters { get; set; }
        /// <summary>
        /// api
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string ActionDesciption { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreationTime { get; private set; } = DateTime.UtcNow;
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime ActionTime { get; set; }
        /// <summary>
        /// 日志来源
        /// </summary>
        public string ApplicationContext { get; set; }
        /// <summary>
        /// 请求的类型
        /// </summary>
        public string ContentType { get; set; }
    }
}
