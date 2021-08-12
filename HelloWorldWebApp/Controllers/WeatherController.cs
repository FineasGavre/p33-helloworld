using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await weatherService.GetWeatherAsync());
        }
    }
}
