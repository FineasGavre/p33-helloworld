using HelloWorldWebApp.Models.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<DailyWeather>> GetWeatherAsync();
    }
}
