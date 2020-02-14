using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    public interface IDeviceTypeQueries
    {
        /// <summary>
        /// 根据主键获取设备类型详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DeviceType GetDeviceType(string id);
        /// <summary>
        /// 根据设备编号和型号获取类型轻轻
        /// </summary>
        /// <param name="code"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        DeviceType GetDeviceType(string code,string model);
    }
}
