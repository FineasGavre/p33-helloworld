// <copyright file="WeatherController.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebApp.Controllers
{
    /// <summary>
    /// Controller for the Weather API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService weatherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherController"/> class.
        /// </summary>
        /// <param name="weatherService">DI injected IWeatherService.</param>
        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        /// <summary>
        /// GET /api/weather.
        /// </summary>
        /// <returns>Weather for configured location response.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await weatherService.GetWeatherAsync());
        }
    }
}
