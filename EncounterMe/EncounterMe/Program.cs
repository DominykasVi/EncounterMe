using System;

namespace EncounterMe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Location location1 = new Location(001, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            Location location2 = new Location(002, "VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            Console.WriteLine(location1.Latitude);
            Console.WriteLine(location1.Name);
            Console.WriteLine(location1.distanceToUser(location2.Latitude, location2.Longtitude));
        }
    }
}
