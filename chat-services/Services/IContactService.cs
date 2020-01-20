using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using realtime_app.Contracts;

namespace realtime_app.Services
{
    public interface IContactService
    {
         IList<UserContactContract> GetContactSuggestions (Guid id);

         Task<string> RequestAddContact(RequestAddFriendContract contract);

         Task AcceptFriendRequest(Guid userId, Guid requestId);

        Task<IList<UserContactContract>> GetUserContacts(Guid userId);

        Task<IList<FriendRequestContract>> GetFriendsRequests(Guid userId);
    }
}