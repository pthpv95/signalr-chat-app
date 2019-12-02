using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using realtime_app.Common;
using realtime_app.Contracts;
using realtime_app.Models;
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
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post(RegiserUserContract contract)
        {
            var appUser = new AppUser()
            {
                UserName = contract.UserName,
                Name = contract.FirstName
            };

            var identityResult = await _userManager.CreateAsync(appUser, contract.Password);
            if(identityResult.Succeeded)
            {
                await _userManager.AddClaimAsync(appUser, new System.Security.Claims.Claim("userName", appUser.UserName));
                await _userManager.AddClaimAsync(appUser, new System.Security.Claims.Claim("role", "player"));

                var user = await _userService.RegisterUserAsync(contract);
                var response = new ResponseMessage 
                {
                    IsSuccess = true,
                    Data = user
                };

                return Ok(response);
            }
            else
            {
                return BadRequest(identityResult.Errors.First().Description);
            }    
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginInputModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var result = new ResponseMessage
                    {
                        Data = "Login success"
                    };

                    return Ok(result);
                }
            }
            
            return BadRequest("Invalid username or password");
        } 
    }
}