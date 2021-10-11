using System;
using System.Collections.Generic;
using EncounterMe.Functions;
using EncounterMe.Classes;
using EncounterMe;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_LocationOutputAndSort();
        }

        static void Test_LocationOutputAndSort()
        {
            //Test Code, non important
            IDGenerator id = IDGenerator.Instance;
            DatabaseManager db = new DatabaseManager("Test");
            GameLogic logic = new GameLogic();
            var locationList = db.readSavedLocations();
            id.setID(locationList);
            List<Location> location = new List<Location>();
            for (int i = 10; i > 0; i--)
            {
                Location loc = new Location(Name: "Location no. " + i, 54.675182+i, 25.273546+i);
                locationList.Add(loc);
            }
            locationList.Sort();
            foreach (Location loc in locationList)
            {
                Console.WriteLine(loc.Name + " " + loc.ID + " " + loc.distanceToUser(temp_Location.currLatitude, temp_Location.currLongitude));
            }
            Location locationByID = id.getLocationByID(127);
            int radius = logic.getRadiusFromUser();
            logic.showLocationInformation(logic.getLocationToFind(locationList, temp_Location.currLatitude, temp_Location.currLongitude, radius));
        }
    }
}
