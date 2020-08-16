using System;
using System.Threading.Tasks;
using chatservices.Contracts;
using realtime_app.Contracts;

namespace realtime_app.Services
{
    public interface INotify
    {
        Task HasNewNotificationsAsync(int numberOfNoti);

        Task HasNewPrivateMessageAsync(SendMessageRequestContract message);

        Task ReceiveReadReadMessageAsync(MessageHasSeenReponseContract message);

        Task HasUnreadMessagesAsync(int unreadMessages);

        Task Typing(Guid conversationId);

        Task StopTyping(Guid conversationId);
    }
}
