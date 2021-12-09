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

        public Location getLocationToFind (List<Location> Locations, float Lat, float Long, double distance, List<Classes.Attribute> attributes)
        {
            //LINQ query
            var locationsQuery =
                (from loc in Locations
                where loc.distanceToUser(Lat, Long) < distance
                select loc).ToList();

            List<Location> byAttr = new List<Location> ();

            if (attributes != null)
            {
                foreach (Classes.Attribute at in attributes)
                {
                    byAttr.AddRange(locationsQuery.FindAll(s => s.Attributes.Contains(at)));
                }
            }
            
            

            Dictionary<Location, float> locationsSortedByDistance = new Dictionary<Location, float>();

            foreach (Location loc in byAttr)
            {
                if (!locationsSortedByDistance.ContainsKey(loc))
                    locationsSortedByDistance.Add(loc, loc.getRating());
            }

            return locationsSortedByDistance.weightedRandom(e => e.Value).Key;

        }

        public void isLocationFound(Location loc, float lat, float lon)
        {
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

        public double getBearingFromUser(double loclat, double loclon, double lat, double lon)
        {
            var lon2 = toRad(loclon);
            var lon1 = toRad(lon);
            var lat2 = toRad(loclat);
            var lat1 = toRad(lat);
            /*const y = Math.sin(λ2 - λ1) * Math.cos(φ2);
            const x = Math.cos(φ1) * Math.sin(φ2) -
                      Math.sin(φ1) * Math.cos(φ2) * Math.cos(λ2 - λ1);
            const θ = Math.atan2(y, x);
            const brng = (θ * 180 / Math.PI + 360) % 360; // in degrees */
            var y = Math.Sin(lon2 - lon1) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1);
            var theta = Math.Atan2(y, x);
            return (theta * 180 / Math.PI + 360) % 360;
        }

        public double toRad(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
