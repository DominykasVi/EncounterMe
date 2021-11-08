using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EncounterMe;
using EncounterMe.Functions;

namespace WebServer.Controllers
{
    public class LocationController : ApiController
    {
        //should change string password to byte[] password later on
        public string[] GetLocationList(string username, string password)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            LogInManager logInManager = new LogInManager(databaseManager);

            User user = logInManager.CheckPassword(username, password);
            if (user.accessLevel == AccessLevel.Admin)
            {
                //DatabaseManager databaseManager = new DatabaseManager();

                //get location list (after we rework DatabaseManager)\
                //serialize location list into string[] and return it
                return null;
            }
            else 
                return null;
        }

        public string GetLocation (float longitude, float latitude, int distance)
        {
            GameLogic gameLogic = new GameLogic();
            List<Location> locations = null;

            //we should probably change getLocationsToFind in a way that List<Location> is not needed as a parameter
            var location = gameLogic.getLocationToFind(locations, latitude, longitude, distance);
            //location should be serialized and returned
            return null;
        }
    }
}
