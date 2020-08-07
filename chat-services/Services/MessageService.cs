using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using chatservices.Contracts;
using chatservices.Models;
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
            var conversation = await _context.Set<Conversation>()
                    .SingleOrDefaultAsync(c => c.Id == request.ConversationId);

            if (conversation != null)
            {
                var message = new Message(
                  request.Message,
                  request.SenderId,
                  (MessageType)request.MessageType,
                  conversation.Id,
                  request.AttachmentUrl
                );

                await _context.AddAsync(message);
                await _context.SaveChangesAsync();
                return message.Id;
            }
            throw new Exception("Conversation is not existed");
        }

        private async Task<Conversation> GetConversationOfUser(Guid userId, Guid contactId)
        {
            return await _context.Set<Conversation>()
                    .Include(c => c.Members)
                    .Where(c => c.Members.Any(m => m.UserId == userId || m.UserId == contactId))
                    .SingleOrDefaultAsync();
        }


        public async Task<ConversationContract> GetPrivateConversationInfo(Guid userId, Guid contactUserId)
        {
            var conversation = await GetConversationOfUser(userId, contactUserId);

            if (conversation != null)
            {
                var messages = await _context.Set<Message>()
                  .Include(m => m.ReadReceipts)
                  .Where(m => m.ConversationId == conversation.Id)
                  .OrderBy(x => x.Created)
                  .Select(x => new MessageDetailsContract
                  {
                      Id = x.Id,
                      Text = x.Text,
                      AttachmentUrl = x.AttachmentUrl,
                      SentAt = x.Created.ToString("HH:mm"),
                      IsResponse = x.SenderId != userId,
                      MessageType = (int)x.MessageType,
                      Seen = x.ReadReceipts.Any()
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

                var title = defaultContact?.FirstName + ' ' + defaultContact?.LastName;

                var newConversation = new Conversation(title, userId, defaultContact.UserId);

                await _context.AddAsync(newConversation);
                await _context.SaveChangesAsync();
                return new ConversationContract
                {
                    Id = newConversation.Id,
                    Title = title,
                    Messages = new List<MessageDetailsContract>()
                };
            }
        }

        public async Task<MessageHasSeenReponseContract> ReadMessage(Guid id, Guid reveiverId)
        {
            var message = await _context.Set<Message>()
                .Include(m => m.ReadReceipts)
                .SingleOrDefaultAsync(m => m.Id == id);

            try
            {
                if (message != null)
                {
                    message.Read(reveiverId);
                    await _context.SaveChangesAsync();
                    return new MessageHasSeenReponseContract
                    {
                        MessageId = message.Id,
                        ConversationId = message.ConversationId,
                        SeenerId = reveiverId
                    };
                }
                else
                {
                    throw new Exception("Message not existed.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GetUnreadMessages(Guid userId)
        {
            var conversationIds = _context.Set<Conversation>()
                .Include(x => x.Members)
                .Where(m => m.Members.Any(x => x.UserId == userId))
                .Select(c => c.Id)
                .ToArray();

            var readMessagesByConversation = await _context.Set<ReadReceipt>()
                .Where(x => conversationIds.Contains(x.ConversationId))
                .Select(x => x.MessageId)
                .ToListAsync();

            int unreadMessages = 0;
            var reveivedMessages = await _context.Set<Message>()
              .Where(m => conversationIds.Contains(m.ConversationId) && m.SenderId != userId)
              .OrderBy(x => x.Created)
              .ToListAsync();

            var messageGroupByConversation = reveivedMessages.GroupBy(x => x.ConversationId);
            foreach (var item in messageGroupByConversation)
            {
                var lastestMessage = item.OrderBy(x => x.Created).Last();
                if (!readMessagesByConversation.Any(r => r == lastestMessage.Id))
                {
                    unreadMessages++;
                }
            }

            return unreadMessages;
        }
    }
}