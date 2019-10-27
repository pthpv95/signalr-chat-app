using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace realtime_app.SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("broadcastMessage", message);
        }
    }
}
