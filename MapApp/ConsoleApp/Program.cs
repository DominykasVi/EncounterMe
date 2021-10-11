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
            string input = Console.ReadLine();
            if (input == "1") { Test_UserCreate(); }
            else if (input == "2") { Test_UserLogIn(); }
            else Console.ReadLine();

            //Test_LocationOutputAndSort();
        }

        static void Test_UserCreate()
        {
            string name;
            string password;
            string email;
            Console.WriteLine("Name:");
            name = Console.ReadLine();
            Console.WriteLine("Email:");
            email = Console.ReadLine();
            Console.WriteLine("Password:");
            password = Console.ReadLine();

            LogInManager login = new LogInManager();

            User user = login.CreateUser(name, email, password);

            Console.ReadLine();

        }

        static void Test_UserLogIn()
        {
            string name;
            string password;
            Console.WriteLine("Name:");
            name = Console.ReadLine();
            Console.WriteLine("Password:");
            password = Console.ReadLine();

            LogInManager login = new LogInManager();

            User user = login.CheckPassword(name, password);

            Console.ReadLine();

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
