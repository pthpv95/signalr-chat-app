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
    private ChatDbContext _context;
    public MessageService(ChatDbContext context)
    {
      _context = context;
    }

    public async Task<Guid> CreateMessageAsync(SendMessageRequestContract request)
    {
        var members = new Guid[] { request.SenderId, request.ContactUserId };
        var participantInConversations = await GetConversationOfParticipants(members);

        if (participantInConversations != null && participantInConversations.Count == 1)
        {
            var conversation = await _context.Set<Conversation>()
                                    .FirstOrDefaultAsync(c => c.Id == participantInConversations.First());

            var message = new Message(
              request.Message,
              request.SenderId,
              (MessageType)request.MessageType, 
              conversation.Id, 
              request.AttachmentUrl
            );

            await _context.AddAsync(message);
            await _context.SaveChangesAsync();
            return request.SenderId;
        }
        throw new Exception("Conversation is not existed");
    }

    private async Task<IList<Guid>> GetConversationOfParticipants(Guid[] members)
    {
        var participantInConversations = await _context.Set<Participant>()
                            .Where(p => members.Contains(p.UserId))
                            .GroupBy(x => x.ConversationId)
                            .Where(x => x.Count() > 1)
                            .Select(x => x.Key)
                            .ToListAsync();

        return participantInConversations;
    }

    public async Task<ConversationContract> GetPrivateConversationInfo(Guid userId, Guid contactUserId)
    {
        var members = new Guid[] { userId, contactUserId };
        var participantInConversations = await GetConversationOfParticipants(members);

        if (participantInConversations != null && participantInConversations.Count == 1)
        {
            var conversation = await _context.Set<Conversation>()
                            .FirstOrDefaultAsync(c => c.Id == participantInConversations.First());

        if (conversation == null)
        {
          throw new Exception("Conversation is not existed");
        }

        var messages = await _context.Set<Message>()
          .Where(m => m.ConversationId == conversation.Id)
          .OrderBy(x => x.Created)
          .Select(x => new MessageDetailsContract
          {
            Id = x.Id,
            Text = x.Text,
            AttachmentUrl = x.AttachmentUrl,
            SentAt = x.Created.ToString("HH:mm"),
            IsResponse = x.SenderId != userId,
            MessageType = (int)x.MessageType
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
        var defaultContact = await _context.Set<Contact>()
                    .SingleOrDefaultAsync(uc => uc.UserId == contactUserId);

        if(defaultContact == null)
        {
            throw new Exception("Contact is not existed.");
        }

        var title = defaultContact.FirstName + ' ' + defaultContact.LastName;

        var conversation = new Conversation(title, userId);

        var sender = new Participant(conversation.Id, ParticipanTypeEnum.Private, userId);
        var contact = new Participant(conversation.Id, ParticipanTypeEnum.Private, contactUserId);
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