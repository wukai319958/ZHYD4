using SFBR.Device.Domain.AggregatesModel.BrandAggregate;
using SFBR.Device.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFBR.Device.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DeviceContext _context;
        public BrandRepository(DeviceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Brand brand)
        {
            _context.Brands.Add(brand);
        }

        public void Delete(Brand brand)
        {
            _context.Brands.Remove(brand);
        }

        public async Task<Brand> GetAsync(string id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public void Patch(Brand brand)
        {
        }

        public void Update(Brand brand)
        {
            _context.Entry(brand).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
