using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using realtime_app.Contracts;
using realtime_app.Services;

namespace realtime_app.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegiserUserContract contract)
        {
            var result = await _userService.RegisterUserAsync(contract);
            return Ok(result);
        } 
    }
}
