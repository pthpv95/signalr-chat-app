using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat_services.Contracts;
using chat_services.Infrastructure.Helpers;
using chatservices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using chat_service.Contracts;
using chat_service.Services;

namespace chat_service.SignalR.Hubs
{
    [Authorize]
    public class ChatHub : Hub<INotify>
    {
        private readonly IMessageService _messageService;
        private readonly ICacheService _cacheService;
        private readonly IClaimsService _claimsService;
        private readonly INotificationService _notificationService;
        private readonly IHubContext<NotificationHub, INotify> _notificationHubContext;


        public ChatHub(
            IMessageService messageService,
            IClaimsService claimsService,
            ICacheService cacheService,
            INotificationService notificationService,
            IHubContext<NotificationHub, INotify> notificationHubContext)
        {
            _messageService = messageService;
            _claimsService = claimsService;
            _cacheService = cacheService;
            _notificationService = notificationService;
            _notificationHubContext = notificationHubContext;
        }

        public override async Task OnConnectedAsync()
        {
            var user = _claimsService.GetUserClaims();
            var key = CachingHelpers.BuildKey("Chat", user.Id);

            var userConnections = _cacheService.Get<List<string>>(key) ?? new List<string>();
            userConnections.Add(Context.ConnectionId);
            _cacheService.Set(key, userConnections);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _claimsService.GetUserClaims();
            var connectionIds = _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", user.Id)) ?? new List<string>();

            var keys = connectionIds.Where(x => x != Context.ConnectionId).ToList();
            _cacheService.Set(CachingHelpers.BuildKey("Chat", user.Id), new List<string> { });
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message, string fileUrl, Guid contactUserId, Guid conversationId)
        {
            var identity = _claimsService.GetUserClaims();
            var payload = new SendMessageRequestContract
            {
                SenderId = identity.Id,
                ContactUserId = contactUserId,
                Message = message,
                MessageType = string.IsNullOrEmpty(fileUrl) ? 0 : 1,
                AttachmentUrl = fileUrl,
                ConversationId = conversationId
            };

            var messageId = await _messageService.CreateMessageAsync(payload);
            payload.MessageId = messageId;
            var userConnectionIds = _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", identity.Id)) ?? new List<string>();
            var contactConnectionIds = _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", contactUserId)) ?? new List<string>();
            var connectionIds = userConnectionIds.Union(contactConnectionIds).ToList();

            await Clients.Clients(connectionIds).HasNewPrivateMessageAsync(payload);

            var unreadMessages = await _messageService.GetUnreadMessages(contactUserId);
            await _notificationHubContext.Clients.Clients(_cacheService.Get<List<string>>(CachingHelpers.BuildKey("Notification", contactUserId)) ?? new List<string>()).HasUnreadMessagesAsync(unreadMessages);
        }

        public async Task ReadMessage(Guid messageId, Guid contactUserId)
        {
            var identity = _claimsService.GetUserClaims();
            var contactConnectionIds = _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", contactUserId)) ?? new List<string>();
            var userConnectionIds = _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Notification", identity.Id)) ?? new List<string>();
            var messageRes = await _messageService.ReadMessage(messageId, identity.Id);
            var unreadMessages = await _messageService.GetUnreadMessages(identity.Id);

            await Clients.Clients(contactConnectionIds).ReceiveReadMessageAsync(messageRes);
            await _notificationHubContext.Clients.Clients(userConnectionIds).HasUnreadMessagesAsync(unreadMessages);
        }

        public async Task MessageTyping(Guid conversationId, Guid contactUserId)
        {
            var identity = _claimsService.GetUserClaims();
            var contactConnectionIds = _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", contactUserId)) ?? new List<string>();
            await Clients.Clients(contactConnectionIds).Typing(new TypingOnConversationContract
            {
                ConversationId = conversationId,
                ContactUserId = identity.Id
            });
        }

        public async Task MessageStopTyping(Guid conversationId, Guid contactUserId)
        {
            var identity = _claimsService.GetUserClaims();
            var contactConnectionIds = _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", contactUserId)) ?? new List<string>();

            await Clients.Clients(contactConnectionIds).StopTyping(new TypingOnConversationContract
            {
                ConversationId = conversationId,
                ContactUserId = identity.Id
            });
        }
    }
}


