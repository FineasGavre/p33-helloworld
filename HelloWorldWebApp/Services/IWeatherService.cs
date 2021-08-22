// <copyright file="IWeatherService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Models.Weather;

namespace HelloWorldWebApp.Services
{
    /// <summary>
    /// Interface for Weather Service.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Get the 7-day weather forecast for configured location.
        /// </summary>
        /// <returns>Enumerable of DailyWeather objects.</returns>
        Task<IEnumerable<DailyWeather>> GetWeatherAsync();
    }
}
