using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    public interface IDeviceRepository:SeedWork.IRepository<Device>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="device"></param>
        void Add(Device device);
        /// <summary>
        /// 全部更新
        /// </summary>
        /// <param name="device"></param>
        void Update(Device device);
        /// <summary>
        /// 只更新修改部分
        /// </summary>
        /// <param name="device"></param>
        void Patch(Device device);
        /// <summary>
        /// 根据主键获取设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Device> GetAsync(string id);
        /// <summary>
        /// 按需聚合
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<Device> GetAsync<TProperty>(string id, params Expression<Func<Device, TProperty>>[] includes);
        /// <summary>
        /// 根据设备编号获取设备
        /// </summary>
        /// <param name="equipNum"></param>
        /// <returns></returns>
        Task<Device> FindByEquipNumAsync(string equipNum);
        /// <summary>
        /// 按需聚合
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="equipNum"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<Device> FindByEquipNumAsync<TProperty>(string equipNum, params Expression<Func<Device, TProperty>>[] includes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipNum"></param>
        /// <returns></returns>
        Task<bool> ExistsByEquipNumAsync(string equipNum);

    }
}
