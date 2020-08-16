using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatservices.Contracts;
using chatservices.Db;
using chatservices.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using realtime_app.Contracts;
using realtime_app.Db;
using realtime_app.Models;

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

        public async Task<ConversationContract> GetPrivateConversationInfo(Guid userId, Guid contactUserId)
        {
            var conversation = await GetConversationOfUser(userId, contactUserId);

            if (conversation != null)
            {
                var messages = await _context.Set<Message>()
                  .Where(m => m.ConversationId == conversation.Id)
                  .OrderBy(m => m.Created)
                  .Select(x => new MessageDetailsContract
                  {
                      Id = x.Id,
                      Text = x.Text,
                      AttachmentUrl = x.AttachmentUrl,
                      SentAt = x.Created.ToString("HH:mm"),
                      IsResponse = x.SenderId != userId,
                      MessageType = (int)x.MessageType,
                      SentBy = x.SenderId,
                      Seen = false
                  }).ToListAsync();

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
                    var readReceipts = _context.Set<ReadReceipt>()
                    .Where(x => x.ConversationId == message.ConversationId);

                    _context.Set<ReadReceipt>().RemoveRange(readReceipts);
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
            var conversationIds = await _context.Set<Conversation>()
                .Include(x => x.Members)
                .Where(m => m.Members.Any(x => x.UserId == userId))
                .Select(c => c.Id)
                .ToListAsync();

            var readMessagesByConversation = await _context.Set<ReadReceipt>()
                .Where(x => conversationIds.Contains(x.ConversationId))
                .Select(x => x.MessageId)
                .ToListAsync();

            int unreadMessages = 0;

            using (var conn = _chatDbConnection.Connection)
            {
                var lastestMessagesByConversationQuery = $@"
                    WITH ranked_messages AS (SELECT m.*, ROW_NUMBER() OVER (PARTITION BY conversationid ORDER BY created DESC) AS rn FROM Messages AS m)
                    SELECT * FROM ranked_messages WHERE rn = 1 && ConversationId IN @conversationIds && SenderId <> @userId";

                var lastestMessages = await conn.QueryAsync<Message>(lastestMessagesByConversationQuery, new
                {
                    conversationIds,
                    userId
                });

                foreach (var message in lastestMessages)
                {
                    if (!readMessagesByConversation.Any(r => r == message.Id))
                    {
                        unreadMessages++;
                    }
                }
            }

            return unreadMessages;
        }
    }
}