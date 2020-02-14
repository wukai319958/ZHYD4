using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate
{
    public interface IDeviceTypeRepository:SeedWork.IRepository<DeviceType>
    {


        void Update(DeviceType deviceType);//

        Task<DeviceType> FindAsync(string code,string model);
        
    }
}
