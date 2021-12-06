using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        private int id = 0;

        private bool idIsSet = false;

        private Hashtable locations = new Hashtable();
        public void setID (List<Location> loc)
        {
            if (loc != null)
            {
                foreach (Location location in loc)
                {
                    locations.Add(location.Id, location);
                    if (location.Id > id) id = location.Id;
                }
            }
            idIsSet = true;
        }

        public int getID (Location loc)
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
        //TODO reiview func, do we really need to save the list in memory?
        public Location getLocationByID (uint ID)
        {
            if (!locations.ContainsKey(ID)) throw new Exception("ID not found!");
            else return (Location)locations[ID];
        }
    }
}
