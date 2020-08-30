using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat_services.Contracts;
using chat_services.Infrastructure.Helpers;
using chatservices.Constants;
using chatservices.Contracts;
using chatservices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using realtime_app.Contracts;
using realtime_app.Models;
using realtime_app.Services;

namespace realtime_app.SignalR.Hubs
{
    [Authorize]
    public class ChatHub : Hub<INotify>
    {
        private readonly IMessageService _messageService;
        private readonly ICacheService _cacheService;
        private readonly IClaimsService _claimsService;
        private readonly IPubSub _pubSub;
        private readonly INotificationService _notificationService;

        public ChatHub(IMessageService messageService, IClaimsService claimsService, ICacheService cacheService, IPubSub pubSub, INotificationService notificationService)
        {
            _messageService = messageService;
            _claimsService = claimsService;
            _cacheService = cacheService;
            _pubSub = pubSub;
            _notificationService = notificationService;
        }

        public override async Task OnConnectedAsync()
        {
            var user = _claimsService.GetUserClaims();
            var key = CachingHelpers.BuildKey("Chat", user.Id);

            var userConnections = await _cacheService.Get<List<string>>(key) ?? new List<string>();
            userConnections.Add(Context.ConnectionId);
            await _cacheService.Set(key, userConnections);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _claimsService.GetUserClaims();
            var connectionIds = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", user.Id)) ?? new List<string>();

            var keys = connectionIds.Where(x => x != Context.ConnectionId).ToList();
            await _cacheService.Set(CachingHelpers.BuildKey("Chat", user.Id), new List<string> { });
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
            var userConnectionIds = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", identity.Id)) ?? new List<string>();
            var contactConnectionIds = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", contactUserId)) ?? new List<string>();
            var connectionIds = userConnectionIds.Union(contactConnectionIds).ToList();

            await _pubSub.Publish(Channels.PrivateMessageChannel,
                new
                PrivateMessageContract
                {
                    ConnectionIds = connectionIds,
                    NewMessage = payload
                });

            var unreadMessages = await _messageService.GetUnreadMessages(contactUserId);
            await _pubSub.Publish(Channels.NotififcationMessageChannel, new NewNotificationMessageContract
            {
                ActionType = NotificationActionType.UnreadMessage,
                ConnectionIds = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Notification", contactUserId)) ?? new List<string>(),
                TotalUnreadMessages = unreadMessages
            });
        }

        public async Task ReadMessage(Guid messageId, Guid contactUserId)
        {
            var identity = _claimsService.GetUserClaims();
            var contactConnectionIds = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", contactUserId)) ?? new List<string>();
            var userConnectionIds = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Notification", identity.Id)) ?? new List<string>();
            var messageRes = await _messageService.ReadMessage(messageId, identity.Id);
            var unreadMessages = await _messageService.GetUnreadMessages(identity.Id);

            await _pubSub.Publish(Channels.PrivateMessageChannel, new PrivateMessageContract
            {
                ActionType = PrivateMessageActionType.SeenMessage,
                ConnectionIds = contactConnectionIds,
                SeenMessage = messageRes
            });

            await _pubSub.Publish(Channels.NotififcationMessageChannel, new NewNotificationMessageContract
            {
                ActionType = NotificationActionType.UnreadMessage,
                ConnectionIds = userConnectionIds,
                TotalUnreadMessages = unreadMessages
            });
        }

        public async Task MessageTyping(Guid conversationId, Guid contactUserId)
        {
            var identity = _claimsService.GetUserClaims();
            var contactConnectionIds = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", contactUserId)) ?? new List<string>();

            await _pubSub.Publish(Channels.PrivateMessageChannel, new PrivateMessageContract
            {
                ActionType = PrivateMessageActionType.Typing,
                ConnectionIds = contactConnectionIds,
                TypingOnConversation = new TypingOnConversationContract
                {
                    ConversationId = conversationId,
                    ContactUserId = identity.Id
                }
            });
        }

        public async Task MessageStopTyping(Guid conversationId, Guid contactUserId)
        {
            var identity = _claimsService.GetUserClaims();
            var contactConnectionIds = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Chat", contactUserId)) ?? new List<string>();

            await _pubSub.Publish(Channels.PrivateMessageChannel, new PrivateMessageContract
            {
                ActionType = PrivateMessageActionType.StopTyping,
                ConnectionIds = contactConnectionIds,
                TypingOnConversation = new TypingOnConversationContract
                {
                    ConversationId = conversationId,
                    ContactUserId = identity.Id
                }
            });
        }
    }
}


