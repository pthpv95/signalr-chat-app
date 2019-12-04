using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

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

    public class NotificationHub : Hub
    {
        
        // public async Task SendFriendRequest(string userId)
        // {
        //     await Clients.Client(Context.ConnectionId).SendAsync("message");
        // }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            UserHandler.UserConnectionIds.Add(new ConnectionUserIdentifierPair(){
                UserId = userId,
                ConnectionId = Context.ConnectionId
            });
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();
            UserHandler.UserConnectionIds.RemoveAll(x => x.ConnectionId == Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}