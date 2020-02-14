using SFBR.Device.Domain.AggregatesModel.RegionAggregate;
using SFBR.Device.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFBR.Device.Infrastructure.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly DeviceContext _context;
        public RegionRepository(DeviceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Region region)
        {
            _context.Regions.Add(region);
        }

        public async Task<Region> GetAsync(string id)
        {
            return await _context.Regions.FindAsync(id);
        }

        public void Patch(Region region)
        {
        }
        public void Delete(Region region)
        {
            _context.Regions.Remove(region);
        }
        public void Update(Region region)
        {
            _context.Entry(region).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
