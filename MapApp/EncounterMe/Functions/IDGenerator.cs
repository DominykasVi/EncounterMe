using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Functions
{
    class IDGenerator
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

        public void setID (IEnumerable<Location> loc)
        {
            foreach (Location location in loc)
            {
                if (location.ID > id) id = location.ID;
            }
            idIsSet = true;
        }

        public int getID ()
        {
            if (idIsSet == true)
            {
                id++;
                return id;
            }
            else
            {
                throw new Exception("ID is not set!");
            }
        }
    }
}
