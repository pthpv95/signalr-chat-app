using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using realtime_app.Common;
using realtime_app.Contracts;
using realtime_app.Services;
using realtime_app.SignalR.Hubs;

namespace realtime_app.Controllers
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
        [Route("contacts-suggestion")]
        public IActionResult GetSuggestedContacts()
        {
            var claims = _claimsService.GetUserClaims();
            var response = new ResponseMessage
            {
                Data = _contactService.GetContactSuggestions(Int32.Parse(claims.Id)),
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact (RequestAddFriendContract request)
        {
            var result = await _contactService.RequestAddContact(request);
            var response = new ResponseMessage
            {
                Data = result,
                IsSuccess = true
            };

            var connectionId = UserHandler.UserConnectionIds.First(x => Int32.Parse(x.UserId) == request.ReceiverId).ConnectionId;
            await _hubContext.Clients.Client(connectionId).SendAsync("reveiveContactRequest", "There's a friend request from " + request.RequesterId + " to " + request.ReceiverId);

            return Ok(response);
        }

        [HttpPost]
        [Route("accept-friend-request")]
        public IActionResult AcceptFriendRequest (AcceptFriendRequest request)
        {
            return Ok(_contactService.AcceptFriendRequest(request));
        }
    }
}