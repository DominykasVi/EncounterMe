using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Interfaces
{
    public interface ILogger
    {
        void logErrorMessage(String message);
        void logErrorList<T>(List<T> list);
        //void logErrortrace();
        //void logErrorDevice();
    }
}
