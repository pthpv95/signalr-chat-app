using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

    public ChatHub(IMessageService messageService, IClaimsService claimsService, IMemoryCache memoryCache)
    {
        _messageService = messageService;
        _claimsService = claimsService;
        _cache = memoryCache;
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

    public async Task SendMessage(string message, Guid contactId, Guid conversationId)
    {
      var identity = _claimsService.GetUserClaims();
      var payload = new SendMessageRequestContract 
      {
          SenderId = identity.Id,
          Message = message,
          ConversationId = conversationId,
          ContactId = contactId
      };

      var messageRes = await _messageService.CreateMessageAsync(payload);
      var userConnectionIds = _cache.Get<List<string>>(contactId);
      
      await Clients.All.SendAsync("ReceiveMessage", message);
      // foreach (var connectionId in userConnectionIds)
      // {
      //   await Clients.Client(connectionId).SendAsync("broadcastMessage", message);
      // }
    }

    // public async Task SendMessage(string user, string message)
    // {
    //   await Clients.All.SendAsync("ReceiveMessage", user, message);
    // }
  }
}
