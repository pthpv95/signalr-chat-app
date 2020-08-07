using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using chatservices.Contracts;
using realtime_app.Contracts;

namespace realtime_app.Services
{
    public interface IMessageService
    {
        Task<Guid> CreateMessageAsync(SendMessageRequestContract request);

        Task<ConversationContract> GetPrivateConversationInfo(Guid userId, Guid contactUserId);

        Task<MessageHasSeenReponseContract> ReadMessage(Guid id, Guid reveiverId);

        Task<int> GetUnreadMessages(Guid userId);
    }
}