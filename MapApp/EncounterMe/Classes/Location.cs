using EncounterMe.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
//might need importing


namespace EncounterMe
{
    [XmlRoot(ElementName = "Location", DataType = "Location", IsNullable = true)]
    public class Location : IComparable
    {
        private const float circumference = (float) 6372.795477598;
        public uint ID { get; set; }
        public String Name { get; set; }
        //Coordiantes are to be written in Decimal Degree (DD) notation. See more here: https://en.wikipedia.org/wiki/Decimal_degrees
        //Might not require duoble precision, can be switched to float later
        public float Latitude { get; set; }
        public float Longtitude { get; set; }
        //public Position Position { get; set; }

        private uint Upvote = 0;

        private uint Downvote = 0;

        public Location(uint ID, String Name, double Latitude, double Longtitude)
        {
            this.ID = ID;
            this.Name = Name;
            this.Latitude = (float) Latitude;
            this.Longtitude = (float) Longtitude;
            //just testing it
            //this.Position = new Position(Latitude, Longtitude);
        }

        public Location(String Name, double Latitude, double Longtitude)
        {
            this.Name = Name;
            this.Latitude = (float)Latitude;
            this.Longtitude = (float)Longtitude;
        }

        public float distanceToUser(float lat, float lon)
        {
            return (float) (circumference *
                Math.Acos(Math.Sin(this.Latitude *Math.PI / 180.00) *
                Math.Sin(lat * Math.PI / 180.00) +
                Math.Cos(this.Latitude * Math.PI / 180.00) *
                Math.Cos(lat * Math.PI / 180.00) *
                Math.Cos((this.Longtitude - lon) * Math.PI / 180)));
        }

        public int CompareTo(object obj)
        {
            Location other = (Location)obj;
            return -1;
            //return (int)(this.distanceToUser(temp_Location.currLatitude, temp_Location.currLongitude) - other.distanceToUser(temp_Location.currLatitude, temp_Location.currLongitude));
            //While user location isn't implemented we are using the temp_Location class
        }

        public void upvote ()
        {
            Upvote++;
        }

        public void downvote()
        {
            Downvote++;
            
        }

        public float getRating ()
        {
            if (Upvote + Downvote == 0) return 0;
            else return (float)(Upvote * 1.0 / (Upvote + Downvote) * 100);
        }
    }
}

