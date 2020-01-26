using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using realtime_app.Contracts;
using realtime_app.Services;

namespace realtime_app.SignalR.Hubs
{
  [Authorize]
  public class ChatHub : Hub
  {
    private IMessageService _messageService;
    private IMemoryCache _cache;
    private readonly IClaimsService _claimsService;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ChatHub(IMessageService messageService, IClaimsService claimsService, IMemoryCache memoryCache, IWebHostEnvironment hostEnvironment)
    {
        _messageService = messageService;
        _claimsService = claimsService;
        _cache = memoryCache;
        _hostEnvironment = hostEnvironment;
    }

    public override async Task OnConnectedAsync()
    {
      var user = _claimsService.GetUserClaims();
      _cache.Set(user.Id, new List<string>{Context.ConnectionId});

      await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
      var user = _claimsService.GetUserClaims();
      var connectionIds = _cache.Get<List<string>>(user.Id);

      _cache.Set(user.Id, connectionIds.Remove(Context.ConnectionId));
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
            MessageType = String.IsNullOrEmpty(fileUrl) ? 0 : 1,
            AttachmentUrl = fileUrl
        };

        await _messageService.CreateMessageAsync(payload);
        var userConnectionIds = _cache.Get<List<string>>(identity.Id) ?? new List<string>();
        var contactConnectionIds = _cache.Get<List<string>>(contactUserId) ?? new List<string>();
        await Clients.Clients(userConnectionIds?.Concat(contactConnectionIds).ToList()).SendAsync("ReceiveMessage", payload);
    }
  }
}
