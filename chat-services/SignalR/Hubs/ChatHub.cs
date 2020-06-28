using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatservices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using realtime_app.Contracts;
using realtime_app.Services;

namespace realtime_app.SignalR.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private IMessageService _messageService;
        private readonly ICacheService _cacheService;
        private readonly IClaimsService _claimsService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ChatHub(IMessageService messageService, IClaimsService claimsService, ICacheService cacheService, IWebHostEnvironment hostEnvironment)
        {
            _messageService = messageService;
            _claimsService = claimsService;
            _cacheService = cacheService;
            _hostEnvironment = hostEnvironment;
        }

        public override async Task OnConnectedAsync()
        {
            var user = _claimsService.GetUserClaims();
            await _cacheService.Set(user.Id.ToString(), new List<string> { Context.ConnectionId });

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _claimsService.GetUserClaims();
            var connectionIds = await _cacheService.Get<List<string>>(user.Id.ToString());

            await _cacheService.Set(user.Id.ToString(), connectionIds.Remove(Context.ConnectionId));
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message, string fileUrl, Guid contactUserId)
        {
            var identity = _claimsService.GetUserClaims();
            var payload = new SendMessageRequestContract
            {
                SenderId = identity.Id,
                ContactUserId = contactUserId,
                Message = message,
                MessageType = string.IsNullOrEmpty(fileUrl) ? 0 : 1,
                AttachmentUrl = fileUrl
            };

            await _messageService.CreateMessageAsync(payload);
            var userConnectionIds = await _cacheService.Get<List<string>>(identity.Id.ToString());
            var contactConnectionIds = await _cacheService.Get<List<string>>(contactUserId.ToString());
            await Clients.Clients(userConnectionIds.Concat(contactConnectionIds).ToList()).SendAsync("ReceiveMessage", payload);
        }
    }
}
