using System;

namespace MiddlewareExamples.Domain.ServiceAgent
{
    public class WeatherServiceAgent : IWeatherServiceAgent
    {
        public decimal? GetTemperatureByDate(DateTime date)
        {
            // fake implementation
            switch (date.Month)
            {
                case 1:
                    return -5;
                case 5:
                    return 15;
                case 9:
                    return 20;
                case 10:
                    return null;
                default:
                    // if this will be hit then exception handling middleware will handle it, but also log aspect will log it.
                    throw new Exception("Temperature is unknown");
            }
        }
    }
}
