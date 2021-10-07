using System;
using System.Collections.Generic;
using EncounterMe.Functions;
using EncounterMe.Classes;
using EncounterMe;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp
{
    //TODO add to android file, even as generate position

    class Program
    {

        static void Main(string[] args)
        {
            //Test_LocationOutputAndSort();
            dominykas_tests();

        }
        static void dominykas_tests() {
            Location location1 = new Location(001, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            Location location2 = new Location(002, "VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            Location location3 = new Location(003, "VU MIF", 54.67518129701089, 25.273545582365784);
            Location location4 = new Location(004, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            //var loc = location1.getLocationCoordinates();
            //Type myType = location1.GetType();
            //IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            Console.WriteLine(Directory.GetCurrentDirectory());
            DatabaseManager db = new DatabaseManager(Directory.GetCurrentDirectory(), "Locations");
            List<Location> testList = new List<Location>() { location1, location2, location3 };
            db.writeToFile(testList);

            //foreach (PropertyInfo prop in props)
            //{
            //    Console.WriteLine(prop.Name.Contains("ID"));
            //    object propValue = prop.GetValue(location1, null);
            //    Console.WriteLine(propValue);
            //    // Do something with propValue
            //}
            //Console.WriteLine(location1.CompareTo(location3) > 0);
            //Console.WriteLine(location1.CompareTo(location4) > 0);

        }
        static void Test_LocationOutputAndSort()
        {
            //Test Code, non important
            Location location1 = new Location(001, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            Location location2 = new Location(002, "VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            Location location3 = new Location(003, "Test", 54.729775633971855, 25.263535399566603);
            IDGenerator id = IDGenerator.Instance;
            DatabaseManager db = new DatabaseManager(Directory.GetCurrentDirectory(),"Locations");
            Location test = new Location(123, "Teeeest", 1.0, 3.0);
            List<Location> testList = new List<Location>() { location1, location2, test };
            db.writeToFile(testList);
            var locationList = db.readFromFile<Location>();
            id.setID(locationList);
            List<Location> location = new List<Location>();
            for (int i = 0; i < 10; i++)
            {
                Location loc = new Location("Location no. " + i, 54.675182+i, 25.273546+i);
            
                locationList.Add(loc);
                loc.ID = id.getID(loc);
            }
            locationList.Sort();
            foreach (Location loc in locationList)
            {
                Console.WriteLine(loc.Name + " " + loc.ID + " " + loc.distanceToUser(temp_Location.currLatitude, temp_Location.currLongitude));
            }
            Location locationByID = id.getLocationByID(123);

            for (int i = 0; i < 50; i++)
            {
                locationByID.upvote();
            }

            for (int i = 0; i < 20; i++)
            {
                locationByID.downvote();
            }
            Console.WriteLine(locationByID.Name + " " + locationByID.ID + " " + locationByID.distanceToUser(temp_Location.currLatitude, temp_Location.currLongitude) + " " + locationByID.getRating());

        }

    }
}
