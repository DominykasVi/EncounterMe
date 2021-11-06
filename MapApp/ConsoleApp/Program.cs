using System;
using System.Collections.Generic;
using EncounterMe.Functions;
using EncounterMe.Classes;
using EncounterMe;
using System.IO;
using System.Reflection;

namespace ConsoleApp
{
    //TODO add to android file, even as generate position

    class Program
    {

        static void Main(string[] args)
        {
            DatabaseManager db = new DatabaseManager("", "users");
            LogInManager lm = new LogInManager(db);
            /*
            lm.CreateUser("Kristijonas", "kr@tr.com", "ZXfdsf123");
            */
            Test_UserLogIn();
            var uss = db.readFromFile<User>();
            foreach (User us in db.readFromFile<User>())
            {
                Console.WriteLine(us.name);
            }

            //Console.WriteLine(lm.CreateUser("Kristis", "kristijonas@gmail.com", "Qw1").name);
            /*List<Location> locations = new List<Location>();
            IDGenerator id = IDGenerator.Instance;
            GameLogic gl = new GameLogic();
            id.setID(locations);
            Location location1 = new Location("1", 2.0, 3.0);
            Location location2 = new Location("2", 2.0, 3.0);
            Location location3 = new Location("3", 2.0, 3.0);
            Location location4 = new Location("4", 2.0, 3.0);
            locations.Add(location1);
            locations.Add(location2);
            locations.Add(location3);
            locations.Add(location4);
            for (int i = 0; i<10; i++)
            {
                location1.upvote();
                location2.upvote();
                location3.upvote();
            }
            location4.upvote();
            for (int i = 0; i < 5; i++)
            {
                location3.downvote();
                location4.downvote();
            }
            foreach (var location in locations)
            {
                Console.WriteLine("location " + location.Name + " has rating of " + location.getRating());
            }
            for (int i=0; i<20; i++)
            {
                gl.showLocationInformation(gl.getLocationToFind(locations, (float)2.0, (float)3.0, 1));
                
            }*/
        }
        
        static void Test_UserCreate()
        {
            DatabaseManager db = new DatabaseManager("", "users");
            string name;
            string password;
            string email;
            Console.WriteLine("Name:");
            name = Console.ReadLine();
            Console.WriteLine("Email:");
            email = Console.ReadLine();
            Console.WriteLine("Password:");
            password = Console.ReadLine();

            LogInManager login = new LogInManager(db);
            
            User user = login.CreateUser(name, email, password);

            Console.ReadLine();

        }
        
        static void Test_UserLogIn()
        {
            DatabaseManager db = new DatabaseManager("", "users");
            string name;
            string password;
            Console.WriteLine("Name:");
            name = Console.ReadLine();
            Console.WriteLine("Password:");
            password = Console.ReadLine();

            LogInManager login = new LogInManager(db);

            User user = login.CheckPassword(name, password);

            Console.ReadLine();

        }
    }
}
