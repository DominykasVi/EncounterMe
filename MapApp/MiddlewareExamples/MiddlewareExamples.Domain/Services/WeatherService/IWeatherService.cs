using MiddlewareExamples.Contracts.GetWeatherForecast;
using System;
using System.Collections.Generic;

namespace MiddlewareExamples.Domain.Services.WeatherService
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetTodaysForecast();

        decimal ConvertCelsiusToFarenheit(decimal temperatureInC);

        decimal? GetTemperatureByDate(DateTime date);
    }
}
