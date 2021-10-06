using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

//TODO:Add check if path exists, if records are not null (I think they should be new functions) (for write, read etc.)
//Also should ID be generated by taking the last record from database, it shuld not be hardcoded (we missed that)

namespace EncounterMe
{
    public class DatabaseManager
    {
        public String path { get; set; }


        public DatabaseManager(string newPath, string filename)
        {
            //var name = filename + ".csv";
           
            path = Path.Combine(newPath, filename + ".xml");
            //Creates folder and file, if they are not initialized
            //if (!Directory.Exists("Records"))
            //{
            //    try
            //    {
            //        Directory.CreateDirectory("Records");
            //        Console.WriteLine("Directory created");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //    }

            //}

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


        public void writeToFile<T>(List<T> records)
        {


            //Checks file size, if file is empty (less than 10 KB), if so cretes file, else appends to it
            FileInfo fi = new FileInfo(path);

            if (fi.Length < 10)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                TextWriter writer = new StreamWriter(path);
                serializer.Serialize(writer, records);
                writer.Close();
                //using (var writer = new StreamWriter(path))
                //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                //{
                //    csv.WriteRecords(records);
                //}
            }
            else
            {
                ////read all records
                //var readRecords = readSavedLocations();

                ////check if any of the passed record names are in the file, if so remove them from the list
                //foreach (var readRecord in readRecords)
                //{
                //    foreach (var passedRecord in records.ToList()) 
                //    {
                //        if (readRecord.Name.Equals(passedRecord.Name))
                //        {
                //            records.Remove(passedRecord);
                //            Console.WriteLine("Duplicate " + passedRecord.Name);
                //        }
                //    }
                //}
                

                ////Exit if list is empty
                //if (!records.Any())
                //{
                //    return;
                //}
                ////write all the left records to file
                //var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                //{
                //    // Don't write the header again.
                //    HasHeaderRecord = false,
                //};
                //using (var stream = File.Open(path, FileMode.Append))
                //using (var writer = new StreamWriter(stream))
                //using (var csv = new CsvWriter(writer, config))
                //{
                //    csv.WriteRecords(records);
                //}
            }

        }

        public List<T> readFromFile<T>()
        {
            //read file
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            FileStream fs = new FileStream(path, FileMode.Open);
            var result = serializer.Deserialize(fs);
            
            //Console.WriteLine(typeof(List<T>));
            //Console.WriteLine(result.ToString());
            return (List<T>) result;
            //using (var reader = new StreamReader(path))
            //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            //{
            //    List<Location> records = csv.GetRecords<Location>().ToList();
            //    return records;
            //}

        }

        public String getPath()
        {
            //read file
            var newDir = Directory.GetCurrentDirectory();
            return newDir;
        }

    }
}