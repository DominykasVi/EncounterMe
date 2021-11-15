using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Classes
{
    public class Attribute
    {
        public String Name { get; }
        public String Image { get; }
        public Attribute() { }

        public Attribute(String name, String image)
        {
            this.Name = name;
            this.Image = image;
        }
    }
}
