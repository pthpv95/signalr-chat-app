using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using chatservices.Models;
using Microsoft.EntityFrameworkCore;
using chat_service.Contracts;
using chat_service.Db;
using chat_service.Models;

namespace chat_service.Services
{
    public class ContactService : IContactService
    {
        private ChatDbContext _context;

        public ContactService(ChatDbContext context)
        {
            _context = context;
        }

        public async Task AcceptFriendRequest(Guid userId, Guid requestId)
        {
            var friendRequest = await _context.Set<FriendsRequest>()
                .SingleOrDefaultAsync(f => f.Id == requestId);

            if (friendRequest == null)
            {
                return;
            }

            friendRequest.Status = FriendsRequestEnum.ACCEPTED;

            var requester = await _context.Set<User>().SingleAsync(u => u.Id == friendRequest.RequesterId);
            var user = await _context.Set<User>().SingleAsync(user => user.Id == userId);

            var receivedContact = await _context.Set<Contact>().SingleAsync(x => x.UserId == user.Id);
            var requesterContact = await _context.Set<Contact>().SingleAsync(x => x.UserId == requester.Id);

            var userContact1 = new UserContact(user.Id, requesterContact.Id);
            var userContact2 = new UserContact(requester.Id, receivedContact.Id);

            await _context.Set<UserContact>().AddRangeAsync(userContact1, userContact2);
            await _context.SaveChangesAsync();
        }

        public IList<UserContactContract> GetContactSuggestions(Guid id)
        {
            var currentUserContactIds = _context.Set<UserContact>()
                  .Where(c => c.UserId == id)
                  .Include(x => x.Contact)
                  .Select(x => x.Contact.UserId)
                  .ToList();

            var userReceivedFriendRequests = _context.Set<FriendsRequest>()
                .Any(fr => fr.ReceiverId == id);

            var friendRequestIds = _context.Set<FriendsRequest>()
                    .Where(x => x.Status == FriendsRequestEnum.PENDING && x.ReceiverId == id);
                    .Select(x => x.RequesterId)
                    .ToList();

            var suggestedContacts = _context.Set<User>()
                    .Where(u => u.Id != id
                        && !currentUserContactIds.Contains(u.Id)
                        && !friendRequestIds.Contains(u.Id))
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
            var friendRequestsQuery = from fr in _context.Set<FriendsRequest>()
                                      join u in _context.Set<User>()
                                      on fr.RequesterId equals u.Id
                                      where fr.Status == FriendsRequestEnum.PENDING && fr.ReceiverId == userId
                                      select new FriendRequestContract
                                      {
                                          Id = fr.Id,
                                          ContactId = u.Id,
                                          FirstName = u.FirstName,
                                          LastName = u.LastName
                                      };

            return await friendRequestsQuery.ToListAsync();
        }

        public async Task<IList<UserContactContract>> GetUserContacts(Guid userId)
        {
            var contacts = await _context.Set<UserContact>()
              .Include(x => x.Contact)
              .Where(uc => uc.UserId == userId)
              .ToListAsync();

            return contacts.Select(x => new UserContactContract
            {
                Id = x.Contact.Id,
                UserId = x.Contact.UserId,
                FirstName = x.Contact.FirstName,
                LastName = x.Contact.LastName,
            }).ToList();
        }

        public async Task<bool> RequestAddContact(RequestAddFriendContract contract)
        {
            var friendRequest = new FriendsRequest(contract.ReceiverId, contract.RequesterId);

            await _context.Set<FriendsRequest>().AddAsync(friendRequest);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}