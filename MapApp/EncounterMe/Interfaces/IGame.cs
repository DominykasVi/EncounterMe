using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Interfaces
{
    public interface IGame
    {
        public Location getLocationToFind(List<Location> Locations, float Lat, float Long, double distance);

        public void isLocationFound(Location loc, float lat, float lon);

        public float distanceBetweenPoints(float firstLat, float firstLon, float secondLat, float secondLon);

        public int getRadiusFromUser();

        public void showLocationInformation(Location loc);
    }
}
