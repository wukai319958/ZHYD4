using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Models
{
    /// <summary>
    /// 逻辑操作返回结果
    /// </summary>
    public class LogicModel
    {
        public LogicModel()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public LogicModel(bool success,  string message = null, int code = 0,object data=null)
        {
            Success = success;
            Code = code;
            Message = message ;
            Data = data;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 代码，成功为0
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
