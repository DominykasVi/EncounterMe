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
