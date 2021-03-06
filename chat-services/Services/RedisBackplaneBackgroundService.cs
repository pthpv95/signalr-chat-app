﻿using System.Threading;
using System.Threading.Tasks;
using chatservices.Constants;
using chatservices.Contracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using realtime_app.Services;
using realtime_app.SignalR.Hubs;

namespace chatservices.Services
{
    public class RedisBackplaneBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RedisBackplaneBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var notiHubContext = scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub, INotify>>();
                var chatHubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ChatHub, INotify>>();
                var pubsub = scope.ServiceProvider.GetRequiredService<IPubSub>();

                await pubsub.Subscribe<NewNotificationMessageContract>(Channels.NotififcationMessageChannel, async (payload) =>
                {
                    switch (payload.ActionType)
                    {
                        case NotificationActionType.NewNoti:
                            await notiHubContext.Clients.Clients(payload.ConnectionIds).HasNewNotificationsAsync(payload.TotalNoti);
                            break;

                        case NotificationActionType.UnreadMessage:
                            await notiHubContext.Clients.Clients(payload.ConnectionIds).HasUnreadMessagesAsync(payload.TotalUnreadMessages);
                            break;

                        default:
                            break;
                    }
                });

                await pubsub.Subscribe<PrivateMessageContract>(Channels.PrivateMessageChannel, async (payload) =>
                {
                    switch (payload.ActionType)
                    {
                        case PrivateMessageActionType.NewMessage:
                            await chatHubContext.Clients.Clients(payload.ConnectionIds).HasNewPrivateMessageAsync(payload.NewMessage);
                            break;

                        case PrivateMessageActionType.SeenMessage:
                            await chatHubContext.Clients.Clients(payload.ConnectionIds).ReceiveReadMessageAsync(payload.SeenMessage);
                            break;

                        case PrivateMessageActionType.Typing:
                            await chatHubContext.Clients.Clients(payload.ConnectionIds).Typing(payload.TypingOnConversation);
                            break;

                        case PrivateMessageActionType.StopTyping:
                            await chatHubContext.Clients.Clients(payload.ConnectionIds).StopTyping(payload.TypingOnConversation);
                            break;

                        default:
                            break;
                    }
                });
            }
        }
    }
}
