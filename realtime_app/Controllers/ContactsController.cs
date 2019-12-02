using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using realtime_app.Common;
using realtime_app.Contracts;
using realtime_app.Services;
using realtime_app.SignalRChat.Hubs;

namespace realtime_app.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ContactsController(IContactService contactService, IHubContext<NotificationHub> hubContext)
        {
            _contactService = contactService;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("{userId}/contacts-suggestion")]
        public IActionResult GetSuggestedContacts([FromRoute] int userId)
        {
            var response = new ResponseMessage
            {
                Data = _contactService.GetContactSuggestions(userId),
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

            await _hubContext.Clients.All.SendAsync("reveiveContactRequest", "There's a friend request from " + request.RequesterId + " to " + request.ReceiverId);

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