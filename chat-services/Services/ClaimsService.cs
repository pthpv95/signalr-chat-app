using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using realtime_app.Contracts;

namespace realtime_app.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsContext GetUserClaims()
        {
            var user = _httpContextAccessor.HttpContext.User;

            var context = new ClaimsContext
            {
                Id = new Guid(user.Claims.SingleOrDefault(c => c.Type == "chatUserId")?.Value),
                UserName = user.Claims.SingleOrDefault(c => c.Type == "userName")?.Value
            };

            return context;
        }
    }
}