using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using realtime_app.Services;

namespace realtime_app.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private IMessageService _messageService;
        
        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Send(string message)
        {
            string name = Context.User.Identity.Name;
            await _messageService.CreateMessageAsync(message, 1, 1);
            await Clients.All.SendAsync("broadcastMessage", message);
        }
    }
}
