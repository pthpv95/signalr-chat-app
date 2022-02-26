using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using chat_service.Common;
using chat_service.Contracts;
using chat_service.Services;

namespace chat_service.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/users")]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly IClaimsService _claimService;

    public UsersController(IUserService userService, IClaimsService claimService)
    {
      _userService = userService;
      _claimService = claimService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(RegiserUserContract contract)
    {
      var result = await _userService.RegisterUserAsync(contract);
      return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var userContext = _claimService.GetUserClaims();
      var response = new ResponseMessage
      {
        Data = await _userService.GetUserDetails(userContext.Id),
        IsSuccess = true
      };
      return Ok(response);
    }
  }
}
