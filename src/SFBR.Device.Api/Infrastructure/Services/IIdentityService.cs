using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Infrastructure.Services
{
    public interface IIdentityService
    {
        string GetUserId();
        string GetUserName();

        string GetName();

        bool IsDeveloper();

        List<string> GetRoles();

        bool CheckPwd(string pwd);
        string GetPhone();
        string GetEmail();
        string GetUserType();

        string GetTentantId();
    }
}
