using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Simple<T>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public T Data { get; set; }
    }
}
