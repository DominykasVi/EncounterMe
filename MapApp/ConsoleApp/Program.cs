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
            //Test_LocationOutputAndSort();
            Enum_Testing();
        }

        static void Enum_Testing()
        {
            //Using flags
            EncounterMe.Location loc = new EncounterMe.Location(001, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784, 
                Location.LocationAttributes.DifficultTerrain | Location.LocationAttributes.DifficultToFind); //describing enum
            Console.WriteLine(loc.Attributes); //getting flags in string

            //Using enum in filtering - might be useful
            List<Location> locList = new List<Location>();
            locList.Add(new EncounterMe.Location(001, "one", 1, 1,
                Location.LocationAttributes.DifficultTerrain));
            locList.Add(new EncounterMe.Location(002, "two", 1, 1));
            locList.Add(new EncounterMe.Location(003, "three", 1, 1,
                Location.LocationAttributes.DifficultToFind | Location.LocationAttributes.DifficultTerrain | Location.LocationAttributes.FarFromCityCenter));
            locList.Add(new EncounterMe.Location(004, "four", 1, 1,
                Location.LocationAttributes.FarFromCityCenter));
            locList.Add(new EncounterMe.Location(005, "five", 1, 1,
                Location.LocationAttributes.CloseToCityCenter | Location.LocationAttributes.DifficultTerrain));

            //Original list
            Console.WriteLine("\nUnfiltered List:");
            locList.ForEach(x => Console.WriteLine("{0,-10} {1,0}", x.Name, x.Attributes));

            //List that contains DifficultTerrain
            var diffTerrList1 = locList
            .FindAll(x => (Location.LocationAttributes.DifficultTerrain | x.attributes) == x.attributes);

            Console.WriteLine("\nList of coordinates that contains DifficultTerrain:");
            diffTerrList1.ForEach(x => Console.WriteLine("{0,-10} {1,0}", x.Name, x.Attributes));

            //List that contains ONLY DifficultTerrain
            var diffTerrList2 = locList
            .FindAll(x => x.attributes == Location.LocationAttributes.DifficultTerrain);

            Console.WriteLine("\nList of coordinates that contains ONLY DifficultTerrain:");
            diffTerrList2.ForEach(x => Console.WriteLine("{0,-10} {1,0}", x.Name, x.Attributes));
        }

        static void Test_LocationOutputAndSort()
        {
            //Test Code, non important

            IDGenerator id = IDGenerator.Instance;
            DatabaseManager db = new DatabaseManager("Test");
            //Location test = new Location(123, "Teeeest", 1.0, 3.0);
            //List<Location> testList = new List<Location>() { test };
            //db.writeToFile(testList);
            var locationList = db.readSavedLocations();
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
