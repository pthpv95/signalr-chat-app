using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using chat_services.Contracts;
using chatservices.Contracts;
using chat_service.Contracts;

namespace chat_service.Services
{
    public interface IMessageService
    {
        Task<Guid> CreateMessageAsync(SendMessageRequestContract request);

        Task<PrivateMessagePaginationResponseContract> GetPrivateConversationInfo(PrivateMessagePaginationContract input);

        Task<MessageHasSeenResponseContract> ReadMessage(Guid id, Guid reveiverId);

        Task<int> GetUnreadMessages(Guid userId);
    }
}