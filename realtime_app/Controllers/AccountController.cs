using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using realtime_app.Contracts;
using realtime_app.Services;

namespace realtime_app.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<Models.AppUser> _userManager;
        private readonly SignInManager<Models.AppUser> _signInManager;

        private readonly IIdentityServerInteractionService _interaction;

        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IClientStore _clientStore;

        private readonly IEventService _events;
        public AccountController(IUserService userService, UserManager<Models.AppUser> userManager, IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider schemeProvider, IClientStore clientStore, IEventService events)
        {
            _userManager = userManager;
            _interaction = interaction;
            _schemeProvider = schemeProvider;
            _clientStore = clientStore;
            _events = events;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post(RegiserUserContract contract)
        {
            return Ok(await _userService.RegisterUserAsync(contract));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            var user = await _userManager.FindByNameAsync(model.Username);
            if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

            }
            return Ok();
        } 
    }
}