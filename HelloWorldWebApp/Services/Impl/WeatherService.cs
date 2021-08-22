// <copyright file="WeatherService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HelloWorldWebApp.Models.Weather;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace HelloWorldWebApp.Services.Impl
{
    /// <summary>
    /// Implementation of IWeatherService.
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration configuration;
        private readonly string apiKey;
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherService"/> class.
        /// </summary>
        /// <param name="configuration">Application Configuration.</param>
        public WeatherService(IConfiguration configuration)
        {
            this.configuration = configuration;

            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.openweathermap.org/data/2.5"),
            };

            apiKey = configuration.GetValue<string>("OpenWeatherApiKey");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<DailyWeather>> GetWeatherAsync()
        {
            var latitude = configuration["Latitude"];
            var longitude = configuration["Longitude"];
            var response = await httpClient.GetStringAsync($"http://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=current,minutely,hourly,alerts&appid={apiKey}");

            return ParseWeatherForecastResponse(response);
        }

        /// <summary>
        /// Parses the response from the API.
        /// </summary>
        /// <param name="jsonResponse">String that contains the JSON response of the API.</param>
        /// <returns>An enumerable of DailyWeather for the given string.</returns>
        public IEnumerable<DailyWeather> ParseWeatherForecastResponse(string jsonResponse)
        {
            var weatherJsonResponse = JObject.Parse(jsonResponse);
            var dailyForecastArray = weatherJsonResponse.SelectToken("daily");
            return dailyForecastArray.Select(forecast => CreateDailyWeatherFromJToken(forecast));
        }

        /// <summary>
        /// Converts temperature from Kelvin to Celsius.
        /// </summary>
        /// <param name="kelvinTemperature">Temperature in Kelvin.</param>
        /// <returns>Temperature in Celsius.</returns>
        public double ConvertKelvinToCelsius(double kelvinTemperature)
        {
            return kelvinTemperature - 273.15;
        }

        private DailyWeather CreateDailyWeatherFromJToken(JToken token)
        {
            var dateTimeUnixFormat = token.Value<long>("dt");
            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(dateTimeUnixFormat);

            var temperatureObject = token.SelectToken("temp");
            var temperatureKelvin = temperatureObject.Value<double>("day");
            var temperature = ConvertKelvinToCelsius(temperatureKelvin);

            var weatherTypes = token.SelectToken("weather")
                .Select(type => WeatherTypeConverter.GetWeatherTypeFromWeatherCode(type.Value<int>("id")))
                .ToList();

            return new DailyWeather
            {
                Date = dateTimeOffset,
                Temperature = temperature,
                WeatherTypes = weatherTypes,
            };
        }
    }
}
