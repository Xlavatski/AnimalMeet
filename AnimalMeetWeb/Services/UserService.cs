using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalMeetWeb.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _http;

        private ClaimsPrincipal User => _http.HttpContext?.User;

        public UserService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public string UserName
        {
            get
            {
                if(User is null)
                {
                    return string.Empty;
                }

                var userName = User.FindFirstValue(ClaimTypes.Name);

                if (string.IsNullOrEmpty(userName))
                {
                    return string.Empty;
                }

                return userName;
            }
        }
    }
}
