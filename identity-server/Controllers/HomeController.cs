using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServerWithAspNetIdentity.Models;
using IdentityServer4.Services;
using IdentityServer4.Quickstart.UI;
using IdentityServer.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace IdentityServerWithAspNetIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ClientConfigs _clientConfigs;

        public HomeController(
            IIdentityServerInteractionService interaction, 
            IOptions<ClientConfigs> clientConfigs)
        {
            _interaction = interaction;
            _clientConfigs = clientConfigs.Value;
        }

        public IActionResult Index()
        {
            return Redirect("/Account/Login");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}
