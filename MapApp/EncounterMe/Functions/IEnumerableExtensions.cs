using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Functions
{
    public static class IEnumerableExtensions
    {
        public static T weightedRandom <T>(this IEnumerable<T> enumerable, Func<T, float> weightFunc)
        {
            float totalWeight = 0;
            T selected = default(T);
            Random random = new Random();
            foreach (var location in enumerable)
            {
                float weight = weightFunc(location);
                float r = (float)random.NextDouble() * (totalWeight + weight);
                if (r >= totalWeight)
                {
                    selected = location;
                }
                totalWeight += weight;
            }
            return selected;
        }
    }
}
