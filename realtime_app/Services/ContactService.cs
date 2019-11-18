using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using realtime_app.Contracts;
using realtime_app.Db;
using realtime_app.Models;

namespace realtime_app.Services
{
  public class ContactService : IContactService
  {
    private RealtimeAwesomeDbContext _context;
    public ContactService(RealtimeAwesomeDbContext context)
    {
        _context = context;
    }

    public IList<ContactSuggestionsContract> GetContactSuggestions(int userId)
    {
        var currentUserContactIds = _context.Set<UserContact>()
            .Where(c => c.UserId == userId)
            .Select(x => x.ContactId);

        var suggestedContacts = _context.Set<User>()
            .Where(u => !currentUserContactIds.Contains(u.Id))
            .Select(x => new ContactSuggestionsContract
            {
                ContactId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();

        return suggestedContacts;
    }

    public Task RequestAddContact(string requesterId)
    {
      throw new System.NotImplementedException();
    }
  }
}