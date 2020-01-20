using System;
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

    public async Task AcceptFriendRequest(Guid userId, Guid requestId)
    {
      var friendRequest = await _context.Set<FriendsRequest>()
          .SingleOrDefaultAsync(f => f.RequesterId == requestId);

      if (friendRequest == null)
      {
        return;
      }

      var requester = await _context.Set<User>().SingleOrDefaultAsync(u => u.Id == friendRequest.RequesterId);

      var contact = new Contact(friendRequest.RequesterId, requester.FirstName, requester.LastName);

      var userContact = new UserContact(userId, contact.Id);
      

      friendRequest.Status = FriendsRequestEnum.ACCEPTED;
      await _context.Set<Contact>().AddAsync(contact);
      await _context.Set<UserContact>().AddAsync(userContact);
      await _context.SaveChangesAsync();
    }

    public IList<UserContactContract> GetContactSuggestions(Guid id)
    {
      var currentUserContactIds = _context.Set<UserContact>()
          .Where(c => c.UserId == id)
          .Select(x => x.ContactId)
          .ToList();

      var requestedAddFriendUserIds = _context.Set<FriendsRequest>()
          .Where(x => x.RequesterId == id && x.Status == FriendsRequestEnum.PENDING)
          .Select(x => x.RecieverId)
          .ToList();

      var suggestedContacts = _context.Set<User>()
          .Where(u => u.Id != id
            && !currentUserContactIds.Contains(u.Id)
            && !requestedAddFriendUserIds.Contains(u.Id))
          .Select(x => new UserContactContract
          {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName
          }).ToList();

      return suggestedContacts;
    }

    public async Task<IList<FriendRequestContract>> GetFriendsRequests(Guid userId)
    {
      var friendRequestsQuery =
        from fr in _context.Set<FriendsRequest>()
        join u in _context.Set<User>()
        on fr.RequesterId equals u.Id
        where fr.RecieverId == userId && fr.Status == FriendsRequestEnum.PENDING
        select new FriendRequestContract { Id = fr.Id, ContactId = u.Id, FirstName = u.FirstName, LastName = u.LastName };

      return await friendRequestsQuery.ToListAsync();
    }

    public async Task<IList<UserContactContract>> GetUserContacts(Guid userId)
    {
      var contacts = await _context.Set<UserContact>()
        .Include(x => x.Contact)
        .Where(uc => uc.UserId == userId)
        .ToListAsync();

      var partipants = _context.Set<Participant>()
        .Where(p => p.UserId != userId)
        .Select(x => new 
        {
          ContactId = x.UserId,
          ConversationId = x.ConversationId
        }).ToList();


      return contacts.Select(x => new UserContactContract
      {
        Id = x.Contact.Id,
        FirstName = x.Contact.FirstName,
        LastName = x.Contact.LastName,
        ConversationId = partipants.FirstOrDefault(p => p.ContactId == x.ContactId)?.ConversationId
      }).ToList();
    }

    public async Task<string> RequestAddContact(RequestAddFriendContract contract)
    {
      var friendRequest = new FriendsRequest(contract.ReceiverId, contract.RequesterId);

      await _context.Set<FriendsRequest>().AddAsync(friendRequest);
      await _context.SaveChangesAsync();

      return "Successful";
    }
  }
}