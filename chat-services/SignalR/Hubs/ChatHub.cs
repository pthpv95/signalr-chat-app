using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatservices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using realtime_app.Contracts;
using realtime_app.Services;

namespace realtime_app.SignalR.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly ICacheService _cacheService;
        private readonly IClaimsService _claimsService;

        public ChatHub(IMessageService messageService, IClaimsService claimsService, ICacheService cacheService)
        {
            _messageService = messageService;
            _claimsService = claimsService;
            _cacheService = cacheService;
        }

        public override async Task OnConnectedAsync()
        {
            var user = _claimsService.GetUserClaims();
            var userConnections = await _cacheService.Get<List<string>>(BuildKey(user.Id)) ?? new List<string>();
            userConnections.Add(Context.ConnectionId);
            await _cacheService.Set(BuildKey(user.Id), userConnections);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _claimsService.GetUserClaims();
            var connectionIds = await _cacheService.Get<List<string>>(BuildKey(user.Id)) ?? new List<string>();

            var keys = connectionIds.Where(x => x != Context.ConnectionId).ToList();
            await _cacheService.Set(BuildKey(user.Id), keys);
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
            var userConnectionIds = await _cacheService.Get<List<string>>(BuildKey(identity.Id)) ?? new List<string>();
            var contactConnectionIds = await _cacheService.Get<List<string>>(BuildKey(contactUserId)) ?? new List<string>();
            await Clients.Clients(userConnectionIds.Concat(contactConnectionIds).ToList()).SendAsync("ReceiveMessage", payload);
        }

        private string BuildKey(Guid key) => $"OT_{key}";
    }
}
