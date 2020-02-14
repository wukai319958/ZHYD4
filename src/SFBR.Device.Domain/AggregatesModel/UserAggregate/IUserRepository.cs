using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFBR.Device.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository:SeedWork.IRepository<User>
    {
        Task<User> GetAccountAsync(string account);
    }
}
