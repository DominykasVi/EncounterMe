using EncounterMe;
using EncounterMe.Classes;
using EncounterMe.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiddlewareExamples.Contracts.GetWeatherForecast;
using MiddlewareExamples.Domain.Services.WeatherService;
using System;
using System.Collections.Generic;

namespace MiddlewareExamples.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        DatabaseManager db;
        

        private readonly ILogger<LocationController> _logger;
        private readonly IGame gameLogic;

        public LocationController(
            ILogger<LocationController> logger,
            IGame gameLogic)
        {
            _logger = logger;
            this.gameLogic = gameLogic;
        }

        //should change string password to byte[] password later on
        [Route("api/Location/Test")]
        [HttpPost]
        public string TestPostSimple([FromBody] string username)
        {
            Console.WriteLine(username);
            return username;
        }

        public class TestUser
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        [Route("api/Location/GetLocationList")]
        [HttpPost]
        public string GetLocationList(TestUser temp)
        {
            return temp.username;
            //this code returns errors


            //DatabaseManager databaseManager = new DatabaseManager();
            //LogInManager logInManager = new LogInManager(databaseManager);

            //User user = logInManager.CheckPassword(temp.username, temp.password);
            //if (user.accessLevel == AccessLevel.Admin)
            //{
            //    //DatabaseManager databaseManager = new DatabaseManager();

            //    //get location list (after we rework DatabaseManager)\
            //    //serialize location list into string[] and return it
            //    return "yes";
            //}
            //else
            //    return "no";
        }

        [HttpPost]
        [Route("api/Location/FindLocation")]
        public Location GetLocation(LocationToFind userLocation)
        {
            //var distance = 50;
            //Console.WriteLine(userLocation.Distance);
            this.db = new DatabaseManager(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test", new DatabaseLogger());
            var locationList = db.readFromFile<EncounterMe.Location>();
            //List<Location> locations = null;
            //if (dist <= searchRadius.Kilometers && ((location.attributes & filterList) > 0))
            //we should probably change getLocationsToFind in a way that List<Location> is not needed as a parameter
            var location = gameLogic.getLocationToFind(locationList, userLocation.Latitude, userLocation.Longtitude, userLocation.Distance);
            //location should be serialized and returned
            return location;
        }
    }
}
