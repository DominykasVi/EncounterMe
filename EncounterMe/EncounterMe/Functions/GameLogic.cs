using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Functions
{
    class GameLogic
    {
        public Location getLocationToFind (IEnumerable<Location> Locations, float Lat, float Long, int distance)
        {
            List <Location> locationsSortedByDistance = new List<Location>();
            foreach (Location loc in Locations)
            {
                if (loc.distanceToUser(Lat, Long) <= distance)
                {
                    locationsSortedByDistance.Add(loc);
                }
            }
            Random random = new Random();
            int index = random.Next(locationsSortedByDistance.Count);
            try
            {
                Location location =  locationsSortedByDistance[index];
                return location;
            }
            catch (ArgumentOutOfRangeException e)
            {
                
            }
            return null;
        }

        public int getRadiusFromUser ()
        {
            Console.WriteLine("Please write radius for Locations: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public void showLocationInformation(Location loc)
        {
            if (loc == null) Console.WriteLine("There is no location to find");
            else Console.WriteLine("Your location to find is: " + loc.Name);
        }
    }
}
