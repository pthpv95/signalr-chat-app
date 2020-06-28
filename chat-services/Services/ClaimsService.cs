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

            return new ClaimsContext
            {
                Id = new Guid(user.Claims.SingleOrDefault(c => c.Type == "chat_user_id")?.Value),
                UserName = user.Claims.SingleOrDefault(c => c.Type == "user_name")?.Value
            };
        }
    }
}