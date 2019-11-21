using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using realtime_app.Contracts;
using realtime_app.Services;

namespace realtime_app.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Route("{userId}/contacts-suggestion")]
        public IActionResult GetSuggestedContacts([FromRoute] int userId)
        {
            return Ok(_contactService.GetContactSuggestions(userId));
        }

        [HttpPost]
        public IActionResult AddContact (RequestAddFriendContract request)
        {
            return Ok(_contactService.RequestAddContact(request));
        }

        [HttpPost]
        [Route("accept-friend-request")]
        public IActionResult AcceptFriendRequest (AcceptFriendRequest request)
        {
            return Ok(_contactService.AcceptFriendRequest(request));
        }
    }
}