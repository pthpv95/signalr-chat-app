using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatservices.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace realtime_app.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheService _cacheService;

        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor httpContextAccessor, ICacheService cacheService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _cacheService.Set("foo", new List<string>() { "hello world" });
            var x = await _cacheService.Get<List<string>>("foo");
            // var value1 = await _cacheService.Get<List<string>>("OT_666f1370-9a66-45ab-a95e-cdc9e0a0764a");
            // var value2 = await _cacheService.Get<List<string>>("OT_be8d770c-af48-4392-b04d-a21ef9fd647f");

            var user = _httpContextAccessor.HttpContext.User;
            var rng = new Random();
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
