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
  public class MessageService : IMessageService
  {
    private RealtimeAwesomeDbContext _context;
    public MessageService(RealtimeAwesomeDbContext context)
    {
      _context = context;
    }

    public async Task<Guid> CreateMessageAsync(SendMessageRequestContract request)
    {
      var conversationId = request.ConversationId;
      if (request.ConversationId == Guid.Empty)
      {
        var contactInfo = await _context.Set<Contact>()
          .FirstAsync(c => c.Id == request.ContactId);

        var conversation = new Conversation(contactInfo.FirstName, request.SenderId);
        conversationId = conversation.Id;

        var sender = new Participant(conversationId, ParticipanTypeEnum.Private, request.SenderId);
        var contact = new Participant(conversationId, ParticipanTypeEnum.Private, request.ContactId);
        await _context.AddRangeAsync(conversation, sender, contact);
      }

      var message = new Message(request.Message, request.SenderId, MessageType.Text, conversationId);
      await _context.AddAsync(message);
      await _context.SaveChangesAsync();

      return conversationId;
    }

    public async Task<ConversationContract> GetPrivateConversationInfo(Guid userId, Guid contactUserId, Guid conversationId)
    {
      var members = new Guid[] { userId, contactUserId };
      var hasMemberInConversation = await _context.Set<Participant>().AnyAsync(p => members.Contains(p.UserId));

      if (hasMemberInConversation)
      {
        var conversation = await _context.Set<Conversation>()
                  .FirstOrDefaultAsync(c => c.Id == conversationId);

        if (conversation == null)
        {
          throw new Exception("Conversation is not existed");
        }

        var messages = await _context.Set<Message>()
          .Where(m => m.ConversationId == conversationId)
          .OrderBy(x => x.Created)
          .Select(x => new MessageDetailsContract
          {
            Id = x.Id,
            Content = x.Text,
            SentAt = x.Created.ToString("h:mm tt"),
            IsResponse = x.SenderId != userId

          }).ToListAsync();

        return new ConversationContract()
        {
          Id = conversation.Id,
          Title = conversation.Title,
          Messages = messages
        };
      }
      else
      {
        var contactInfo = await (from c in _context.Set<Contact>()
                                 join u in _context.Set<User>()
                                 on c.UserId equals u.Id
                                 where u.Id == contactUserId
                                 select new UserContactContract
                                 {
                                   Id = c.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName
                                 }).FirstOrDefaultAsync();

        var title = contactInfo.FirstName + ' ' + contactInfo.LastName;

        var conversation = new Conversation(title, userId);

        var sender = new Participant(conversation.Id, ParticipanTypeEnum.Private, userId);
        var contact = new Participant(conversation.Id, ParticipanTypeEnum.Private, contactInfo.Id);
        await _context.AddRangeAsync(conversation, sender, contact);
        await _context.SaveChangesAsync();

        return new ConversationContract()
        {
          Id = conversation.Id,
          Title = title,
          Messages = new List<MessageDetailsContract>()
        };
      }
    }
  }
}