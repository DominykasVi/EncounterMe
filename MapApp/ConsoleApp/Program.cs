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
            UserManager aa = new UserManager();
            aa.createXML();
            string input = Console.ReadLine();
            if (input == "1") { Test_UserCreate(); }
            else if (input == "2") { Test_UserLogIn(); }
            else aa.printListOfUsers();
            //else Console.ReadLine();

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
