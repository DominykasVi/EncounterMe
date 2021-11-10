using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EncounterMe;
using EncounterMe.Classes;
using EncounterMe.Functions;

namespace WebServer.Controllers
{
    public class LocationController : ApiController
    {
        DatabaseManager db;
        public string Get()
        {
            return "hello";
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
        [Route("api/Location/FindLocation")]
        [HttpPost]
        public Location GetLocation (LocationToFind userLocation)
        {
            //var distance = 50;
            GameLogic gameLogic = new GameLogic();
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
