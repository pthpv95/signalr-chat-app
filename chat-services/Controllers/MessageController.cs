using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using realtime_app.Common;
using realtime_app.Contracts;
using realtime_app.Services;

namespace realtime_app.Controllers
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

    [HttpGet]
    [Route("contact/{contactUserId}")]
    public async Task<IActionResult> GetConversationInfo([FromRoute] Guid contactUserId)
    {
      var conversation = await _messageService.GetPrivateConversationInfo(_claimsService.GetUserClaims().Id, contactUserId);
      var response = new ResponseMessage
      {
        Data = conversation,
        IsSuccess = true
      };

      return Ok(response);
    }
  }
}