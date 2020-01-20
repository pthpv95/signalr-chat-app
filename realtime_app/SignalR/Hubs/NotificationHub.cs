using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using realtime_app.Services;

namespace realtime_app.SignalR.Hubs
{
    public class ConnectionUserIdentifierPair 
    {
        public string ConnectionId { get; set; }

        public int UserId { get; set; }
    }

    public static class UserHandler
    {
        public static List<ConnectionUserIdentifierPair> UserConnectionIds = new List<ConnectionUserIdentifierPair>();
    }

    [Authorize]
    public class NotificationHub : Hub<INotify>
    {
        private readonly IClaimsService _claimsService;

        public NotificationHub(IClaimsService claimsService)
        {
            _claimsService = claimsService;
        }

        public async Task DispatchNotifications()
        {
            await Clients.Client(Context.ConnectionId).DispatchNewNotification();
        }

        public override async Task OnConnectedAsync()
        {
            var user = _claimsService.GetUserClaims();

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _claimsService.GetUserClaims();
            await base.OnDisconnectedAsync(exception);
        }
    }
}