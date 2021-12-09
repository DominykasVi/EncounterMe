using MiddlewareExamples.Contracts.GetWeatherForecast;
using MiddlewareExamples.Domain.Aspects;
using MiddlewareExamples.Domain.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddlewareExamples.Domain.Services.WeatherService
{
    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IWeatherServiceAgent _weatherServiceAgent;

        public WeatherService(IWeatherServiceAgent weatherServiceAgent)
        {
            _weatherServiceAgent = weatherServiceAgent;
        }

        public IEnumerable<WeatherForecast> GetTodaysForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        public decimal ConvertCelsiusToFarenheit(decimal temperatureInC)
        {
            var temperatureInF = temperatureInC * 1.8m + 32m;

            return temperatureInF;
        }

        public decimal? GetTemperatureByDate(DateTime date)
        {
            return _weatherServiceAgent.GetTemperatureByDate(date);
        }
    }
}
