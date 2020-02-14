using Microsoft.EntityFrameworkCore;
using SFBR.Device.Domain.AggregatesModel.UserAggregate;
using SFBR.Device.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFBR.Device.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        DeviceContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(DeviceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> GetAccountAsync(string account)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Account == account);
        }
    }
}
