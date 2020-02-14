using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.DomainServices
{
    public interface IDomainService
    {
        /// <summary>
        /// 配置设备权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="devices"></param>
        void SetDevicePower(string userId, List<AggregatesModel.DeviceAggregate.Device> devices);

        /// <summary>
        /// 回路挂载负载
        /// </summary>
        /// <param name="portNumber"></param>
        /// <param name="device"></param>
        /// <param name="load"></param>
        //void SetLoad(int portNumber, AggregatesModel.DeviceAggregate.Device device, AggregatesModel.LoadAggregate.Load load);
    }
}
