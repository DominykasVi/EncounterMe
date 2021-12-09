using EncounterMe;
using EncounterMe.Classes;
using EncounterMe.Functions;
using EncounterMe.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        DatabaseManager db;
        IGame gameLogic;

        public LocationController(IGame gameLogic)
        {
            this.gameLogic = gameLogic;
        }
        //should change string password to byte[] password later on
        [Route("Test")]
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
        [Route("GetLocationList")]
        [HttpGet]
        public List<Location> GetLocationList()
        {
            this.db = new DatabaseManager(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test", new DatabaseLogger());
            var locationList = db.readFromFile<EncounterMe.Location>();
            return locationList;
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
        [Route("FindLocation")]
        [HttpPost]
        //public Location GetLocation(LocationToFind userLocation)
        public Location GetLocation(LocationToFind userLocation)
        {
            //var distance = 50;
            //GameLogic gameLogic = new GameLogic();
            this.db = new DatabaseManager(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test", new DatabaseLogger());
            var locationList = db.readFromFile<EncounterMe.Location>();
            //List<Location> locations = null;
            //if (dist <= searchRadius.Kilometers && ((location.attributes & filterList) > 0))
            //we should probably change getLocationsToFind in a way that List<Location> is not needed as a parameter
            var location = gameLogic.getLocationToFind(locationList, userLocation.Latitude, userLocation.Longtitude, userLocation.Distance, null);
            //location should be serialized and returned
            //return location;
            return location;
        }
    }
}
