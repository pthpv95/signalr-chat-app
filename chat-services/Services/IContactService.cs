using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using chat_service.Contracts;

namespace chat_service.Services
{
    public interface IContactService
    {
         IList<UserContactContract> GetContactSuggestions (Guid id);

         Task<bool> RequestAddContact(RequestAddFriendContract contract);

         Task AcceptFriendRequest(Guid userId, Guid requestId);

         Task<IList<UserContactContract>> GetUserContacts(Guid userId);

         Task<IList<FriendRequestContract>> GetFriendsRequests(Guid userId);
    }
}