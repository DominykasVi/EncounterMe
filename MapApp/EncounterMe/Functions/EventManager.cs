using System;
using System.Collections.Generic;
using EncounterMe.Classes;
using System.Text;

namespace EncounterMe.Functions
{
    public class EventManager
    {
        GameLogic gl;

        public EventManager(GameLogic glogic)
        {
            Console.WriteLine("subs");
            gl = glogic;
            gl.LocationFound += LocationFoundHandler;
            gl.LocationNotFound += LocationNotFoundHandler;
        }

        private static void LocationFoundHandler(Location loc)
        {
            Console.WriteLine("Location " + loc.Name + " found!");
        }
        private static void LocationNotFoundHandler()
        {
            Console.WriteLine("Location not found! Try again.");
        }
    }
}
