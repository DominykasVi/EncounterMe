using System.Collections.Generic;

namespace EncounterMe.Interfaces
{
    public interface IDatabaseManager
    {
        List<T> readFromFile<T>();
        void writeToFile<T>(List<T> records);
    }
}