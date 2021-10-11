using System;
using System.Collections.Generic;
using System.Linq;
using EncounterMe.Classes;

namespace EncounterMe.Functions
{
    public class GameLogic
    {
        public Location getLocationToFind (List<Location> Locations, float Lat, float Long, int distance)
        {
            //LINQ query
            var locationsQuery =
                (from loc in Locations
                where loc.distanceToUser(Lat, Long) < distance
                select loc);

            int i = 0;

            LocationList<Location> locationsSortedByDistance = new LocationList<Location>();

            foreach (Location loc in locationsQuery)
            {
                locationsSortedByDistance[i] = loc;
                i++;
            }

            Random random = new Random();
            int index = random.Next(i);
            try
            {
                Location location =  locationsSortedByDistance[index];
                return location;
            }
            catch
            {
            }
            return null;
        }

        public int getRadiusFromUser ()
        {
            Console.WriteLine("Please write radius for Locations: ");
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            } catch (FormatException e)
            {
                Console.WriteLine("Invalid argument ");
                return getRadiusFromUser();
            }
            
        }

        public void showLocationInformation(Location loc)
        {
            if (loc == null) Console.WriteLine("There is no location to find");
            else Console.WriteLine("Your location to find is: " + loc.Name);
        }
    }
}
