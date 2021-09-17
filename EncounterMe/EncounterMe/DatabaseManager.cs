using CsvHelper;
using System;
using System.Globalization;
using System.IO;

namespace EncounterMe
{
    class DatabaseManager
    {
        public String filename { get; set; }

        public DatabaseManager(String filename)
        {
            this.filename = filename;
            //TODO
            //using (var reader = new StreamReader("Records\\" + filename + ".csv"))

        }


        public void writeToFile(Location record)
        {
            using (var writer = new StreamWriter("Records\\" + filename + ".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(record);
            }

        }

    }
}