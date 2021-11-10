using EncounterMe.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMe.Classes
{
    //only use in database
    public class DatabaseLogger : ILogger
    {
        public void logErrorList<T>(List<T> list)
        {
            foreach (var item in list)
            {
                using (StreamWriter sw = File.AppendText("logs/ErrorList.txt"))
                {
                    item.ToString();
                }
            }
        }

        public void logErrorMessage(string message)
        {
            using (StreamWriter writer = new StreamWriter("logs/DatabaseLogs"))
            {
                writer.WriteLine("Error");
                writer.WriteLine(message);
                writer.WriteLine(DateTime.Now);
            }
        }

    }

}
