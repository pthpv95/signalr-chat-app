using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using chat_service.Common;
using chat_service.Contracts;
using chat_service.Services;
using chat_service.SignalR.Hubs;

namespace chat_service.Controllers
{
  [Authorize]
  [ApiController]
  [Route("contacts")]
  public class ContactsController : ControllerBase
  {
    private readonly IClaimsService _claimsService;
    private readonly IContactService _contactService;
    private readonly IHubContext<NotificationHub> _hubContext;

    public ContactsController(IContactService contactService, IHubContext<NotificationHub> hubContext, IClaimsService claimsService)
    {
      _contactService = contactService;
      _hubContext = hubContext;
      _claimsService = claimsService;
    }

    [HttpGet]
    [Route("suggestions")]
    public IActionResult GetSuggestedContacts()
    {
      var claims = _claimsService.GetUserClaims();
      var response = new ResponseMessage
      {
        Data = _contactService.GetContactSuggestions(claims.Id),
      };

      return Ok(response);
    }

    [HttpGet]
    [Route("requests")]
    public async Task<IActionResult> GetFriendRequests()
    {
      var claims = _claimsService.GetUserClaims();
      var data = await _contactService.GetFriendsRequests(claims.Id);

      var response = new ResponseMessage
      {
        Data = data
      };

      return Ok(response);
    }

    [HttpGet]
    [Route("user")]
    public async Task<IActionResult> GetUserContacts()
    {
      var claims = _claimsService.GetUserClaims();
      var contacts = await _contactService.GetUserContacts(claims.Id);
      var response = new ResponseMessage
      {
        Data = contacts
      };

      return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact(RequestAddFriendContract request)
    {
      var userClaims = _claimsService.GetUserClaims();

      request.RequesterId = userClaims.Id;
      var result = await _contactService.RequestAddContact(request);
      var response = new ResponseMessage
      {
        Data = result,
        IsSuccess = true
      };

    //   var connectionId = UserHandler.UserConnectionIds.First(x => x.UserId == userClaims.Id).ConnectionId;
    //   await _hubContext.Clients.Client(connectionId).SendAsync("reveiveContactRequest", "There's a friend request from " + request.RequesterId + " to " + request.ReceiverId);

      return Ok(response);
    }

    [HttpPost]
    [Route("{requestId}/accept-friend-request")]
    public async Task<IActionResult> AcceptFriendRequest([FromRoute] Guid requestId)
    {
        await _contactService.AcceptFriendRequest(_claimsService.GetUserClaims().Id, requestId);
        var response = new ResponseMessage
        {
            Data = "Success",
            IsSuccess = true
        };
        return Ok(response);
    }
  }
}