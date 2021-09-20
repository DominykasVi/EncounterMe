using System;
using System.Collections.Generic;

namespace EncounterMe
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_SaveLocation();
        }

        static void Test_SaveLocation()
        {
            Location location1 = new Location(001, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            Location location2 = new Location(002, "VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);

            var locationList = new List<Location>() { location1, location2 };
            DatabaseManager db = new DatabaseManager("Test");

            db.writeToFile(locationList);
            var list = db.readSavedLocations();

            foreach (var value in list)
            {
                Console.WriteLine(value.Name);
            }

            //Console.WriteLine(location1.Latitude);
            //Console.WriteLine(location1.Name);
            //Console.WriteLine(location1.distanceToUser(location2.Latitude, location2.Longtitude));
        }
    }
}
