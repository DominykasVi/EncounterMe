using System;
using System.Collections;
using EncounterMe.Classes;
using EncounterMe.Functions;

namespace EncounterMe
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
            //Location location1 = new Location(001, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            //Location location2 = new Location(002, "VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            //Location location3 = new Location(003, "Test", 54.729775633971855, 25.263535399566603);
            DatabaseManager db = new DatabaseManager("Test");
            var locationList = db.readSavedLocations();

            locationList.Sort();

            foreach (Location location in locationList)
            {
                Console.WriteLine(location.Name + " " + location.ID + " " + location.distanceToUser(temp_Location.currLatitude, temp_Location.currLongitude));
            }

        }
    }
}
