using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using chat_service.Contracts;

namespace chat_service.Services
{
    public interface IUserService
    {
         Task<UserContract> RegisterUserAsync(RegiserUserContract contract);

         Task<IList<UserContract>> GetSuggestedFriend(string userId);

         Task<UserContract> GetUserDetails(Guid id);
    }
}