using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Classes
{
    class LocationList<T>
    {
        private T[] arr = new T[100];

        public T this[int i]
        {
            get { return arr[i]; }
            set { arr[i] = value; }
        }
    }
}
