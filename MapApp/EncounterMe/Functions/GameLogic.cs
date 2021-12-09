using System;
using System.Collections.Generic;
using System.Linq;
using EncounterMe.Classes;
using EncounterMe.Interfaces;
using EncounterMe.Functions;

namespace EncounterMe.Functions
{
    public class GameLogic : IGame
    {

        public delegate void LocationFoundDel(Location loc);
        public event LocationFoundDel? LocationFound;
        public event Action? LocationNotFound;
        private const double circumference = 6372.795477598;

        public Location getLocationToFind (List<Location> Locations, float Lat, float Long, double distance)

        {
            //LINQ query
            var locationsQuery =
                (from loc in Locations
                where loc.distanceToUser(Lat, Long) < distance
                select loc).ToList();

            Dictionary<Location, float> locationsSortedByDistance = new Dictionary<Location, float>();

            foreach (Location loc in locationsQuery)
            {
                locationsSortedByDistance.Add(loc, loc.getRating());
            }

            return locationsSortedByDistance.weightedRandom(e => e.Value).Key;

        }

        public void isLocationFound(Location loc, float lat, float lon)
        {
            Console.WriteLine(loc.Name);
            if (loc.distanceToUser(lat, lon) < 0.005)
            {
                OnLocationFound(loc);
            }
            else
            {
                OnLocationNotFound();
            }
        }

        public float distanceBetweenPoints(float firstLat, float firstLon, float secondLat, float secondLon)
        {
            return (float)(circumference *
                Math.Acos(Math.Sin(firstLat * Math.PI / 180.00) *
                Math.Sin(secondLat * Math.PI / 180.00) +
                Math.Cos(firstLat * Math.PI / 180.00) *
                Math.Cos(secondLat * Math.PI / 180.00) *
                Math.Cos((firstLon - secondLon) * Math.PI / 180)));
        }

        protected virtual void OnLocationFound(Location loc)
        {
            LocationFound?.Invoke(loc);
        }
        protected virtual void OnLocationNotFound()
        {
            LocationNotFound?.Invoke();
        }

        public int getRadiusFromUser ()
        {
            Console.WriteLine("Please write radius for Locations: ");
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            } catch (FormatException)
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
