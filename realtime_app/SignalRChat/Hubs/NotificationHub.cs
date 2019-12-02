using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace realtime_app.SignalRChat.Hubs
{
    public class NotificationHub : Hub
    {
        // public async Task SendFriendRequest(string userId)
        // {
        //     await Clients.Client(Context.ConnectionId).SendAsync("message");
        // }
    }
}