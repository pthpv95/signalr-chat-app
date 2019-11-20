using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public async Task AcceptFriendRequest(AcceptFriendRequest request)
    {
      var friendRequest = await _context.Set<FriendsRequest>()
          .SingleOrDefaultAsync(f => f.Id == request.RequestId);

      if (friendRequest != null)
      {
        var requester = await _context.Set<User>().SingleOrDefaultAsync(u => u.Id == friendRequest.RequesterId);

        var contact = new Contact();
        contact.FirstName = requester.FirstName;
        contact.LastName = requester.LastName;

        var userContact = new UserContact();

        userContact.ContactId = contact.Id;
        userContact.UserId = request.UserId;

        friendRequest.Status = FriendsRequestStatus.ACCEPPTED;
        await _context.Set<Contact>().AddAsync(contact);
        await _context.Set<UserContact>().AddAsync(userContact);
        await _context.SaveChangesAsync();
      }
    }

    public IList<ContactSuggestionsContract> GetContactSuggestions(int userId)
    {
      var currentUserContactIds = _context.Set<UserContact>()
          .Where(c => c.UserId == userId)
          .Select(x => x.ContactId);

      var suggestedContacts = _context.Set<User>()
          .Where(u => !currentUserContactIds.Contains(u.Id) && u.Id != userId)
          .Select(x => new ContactSuggestionsContract
          {
            ContactId = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName
          }).ToList();

      return suggestedContacts;
    }

    public async Task RequestAddContact(RequestAddFriendContract contract)
    {
      var friendRequest = new FriendsRequest(contract.ReceiverId, contract.RequesterId);
      friendRequest.Status = FriendsRequestStatus.PENDING;

      await _context.Set<FriendsRequest>().AddAsync(friendRequest);
      await _context.SaveChangesAsync();
    }
  }
}