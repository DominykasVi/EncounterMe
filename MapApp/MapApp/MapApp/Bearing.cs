using System;
using System.Collections.Generic;
using System.Text;

namespace MapApp
{
    class Bearing
    {
        private static readonly Lazy<Bearing> lazy =
        new Lazy<Bearing>(() => new Bearing());

        public static Bearing Instance { get { return lazy.Value; } }

        private Bearing()
        {
        }

        public double bearingToUser { get; set; } 
    }
}
