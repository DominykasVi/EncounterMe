using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

//TODO:Add check if path exists, if records are not null (I think they should be new functions) (for write, read etc.)
//Also should ID be generated by taking the last record from database, it shuld not be hardcoded (we missed that)

namespace EncounterMe
{
    public struct DatabaseManager
    {
        public String path { get; set; }



        public DatabaseManager(string newPath, string filename)
        {
            //var name = filename + ".csv";
           
            path = Path.Combine(newPath, filename + ".xml");


            //Creates file if it does not exist
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
                
                //read all records
                var readRecords = readFromFile<T>();

                //check if porps are same (except ID), if any prop is different, consider a new object
                foreach (var readRecord in readRecords)
                {
                    Type myType = readRecord.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                    bool equalObjects = true;
                    foreach (var passedRecord in records.ToList())
                    {
                        foreach (PropertyInfo prop in props)
                        {
                            if (!prop.Name.Contains("ID"))
                            {
                                object readValue = prop.GetValue(readRecord, null);
                                object passedValue = prop.GetValue(passedRecord, null);
                                if (!readValue.Equals(passedValue))
                                {
                                    equalObjects = false;
                                }
                            }
                        }
                        if (equalObjects)
                        {
                            records.Remove(passedRecord);
                        }
                    }
                }



                //Exit if list is empty
                if (!records.Any())
                {
                    return;
                }

                readRecords.AddRange(records);

                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                TextWriter writer = new StreamWriter(path);
                serializer.Serialize(writer, readRecords);
                writer.Close();
            }

        }

        public List<T> readFromFile<T>()
        {
            //read file
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            FileStream fs = new FileStream(path, FileMode.Open);
            var result = serializer.Deserialize(fs);
            fs.Close();
            return (List<T>) result;
        }

        public String getPath()
        {
            //read file
            var newDir = Directory.GetCurrentDirectory();
            return newDir;
        }

    }
}