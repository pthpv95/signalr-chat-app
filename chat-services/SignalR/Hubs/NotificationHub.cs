using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat_services.Infrastructure.Helpers;
using chatservices.Constants;
using chatservices.Contracts;
using chatservices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using realtime_app.Services;

namespace realtime_app.SignalR.Hubs
{
    [Authorize]
    public class NotificationHub : Hub<INotify>
    {
        private readonly IClaimsService _claimsService;
        private readonly INotificationService _notificationService;
        private readonly IMessageService _messageService;
        private readonly ICacheService _cacheService;
        private readonly IPubSub _pubSub;

        public NotificationHub(
            IClaimsService claimsService, 
            INotificationService notificationService, 
            IMessageService messageService,
            ICacheService cacheService,
            IPubSub pubSub)
        {
            _claimsService = claimsService;
            _notificationService = notificationService;
            _messageService = messageService;
            _cacheService = cacheService;
            _pubSub = pubSub;
        }

        public async Task DispatchNewNotificationAsync(int numberOfNoti)
        {
            await Clients.Client(Context.ConnectionId).HasNewNotificationsAsync(numberOfNoti);
        }

        public async Task DispatchUnreadMessagesAsync(int totalMessages)
        {
            await Clients.Client(Context.ConnectionId).HasUnreadMessagesAsync(totalMessages);
        }

        public override async Task OnConnectedAsync()
        {
            var user = _claimsService.GetUserClaims();
            var userConnections = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Notification", user.Id)) ?? new List<string>();

            userConnections.Add(Context.ConnectionId);
            await _cacheService.Set(CachingHelpers.BuildKey("Notification", user.Id), userConnections);

            var numOfNotifications = await _notificationService.GetUserNumberOfNotifications(user.Id);
            var unreadMessages = await _messageService.GetUnreadMessages(user.Id);

            Task.WaitAll(new Task[]
            {
                _pubSub.Publish(Channels.NotififcationMessageChannel, new NewNotificationMessageContract
                {
                    ActionType = NotificationActionType.UnreadMessage,
                    ConnectionIds = userConnections,
                    TotalUnreadMessages = unreadMessages
                }),
                _pubSub.Publish(Channels.NotififcationMessageChannel, new NewNotificationMessageContract
                {
                    ActionType = NotificationActionType.NewNoti,
                    ConnectionIds = userConnections,
                    TotalUnreadMessages = numOfNotifications
                }),
            });

            await base.OnConnectedAsync();
        }

        public async Task MessageTyping()
        {
            await Clients.All.Typing(Guid.NewGuid());
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _claimsService.GetUserClaims();
            var connectionIds = await _cacheService.Get<List<string>>(CachingHelpers.BuildKey("Notification", user.Id)) ?? new List<string>();

            var ids = connectionIds.Where(x => x != Context.ConnectionId).ToList();
            await _cacheService.Set(CachingHelpers.BuildKey("Notification", user.Id), ids);
      
            await base.OnDisconnectedAsync(exception);
        }
    }
}