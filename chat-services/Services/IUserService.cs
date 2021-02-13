using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using realtime_app.Contracts;

namespace realtime_app.Services
{
    public interface IUserService
    {
         Task<UserContract> RegisterUserAsync(RegiserUserContract contract);

         Task<IList<UserContract>> GetSuggestedFriend(string userId);

         Task<UserContract> GetUserDetails(Guid id);
    }
}