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
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration configuration;
        private string API_KEY;
        private readonly HttpClient httpClient;

        public WeatherService(IConfiguration configuration)
        {
            this.configuration = configuration;

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5");

            API_KEY = configuration.GetValue<string>("OpenWeatherApiKey");
        }

        public async Task<IEnumerable<DailyWeather>> GetWeatherAsync()
        {
            var latitude = "46.7700";
            var longitude = "23.5800";
            var response = await httpClient.GetStringAsync($"http://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=current,minutely,hourly,alerts&appid={API_KEY}");

            return ParseWeatherForecastResponse(response);
        }

        public IEnumerable<DailyWeather> ParseWeatherForecastResponse(string jsonResponse)
        {
            var weatherJsonResponse = JObject.Parse(jsonResponse);
            var dailyForecastArray = weatherJsonResponse.SelectToken("daily");

            var dailyWeather = dailyForecastArray.Select(forecast =>
            {
                var dateTimeUnixFormat = forecast.Value<long>("dt");
                var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(dateTimeUnixFormat);

                var temperatureObject = forecast.SelectToken("temp");
                var temperatureKelvin = temperatureObject.Value<double>("day");
                var temperature = ConvertKelvinToCelsius(temperatureKelvin);

                var weatherTypes = forecast.SelectToken("weather")
                    .Select(type => WeatherTypeConverter.GetWeatherTypeFromWeatherCode(type.Value<int>("id")))
                    .ToList();

                return new DailyWeather
                {
                    Date = dateTimeOffset,
                    Temperature = temperature,
                    WeatherTypes = weatherTypes,
                };
            });

            return dailyWeather;
        }

        public double ConvertKelvinToCelsius(double kelvinTemperature)
        {
            return kelvinTemperature - 273.15;
        }
    }
}
