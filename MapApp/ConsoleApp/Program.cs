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
            test_Events();
        }

        //static void TestIlogger()
        //{
        //    errorLogger.logErrorMessage("File: " + path + "not found");
        //    errorLogger.logErrorMessage("Could not sort location list");

        //}
        static void test_Events ()
        {
            GameLogic gl = new GameLogic();
            EventManager ev = new EventManager(gl);
            IDGenerator id = IDGenerator.Instance;
            id.setID(new List<Location> { });
            Location loc1 = new Location("asd", 1.0f, 1.0f);
            gl.isLocationFound(loc1, 1.0f, 1.0f);
            gl.isLocationFound(loc1, 2.0f, 3.0f);
        }

    }
}
