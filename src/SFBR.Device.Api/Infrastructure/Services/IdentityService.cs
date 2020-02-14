using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserId()
        {
            return _context.HttpContext.User.FindFirst("Id").Value;
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.FindFirst("UserName").Value;
        }

        public string GetName()
        {
            return _context.HttpContext.User.FindFirst("Name").Value;
        }

        public bool IsDeveloper()
        {
            return "true".Equals(_context.HttpContext.User.FindFirst("IsDeveloper").Value, StringComparison.InvariantCultureIgnoreCase);
        }

        public List<string> GetRoles()
        {
            var claim = _context.HttpContext.User.Claims.FirstOrDefault(where=>where.Type == "Roles");
            if(claim != null && !string.IsNullOrEmpty(claim.Value) && "JSON".Equals( claim.ValueType, StringComparison.InvariantCultureIgnoreCase))
            {
                string jsonString = claim.Value.StartsWith("[") ? claim.Value : string.Concat("[", claim.Value, "]");
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Role>>(jsonString)?.Select(t => t.Name).ToList();
            }
            return null;
        }

        public bool CheckPwd(string pwd)
        {
            if (string.IsNullOrEmpty(pwd)) return false;
            return pwd.Equals(_context.HttpContext.User.FindFirst("Password").Value, StringComparison.InvariantCultureIgnoreCase);
        }

        public string GetPhone()
        {
            return _context.HttpContext.User.FindFirst("Phone").Value;
        }

        public string GetEmail()
        {
            return _context.HttpContext.User.FindFirst("Email").Value;
        }

        public string GetUserType()
        {
            return _context.HttpContext.User.FindFirst("UserType").Value;
        }

        public string GetTentantId()
        {
            return _context.HttpContext.User.FindFirst("CompanyId").Value;
        }

        private class Role
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
