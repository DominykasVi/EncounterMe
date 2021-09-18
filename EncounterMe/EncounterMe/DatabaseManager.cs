using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EncounterMe
{
    class DatabaseManager
    {
        public String path { get; set; }

        public DatabaseManager(String filename)
        {
            this.path = "Records\\" + filename + ".csv";
            //Creates folder and file, if they are not initialized
            if (!Directory.Exists("Records"))
            {
                try
                {
                    Directory.CreateDirectory("Records");
                    Console.WriteLine("Directory created");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }

            if (!File.Exists(path))
            {
                try
                {
                    File.Create(path).Dispose();
                    Console.WriteLine("File created");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
            
        }


        public void writeToFile(List<Location> records)
        {
            //Checks file size, if file is empty (less than 10 KB), if so cretes file, else appends to it
            FileInfo fi = new FileInfo(path);

            if (fi.Length < 10)
            {
                using (var writer = new StreamWriter(path))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
            }
            else
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                using (var stream = File.Open(path, FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(records);
                }
            }

        }

        public IEnumerable<Location> readSavedLocations()
        {
            //read file
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Location>().ToList();
                return records;
            }

        }

    }
}