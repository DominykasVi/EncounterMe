using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EncounterMe.Interfaces;

namespace EncounterMe.Classes
{
    class LogicLogger : ILogger
    {
        public void logErrorList<T>(List<T> list)
        {
            //TODO: implement random error list names, check for empty
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

            using (StreamWriter sw = File.AppendText("logs/GameLogicLogs.txt"))
            {
                sw.WriteLine("Error");
                sw.WriteLine("Error list name: ErrorList");
                sw.WriteLine(message);
                sw.WriteLine(DateTime.Now);
            }

        }
    }
}
