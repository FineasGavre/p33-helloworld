// <copyright file="WeatherTypeConverter.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Models.Weather
{
    /// <summary>
    /// WeatherType enum.
    /// </summary>
    public enum WeatherType
    {
#pragma warning disable SA1602, CS1591 // Enumeration items should be documented
        Thunderstorm,
        Drizzle,
        Rain,
        Snow,
        Atmosphere,
        Clear,
        Clouds,
#pragma warning restore SA1602, CS1591 // Enumeration items should be documented
    }

    /// <summary>
    /// WeatherType converter class.
    /// </summary>
    public class WeatherTypeConverter
    {
        /// <summary>
        /// Converts an OpenWeather code to a WeatherType enum.
        /// </summary>
        /// <param name="code">OpenWeather code.</param>
        /// <returns>Corresponding WeatherType.</returns>
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
