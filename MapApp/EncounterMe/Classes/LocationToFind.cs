using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Classes
{
    public class LocationToFind : Location
    {
        public LocationToFind(double latitude, double longtitude, double distance)
        {
            this.Latitude = (float) latitude;
            this.Longtitude = (float) longtitude;
            this.Distance = distance;
        }
        public double Distance { get; set; }
    }
}
