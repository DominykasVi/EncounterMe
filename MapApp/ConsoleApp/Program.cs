using System;
using System.Collections.Generic;
using EncounterMe.Functions;
using EncounterMe.Classes;
using EncounterMe;
using System.IO;
using System.Reflection;
using Weighted_Randomizer;
using System.Net;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace ConsoleApp
{
    //TODO add to android file, even as generate position

    class Program
    {

        static void Main(string[] args)
        {
            //test_Events();
            DominykasTest();
            string userName = Console.ReadLine();

        }

        static async Task DominykasTest() {
            //var task = DominykasTestErrors("random str");
            //task.Wait();
            var program = new Program();
            //string userName = Console.ReadLine();

            //var erro = new AppLogger();
            //erro.logErrorMessage("some value");

            LocationToFind sendLocation = new LocationToFind(54.7074356, 25.1720581, 1000);

            EncounterMe.Location locationToFind = await program.sendPostrequestAsync(sendLocation);
            Console.WriteLine(locationToFind.ToString());
            
        }

        private async Task<EncounterMe.Location> sendPostrequestAsync(LocationToFind sendLocation)
        {
            var json = JsonConvert.SerializeObject(sendLocation);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            //var response = await client.PostAsync("https://localhost:44355/api/FindLocation", content);

            var url = "https://localhost:5001/api/Location/FindLocation";
            using HttpClient client = new HttpClient();
            try
            {
                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
                //EncounterMe.Location locationToFind = JsonConvert.DeserializeObject<EncounterMe.Location>(result);
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);


                //return locationToFind;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
            //for debug
            
        }
        private static readonly HttpClient client = new HttpClient();

        static async Task<string> DominykasTestErrors(string message)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", message)
            });

            var response = await client.PostAsync("https://localhost:44355/api/error", content);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("before");
            Console.WriteLine(responseString);
            Console.WriteLine("after");
            return responseString;
            //Console.WriteLine(test);
        }

        static async Task<string> DominykasTestPost() 
        {
            Console.WriteLine("You are here");
            var wb = new WebClient();
            var data = new NameValueCollection();
            string url = "https://localhost:44355/api/Location/GetLocationList";
            data["username"] = "5";

            var response = wb.UploadValues(url, "POST", data);

            //Console.WriteLine(responseString);
            return response.ToString();
        }

        //static void TestIlogger()
        //{
        //    errorLogger.logErrorMessage("File: " + path + "not found");
        //    errorLogger.logErrorMessage("Could not sort location list");

        //}
        static void test_Events ()
        {
            GameLogic gl = new GameLogic();
            EventManager ev = new EventManager(gl);
            IDGenerator id = IDGenerator.Instance;
            id.setID(new List<Location> { });
            Location loc1 = new Location("asd", 1.0f, 1.0f);
            gl.isLocationFound(loc1, 1.0f, 1.0f);
            gl.isLocationFound(loc1, 2.0f, 3.0f);
        }

    }
}
