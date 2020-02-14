using Microsoft.EntityFrameworkCore;
using SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate;
using SFBR.Device.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFBR.Device.Infrastructure.Repositories
{
    public class DeviceTypeRepository : IDeviceTypeRepository
    {
        DeviceContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public DeviceTypeRepository(DeviceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DeviceType> FindAsync(string code,string model)
        {
            var temp = await _context.DeviceTypes.FirstOrDefaultAsync(where => where.Code == code && where.Model == model);
            if(temp != null)
            {
                await _context.Entry(temp).Collection(d => d.Parts).LoadAsync();
                await _context.Entry(temp).Collection(d => d.Sensors).LoadAsync();
                await _context.Entry(temp).Collection(d => d.Controllers).LoadAsync();
                await _context.Entry(temp).Collection(d => d.Functions).LoadAsync();
                await _context.Entry(temp).Collection(d => d.Alarms).LoadAsync();
                await _context.Entry(temp).Collection(d => d.Channels).LoadAsync();
            }
            return temp;
        }

        public void Update(DeviceType deviceType)
        {
            
        }
    }
}
