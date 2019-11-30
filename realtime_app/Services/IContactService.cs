using System.Collections.Generic;
using System.Threading.Tasks;
using realtime_app.Contracts;

namespace realtime_app.Services
{
    public interface IContactService
    {
         IList<ContactSuggestionsContract> GetContactSuggestions (int userId);

         Task<string> RequestAddContact(RequestAddFriendContract contract);

         Task AcceptFriendRequest(AcceptFriendRequest request);
    }
}