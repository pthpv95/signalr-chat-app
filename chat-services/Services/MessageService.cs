using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat_services.Contracts;
using chatservices.Contracts;
using chatservices.Db;
using chatservices.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using realtime_app.Contracts;
using realtime_app.Db;
using realtime_app.Models;
using System.Linq.Expressions;

namespace realtime_app.Services
{
    public class MessageService : IMessageService
    {
        private ChatDbContext _context;
        private readonly ChatDbConnection _chatDbConnection;
        public MessageService(ChatDbContext context, ChatDbConnection chatDbConnection)
        {
            _context = context;
            _chatDbConnection = chatDbConnection;
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

        private async Task<Conversation> GetConversationOfUser(Guid userId, Guid contactUserId)
        {
            var conversations = await _context.Set<Member>()
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .Select(c => c.ConversationId)
                .ToListAsync();

            var conversationOfThisUser = await _context.Set<Member>()
                .Where(m => conversations.Contains(m.ConversationId) && m.UserId == contactUserId)
                .AsNoTracking()
                .Select(m => m.Conversation)
                .FirstOrDefaultAsync();

            return conversationOfThisUser;
        }

        public async Task<PrivateMessagePaginationResponseContract> GetPrivateConversationInfo(PrivateMessagePaginationContract input)
        {
            var conversation = !input.ConversationId.HasValue ? await GetConversationOfUser(input.UserId, input.ContactUserId) :
                await _context.Set<Conversation>().SingleOrDefaultAsync(c => c.Id == input.ConversationId.Value);

            if (conversation != null)
            {
                var getMore = input.ConversationId.HasValue;

                Expression<Func<Message, bool>> predicate = m => m.ConversationId == conversation.Id;

                if(input.Cursor.HasValue)
                {
                    predicate = m => m.ConversationId == conversation.Id && m.Created < input.Cursor.Value;
                }

                var messages = await _context.Set<Message>()
                  .Where(predicate)
                  .OrderByDescending(m => m.Created)
                  .Select(x => new MessageDetailsContract
                  {
                      Id = x.Id,
                      Text = x.Text,
                      AttachmentUrl = x.AttachmentUrl,
                      SentAt = x.Created,
                      IsResponse = x.SenderId != input.UserId,
                      MessageType = (int)x.MessageType,
                      SentBy = x.SenderId,
                      Seen = false,
                  })
                  .Take(input.PageSize)
                  .ToListAsync();

                var readReceipt = await _context.Set<ReadReceipt>()
                                      .FirstOrDefaultAsync(x => x.ConversationId == conversation.Id);

                if (readReceipt != null)
                {
                    messages.ForEach(message =>
                    {
                        if (message.Id == readReceipt.MessageId)
                        {
                            message.Seen = true;
                        }
                    });
                }

                var conversationResponse = new ConversationContract()
                {
                    Id = conversation.Id,
                    Title = conversation.Title,
                    Messages = messages.OrderBy(m => m.SentAt).ToList()
                };

                DateTime? nextCursor = null;
                if(messages.Count >= input.PageSize)
                {
                    nextCursor = messages.Last().SentAt;
                }

                return new PrivateMessagePaginationResponseContract
                {
                    NextCursor = nextCursor,
                    Conversation = conversationResponse
                };
            }
            else
            {
                var defaultContact = await _context.Set<Contact>()
                            .SingleOrDefaultAsync(uc => uc.UserId == input.ContactUserId);

                var title = defaultContact?.FirstName + ' ' + defaultContact?.LastName;

                var newConversation = new Conversation(title, input.UserId, defaultContact.UserId);

                await _context.AddAsync(newConversation);
                await _context.SaveChangesAsync();
                var conversationReponse = new ConversationContract
                {
                    Id = newConversation.Id,
                    Title = title,
                    Messages = new List<MessageDetailsContract>()
                };

                return new PrivateMessagePaginationResponseContract
                {
                    NextCursor = null,
                    Conversation = conversationReponse
                };
            }
        }

        public async Task<MessageHasSeenResponseContract> ReadMessage(Guid id, Guid receiverId)
        {
            var message = await _context.Set<Message>()
                .Include(m => m.ReadReceipts)
                .SingleOrDefaultAsync(m => m.Id == id);

            try
            {
                if (message != null)
                {
                    var readReceipts = _context.Set<ReadReceipt>()
                    .Where(x => x.ConversationId == message.ConversationId && x.SeenerId == receiverId);

                    _context.Set<ReadReceipt>().RemoveRange(readReceipts);
                    message.Read(receiverId);

                    await _context.SaveChangesAsync();
                    return new MessageHasSeenResponseContract
                    {
                        MessageId = message.Id,
                        ConversationId = message.ConversationId,
                        SeenerId = receiverId
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
            var conversationIds = await _context.Set<Conversation>()
                .Include(x => x.Members)
                .Where(m => m.Members.Any(x => x.UserId == userId))
                .Select(c => c.Id)
                .ToListAsync();

            var readMessagesByConversation = await _context.Set<ReadReceipt>()
                .Where(x => conversationIds.Contains(x.ConversationId) && x.SeenerId == userId)
                .Select(x => x.MessageId)
                .ToListAsync();

            int unreadMessages = 0;

            foreach (var id in readMessagesByConversation)
            {
                var message = await _context.Set<Message>().FirstAsync(m => m.Id == id);
                var hasUnReadMessage = await _context.Set<Message>().AnyAsync(m => m.Created > message.Created);
                if(hasUnReadMessage){
                    unreadMessages++;
                }
            }

            return unreadMessages;
        }
    }
}