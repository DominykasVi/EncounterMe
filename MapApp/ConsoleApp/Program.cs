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
            id.setID(db.readSavedLocations());
            List<Location> location = new List<Location>();
            for (int i = 0; i < 10; i++)
            {
                location.Add(new Location(id.getID(), "Location no. " + i, 54.67518129701089 + i, 25.273545582365784 + i));
            }
            /*Location location1 = new Location(id.getID(), "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            Location location2 = new Location(id.getID(), "VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            Location location3 = new Location(id.getID(), "Test", 54.729775633971855, 25.263535399566603);*/
            db.writeToFile(location);
            var locationList = db.readSavedLocations();

            locationList.Sort();

            foreach (Location loc in locationList)
            {
                Console.WriteLine(loc.Name + " " + loc.ID + " " + loc.distanceToUser(temp_Location.currLatitude, temp_Location.currLongitude));
            }

        }
    }
}
