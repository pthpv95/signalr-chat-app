using System;
using System.Threading.Tasks;
using chat_services.Contracts;
using chatservices.Contracts;
using chat_service.Contracts;

namespace chat_service.Services
{
    public interface INotify
    {
        Task HasNewNotificationsAsync(int numberOfNoti);

        Task HasNewPrivateMessageAsync(SendMessageRequestContract message);

        Task ReceiveReadMessageAsync(MessageHasSeenResponseContract message);

        Task HasUnreadMessagesAsync(int unreadMessages);

        Task Typing(TypingOnConversationContract data);

        Task StopTyping(TypingOnConversationContract data);
    }
}
