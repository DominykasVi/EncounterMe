using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiddlewareExamples.Contracts.GetWeatherForecast;
using MiddlewareExamples.Domain.Services.WeatherService;
using System;
using System.Collections.Generic;

namespace MiddlewareExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {  
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), 200)]
        public IActionResult Get()
        {
            var weatherForecast = _weatherService.GetTodaysForecast();

            if(weatherForecast == null)
            {
                return NotFound();
            }

            return Ok(weatherForecast);
        }


        [HttpGet]
        [Route("{date}")]
        public IActionResult GetByDate([FromRoute] DateTime date)
        {
            var temperature = _weatherService.GetTemperatureByDate(date);

            if (temperature == null)
            {
                return NotFound();
            }

            // to test that exception handling middleware works enter date like 2021-05-05
            var veryBadCode = date.Year / (date.Month - date.Day);

            return Ok(temperature);
        }
    }
}
