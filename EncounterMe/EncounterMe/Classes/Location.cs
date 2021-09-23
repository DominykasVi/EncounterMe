using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe
{
    class Location
    {
        private const float circumference = (float) 6372.795477598;
        public int ID { get; set; }
        public String Name { get; set; }
        //Coordiantes are to be written in Decimal Degree (DD) notation. See more here: https://en.wikipedia.org/wiki/Decimal_degrees
        //Might not require duoble precision, can be switched to float later
        public float Latitude { get; set; }
        public float Longtitude { get; set; }

        //public Location(int id, String name, double northCoord, double eastCoord)
        public Location(int ID, String Name, double Latitude, double Longtitude)
        {
            this.ID = ID;
            this.Name = Name;
            this.Latitude = (float) Latitude;
            this.Longtitude = (float) Longtitude;
        }

        public float distanceToUser(double lat, double lon)
        {
            return (float) (circumference * Math.Acos(Math.Sin(this.Latitude * Math.PI / 180.00) * Math.Sin(lat * Math.PI / 180.00) + Math.Cos(this.Latitude * Math.PI / 180.00) * Math.Cos(lat * Math.PI / 180.00) * Math.Cos((this.Longtitude - lon) * Math.PI / 180)));
        }

    }
}