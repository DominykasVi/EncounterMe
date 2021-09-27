using System;
using System.Collections.Generic;
//using System.Collections.Generic;
using EncounterMe.Functions;

namespace EncounterMe
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }
        static void Test ()
        {
            GameLogic logic = new GameLogic();
            Location location1 = new Location(001, "VU MIF Naugardukas", (float)54.67518129701089, (float)25.273545582365784);
            Location location2 = new Location(002, "VU MIF Baltupiai", (float)54.729775633971855, (float)25.263535399566603);
            Location location3 = new Location(003, "Test", (float)54.729775633971855, (float)25.263535399566603);
            var locationList = new List<Location>() { location1, location2, location3 };
            DatabaseManager db = new DatabaseManager("Test");
            db.writeToFile(locationList);
            int radius = logic.getRadiusFromUser();
            logic.showLocationInformation(logic.getLocationToFind(db.readSavedLocations(), (float)54.698135, (float)25.278630, radius));
        }
    }
}
