using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Functions
{
    public class IDGenerator
    {
        //lazy, thread-safe singleton pattern, based on https://csharpindepth.com/articles/singleton
        private static readonly Lazy<IDGenerator> lazy =
        new Lazy<IDGenerator>(() => new IDGenerator());

        public static IDGenerator Instance { get { return lazy.Value; } }

        private IDGenerator()
        {
        }

        private uint id = 0;

        private bool idIsSet = false;

        private Hashtable locations = new Hashtable();
        public void setID (List<Location> loc)
        {
            if (loc != null)
            {
                foreach (Location location in loc)
                {
                    locations.Add(location.ID, location);
                    if (location.ID > id) id = location.ID;
                }
            }
            idIsSet = true;
        }

        public uint getID (Location loc)
        {
            if (idIsSet == true)
            {
                id++;
                locations.Add(id, loc);
                return id;
            }
            else
            {
                throw new Exception("ID is not set!");
            }
        }

        public Location getLocationByID (uint ID)
        {
            if (!locations.ContainsKey(ID)) throw new Exception("ID not found!");
            else return (Location)locations[ID];
        }
    }
}
