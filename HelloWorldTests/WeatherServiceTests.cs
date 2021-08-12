using HelloWorldWebApp.Models.Weather;
using HelloWorldWebApp.Services.Impl;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorldTests
{
    public class WeatherServiceTests
    {
        private IConfiguration configuration;

        public WeatherServiceTests()
        {
            var inMemoryConfiguration = new Dictionary<string, string>
            {
                {"OpenWeatherApiKey", "87c518ece5346b1e8f0e944352222508"}
            };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemoryConfiguration)
                .Build();
        }

        [Fact]
        public void CheckIfParsesForecastCorrectly()
        {
            // Arrange
            var weatherService = new WeatherService(configuration);
            var jsonForecast = "{\"lat\":27.2578,\"lon\":33.8117,\"timezone\":\"Africa\",\"timezone_offset\":7200,\"daily\":[{\"dt\":1628758800,\"sunrise\":1628738062,\"sunset\":1628785523,\"moonrise\":1628750880,\"moonset\":1628795460,\"moon_phase\":0.13,\"temp\":{\"day\":309.17,\"min\":301.8,\"max\":309.17,\"night\":304.32,\"eve\":306.19,\"morn\":301.8},\"feels_like\":{\"day\":307.85,\"night\":303.16,\"eve\":307.22,\"morn\":301.95},\"pressure\":1007,\"humidity\":23,\"dew_point\":284.79,\"wind_speed\":6.65,\"wind_deg\":297,\"wind_gust\":10.53,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"clouds\":0,\"pop\":0,\"uvi\":9.91},{\"dt\":1628845200,\"sunrise\":1628824493,\"sunset\":1628871873,\"moonrise\":1628840940,\"moonset\":1628883960,\"moon_phase\":0.17,\"temp\":{\"day\":307.83,\"min\":301.75,\"max\":308.67,\"night\":306.54,\"eve\":306.94,\"morn\":301.75},\"feels_like\":{\"day\":309.5,\"night\":305.35,\"eve\":308.22,\"morn\":301.99},\"pressure\":1008,\"humidity\":39,\"dew_point\":289.94,\"wind_speed\":6.47,\"wind_deg\":332,\"wind_gust\":9.03,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"clouds\":0,\"pop\":0,\"uvi\":10.2}]}";

            // Act
            var forecast = weatherService.ParseWeatherForecastResponse(jsonForecast).ToList();

            // Assume
            Assert.Equal(2, forecast.Count);

            var firstDay = forecast[0];
            Assert.Equal(36.02, firstDay.Temperature, 2);
            Assert.True(firstDay.Date.DayOfYear == new DateTime(2021, 8, 12).DayOfYear);
            Assert.Single(firstDay.WeatherTypes);
            Assert.Equal(WeatherType.Clear, firstDay.WeatherTypes.First());

            var secondDay = forecast[1];
            Assert.Equal(34.68, secondDay.Temperature, 2);
            Assert.True(secondDay.Date.DayOfYear == new DateTime(2021, 8, 13).DayOfYear);
            Assert.Single(secondDay.WeatherTypes);
            Assert.Equal(WeatherType.Clear, secondDay.WeatherTypes.First());
        }

        [Fact]
        public void CheckIfConvertKelvinToCelsiusWorks()
        {            
            // Arrange
            var weatherService = new WeatherService(configuration);
            double kelvinTemp = 303.15;
            double expectedCelsiusTemp = 30;

            // Act
            double convertedTemp = weatherService.ConvertKelvinToCelsius(kelvinTemp);

            // Assert
            Assert.Equal(expectedCelsiusTemp, convertedTemp);
        }
    }
}
