using System;

namespace MiddlewareExamples.Domain.ServiceAgent
{
    public interface IWeatherServiceAgent
    {
        decimal? GetTemperatureByDate(DateTime date);
    }
}
