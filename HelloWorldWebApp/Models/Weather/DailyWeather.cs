// <copyright file="DailyWeather.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Models.Weather
{
    /// <summary>
    /// DailyWeather model.
    /// </summary>
    public class DailyWeather
    {
        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets the Temperature.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Gets or sets the list of WeatherTypes.
        /// </summary>
        public IList<WeatherType> WeatherTypes { get; set; }
    }
}
