using MiddlewareExamples.Domain.ServiceAgent;
using MiddlewareExamples.Domain.Services.WeatherService;
using Moq;
using System;
using Xunit;

namespace MiddlewareExamplesTests
{
    public class WeatherServiceUnitTests
    {
        public WeatherServiceUnitTests()
        {

        }

        [Fact]
        public void WeatherService_ConvertCelsiusToFarenheit_ReturnsCorrectFarenheitTemperature()
        {
            var weatherServiceAgent = new Mock<WeatherServiceAgent>();
            var weatherService = new WeatherService(weatherServiceAgent.Object);

            var inFarenheit = weatherService.ConvertCelsiusToFarenheit(0);

            Assert.Equal(32m, inFarenheit);
        }

        [Theory]
        [InlineData(0, 32)]
        [InlineData(10, 50)]
        [InlineData(20, 68)]
        public void WeatherService_ConvertCelsiusToFarenheit_ReturnsCorrectFarenheitTemperatures(decimal tempInC, decimal expectedTempInF)
        {
            var weatherServiceAgent = new Mock<WeatherServiceAgent>();
            var weatherService = new WeatherService(weatherServiceAgent.Object);

            var inFarenheit = weatherService.ConvertCelsiusToFarenheit(tempInC);

            Assert.Equal(expectedTempInF, inFarenheit);
        }

        [Fact]
        public void WeatherServiceAgent_GetTemperatureByDate_ReturnsCorrectTemperature()
        {
            var weatherServiceAgent = new WeatherServiceAgent();

            var temp = weatherServiceAgent.GetTemperatureByDate(new System.DateTime(2020, 09, 01));

            Assert.Equal(20, temp);
        }

        [Fact]
        public void WeatherServiceAgent_GetTemperatureByDate_ThrowsException()
        {
            var weatherServiceAgent = new WeatherServiceAgent();

            var exceptionData = Assert.Throws<Exception>(() => weatherServiceAgent.GetTemperatureByDate(new System.DateTime(2020, 11, 01)));

            Assert.Equal("Temperature is unknown", exceptionData.Message);
        }

        [Fact]
        public void WeatherService_GetTemperatureByDate_ReturnsCorrectFarenheitTemperature()
        {
            var weatherServiceAgent = new Mock<IWeatherServiceAgent>();
            weatherServiceAgent.Setup(agent => agent.GetTemperatureByDate(It.IsAny<DateTime>())).Returns(5);
            var weatherService = new WeatherService(weatherServiceAgent.Object);

            var inFarenheit = weatherService.GetTemperatureByDate(new DateTime(2020,9,1));

            Assert.Equal(5m, inFarenheit);
        }
    }
}
