using System;
using System.Threading.Tasks;
using chat_services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using chat_service.Common;
using chat_service.Contracts;
using chat_service.Services;

namespace chat_service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IClaimsService _claimsService;

        public MessageController(IMessageService messageService, IClaimsService claimsService)
        {
            _messageService = messageService;
            _claimsService = claimsService;
        }

        [HttpPost]
        [Route("contact")]
        public async Task<IActionResult> GetConversationInfo([FromBody] PrivateMessagePaginationContract contract)
        {
            var input = new PrivateMessagePaginationContract
            {
                Cursor = contract.Cursor,
                UserId = _claimsService.GetUserClaims().Id,
                ContactUserId = contract.ContactUserId,
                ConversationId = contract.ConversationId,
                PageSize = contract.PageSize
            };
            var conversation = await _messageService.GetPrivateConversationInfo(input);
            var response = new ResponseMessage
            {
                Data = conversation,
                IsSuccess = true
            };

            return Ok(response);
        }
    }
}