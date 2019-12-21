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
                Id = user.Claims.SingleOrDefault(c => c.Type == "ChatUserId")?.Value,
                UserName = user.Claims.SingleOrDefault(c => c.Type == "UserName")?.Value
            };

            return context;
        }
    }
}