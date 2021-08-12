using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Models.Weather
{
    public enum WeatherType
    {
        Thunderstorm,
        Drizzle,
        Rain,
        Snow,
        Atmosphere,
        Clear,
        Clouds
    }

    public class WeatherTypeConverter
    {
        public static WeatherType GetWeatherTypeFromWeatherCode(int code)
        {
            if (code >= 200 && code <= 299)
            {
                return WeatherType.Thunderstorm;
            }

            if (code >= 300 && code <= 399)
            {
                return WeatherType.Drizzle;
            }

            if (code >= 500 && code <= 599)
            {
                return WeatherType.Rain;
            }

            if (code >= 600 && code <= 699)
            {
                return WeatherType.Snow;
            }

            if (code >= 700 && code <= 799)
            {
                return WeatherType.Atmosphere;
            }

            if (code >= 801 && code <= 899)
            {
                return WeatherType.Clouds;
            }

            return WeatherType.Clear;
        }
    }
}
