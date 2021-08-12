using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Models.Weather
{
    public class DailyWeather
    {
        public DateTimeOffset Date { get; set; }
        public float Temperature { get; set; }
        public IList<WeatherType> WeatherTypes { get; set; }
    }
}
