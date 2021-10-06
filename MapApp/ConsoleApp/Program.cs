using System;
using System.Collections.Generic;
using EncounterMe.Functions;
using EncounterMe.Classes;
using EncounterMe;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp
{
    //TODO add to android file, even as generate position
    public static class MyExtendedMethods
    {
        public static (float, float) getLocationCoordinates(this Location num)
        {
            return (num.Latitude, num.Longtitude);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            //Test_LocationOutputAndSort();

        }
        static void dominykas_tests() {
            Location location1 = new Location(001, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            var loc = location1.getLocationCoordinates();
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
