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

        public string UserId { get; set; }
    }

    public static class UserHandler
    {
        public static List<ConnectionUserIdentifierPair> UserConnectionIds = new List<ConnectionUserIdentifierPair>();
    }

    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly IClaimsService _claimsService;

        public NotificationHub(IClaimsService claimsService)
        {
            _claimsService = claimsService;
        }

        public override async Task OnConnectedAsync()
        {
            var user = _claimsService.GetUserClaims();

            UserHandler.UserConnectionIds.Add(new ConnectionUserIdentifierPair(){
                UserId = user.Id,
                ConnectionId = Context.ConnectionId
            });
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _claimsService.GetUserClaims();
            UserHandler.UserConnectionIds.RemoveAll(x => x.UserId == user.Id);
            await base.OnDisconnectedAsync(exception);
        }
    }
}