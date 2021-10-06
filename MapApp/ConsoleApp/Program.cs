using System;
using System.Collections;
using EncounterMe.Functions;
using EncounterMe.Classes;
using EncounterMe;
using System.IO;
using System.Collections.Generic;

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
            Location location1 = new Location(001, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            Location location2 = new Location(002, "VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            Location location3 = new Location(003, "Test", 54.729775633971855, 25.263535399566603);

            DatabaseManager db = new DatabaseManager(Directory.GetCurrentDirectory(), "Test");
            List<Location> locations = new List<Location>();
            locations.Add(location1);
            locations.Add(location2);
            locations.Add(location3);
            db.writeToFile(locations);
            List<Location> locationList = db.readFromFile<Location>();
            //var locationList = db.readFromFile<List<Location>>();
            Console.WriteLine(locationList);
            ////locationList.Sort();

            foreach (Location location in locationList)
            {
                Console.WriteLine(location.Name + " " + location.ID);
                location.GetHashCode();
            }

        }
    }
}
