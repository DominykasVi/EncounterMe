using EncounterMe.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe
{
    class Location : IComparable
    {
        public int ID { get; set; }
        public String Name { get; set; }
        //Coordiantes are to be written in Decimal Degree (DD) notation. See more here: https://en.wikipedia.org/wiki/Decimal_degrees
        //Might not require duoble precision, can be switched to float later
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        //public Location(int id, String name, double northCoord, double eastCoord)
        public Location(int ID, String Name, double Latitude, double Longtitude)
        {
            this.ID = ID;
            this.Name = Name;
            this.Latitude = Latitude;
            this.Longtitude = Longtitude;
        }

        public double distanceToUser(double lat, double lon)
        {
            return 6372.795477598 * Math.Acos(Math.Sin(this.Latitude * Math.PI / 180.00) * Math.Sin(lat * Math.PI / 180.00) + Math.Cos(this.Latitude * Math.PI / 180.00) * Math.Cos(lat * Math.PI / 180.00) * Math.Cos((this.Longtitude - lon) * Math.PI / 180));
        }

        public int CompareTo(object obj)
        {
            Location other = (Location)obj;
            return (int)(this.distanceToUser(temp_Location.currLatitude, temp_Location.currLongitude) - other.distanceToUser(temp_Location.currLatitude, temp_Location.currLongitude));
            //While user location isn't implemented we are using the temp_Location class
        }
    }
}

