using Microsoft.EntityFrameworkCore;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using SFBR.Device.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace SFBR.Device.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceContext _context;
        public DeviceRepository(DeviceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Domain.AggregatesModel.DeviceAggregate.Device device)
        {
            _context.Devices.Add(device);
        }

        public async Task<Domain.AggregatesModel.DeviceAggregate.Device> FindByEquipNumAsync(string equipNum)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(where => where.EquipNum == equipNum);
            if (device != null)
            {
                if (device is Terminal)
                {
                    var temp = device as Terminal;
                    await _context.Entry(temp).Reference(d => d.Location).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Loads).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Parts).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Sensors).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Controllers).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.DevFunctions).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.DeviceAlarms).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.TimedTasks).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Channels).LoadAsync();
                }
            }
            return device;
        }

        public async Task<Domain.AggregatesModel.DeviceAggregate.Device> FindByEquipNumAsync<TProperty>(string equipNum, params Expression<Func<Domain.AggregatesModel.DeviceAggregate.Device, TProperty>>[] includes)
        {
            if(includes != null)
            {
                foreach (var item in includes)
                {
                    _context.Devices.Include(item);
                }
            }
            return await _context.Devices.FirstOrDefaultAsync(where => where.EquipNum == equipNum);
        }


        public async Task<Domain.AggregatesModel.DeviceAggregate.Device> GetAsync(string id)
        {
            var device = await _context.Devices.FindAsync(id);
            if(device != null)
            {
                if(device is Terminal)
                {
                    var temp = device as Terminal;
                    await _context.Entry(temp).Reference(d => d.Location).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Loads).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Parts).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Sensors).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Controllers).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.DevFunctions).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.DeviceAlarms).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.TimedTasks).LoadAsync();
                    await _context.Entry(temp).Collection(d => d.Channels).LoadAsync();
                }
            }
            return device;
        }

        public async Task<Domain.AggregatesModel.DeviceAggregate.Device> GetAsync<TProperty>(string id, params Expression<Func<Domain.AggregatesModel.DeviceAggregate.Device, TProperty>>[] includes)
        {
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    _context.Devices.Include(item);
                }
            }
            return await _context.Devices.FindAsync(id);
        }


        public void Update(Domain.AggregatesModel.DeviceAggregate.Device device)
        {
            _context.Entry(device).State = EntityState.Modified;//全部更新
        }

        public void Patch(Domain.AggregatesModel.DeviceAggregate.Device device)
        {
            //什么都不做，默认只更新修改部分，前提是启用了状态跟踪
        }

        public void Delete(Domain.AggregatesModel.DeviceAggregate.Device device)
        {
            _context.Devices.Remove(device);
        }

        public async Task<bool> ExistsByEquipNumAsync(string equipNum)
        {
            return await _context.Devices.FirstOrDefaultAsync(where => where.EquipNum == equipNum) != null;
        }
    }
}
