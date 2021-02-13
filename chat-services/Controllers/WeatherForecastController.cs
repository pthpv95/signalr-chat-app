using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatservices.Constants;
using chatservices.Contracts;
using chatservices.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using realtime_app.SignalR.Hubs;

namespace realtime_app.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheService _cacheService;

        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly IPubSub _pubSub;
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IHttpContextAccessor httpContextAccessor, 
            ICacheService cacheService, 
            IHubContext<NotificationHub> notificationHub,
            IPubSub pubSub)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _cacheService = cacheService;
            _notificationHub = notificationHub;
            _pubSub = pubSub;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _cacheService.Set("foo", new List<string>() { "hello world" });
            var x = await _cacheService.Get<List<string>>("Chat_a5b5671c-7ce5-49f3-b400-27d490a5dbf7");
            // var value1 = await _cacheService.Get<List<string>>("OT_666f1370-9a66-45ab-a95e-cdc9e0a0764a");
            // var value2 = await _cacheService.Get<List<string>>("OT_be8d770c-af48-4392-b04d-a21ef9fd647f");

            var user = _httpContextAccessor.HttpContext.User;
            var rng = new Random();

            // await _notificationHub.Clients.All.SendAsync("HasUnreadMessagesAsync", rng.Next(10, 60));
            await _pubSub.Publish(Channels.NotififcationMessageChannel, new NewNotificationMessageContract
            {
              ActionType = NotificationActionType.UnreadMessage,
              ConnectionIds = new List<string>{},
              TotalUnreadMessages = rng.Next(10, 60)
            });

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = "123"
            })
            .ToArray();
        }
    }
}
