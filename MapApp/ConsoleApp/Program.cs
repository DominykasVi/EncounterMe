using System;
using System.Collections.Generic;
using EncounterMe.Functions;
using EncounterMe.Classes;
using EncounterMe;
using System.IO;
using System.Reflection;
using Weighted_Randomizer;

namespace ConsoleApp
{
    //TODO add to android file, even as generate position

    class Program
    {

        static void Main(string[] args)
        {

            //string input = Console.ReadLine();
            //if (input == "1") { Test_UserCreate(); }
            //else if (input == "2") { Test_UserLogIn(); }

            UserManager aa = new UserManager();
            aa.createXML();
            string input = Console.ReadLine();
            if (input == "1") { Test_UserCreate(); }
            else if (input == "2") { Test_UserLogIn(); }
            else aa.printListOfUsers();

            //else Console.ReadLine();

            Test_randomizer();
        }

        static void Test_randomizer()
        {
            IWeightedRandomizer<string> randomizer = new DynamicWeightedRandomizer<string>();
            Location location1 = new Location("VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            Location location2 = new Location("VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            Location location3 = new Location("M. Mažvydo Nacionalinė Biblioteka", 54.690803584492194, 25.263577022718472);

            location1.upvote();
            location1.upvote();

            location2.upvote();
            location2.upvote();

            location3.upvote();

            randomizer.Add(location1.Name, (int) location1.Upvote);
            randomizer.Add(location2.Name, (int)location2.Upvote);
            randomizer.Add(location3.Name, (int)location3.Upvote);

            string randomLocation = randomizer.NextWithReplacement();
            Console.WriteLine(randomLocation);
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
    }
}
