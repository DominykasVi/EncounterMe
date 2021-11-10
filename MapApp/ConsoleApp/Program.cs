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

namespace ConsoleApp
{
    //TODO add to android file, even as generate position

    class Program
    {

        static void Main(string[] args)
        {
            DominykasTest();


        }

        static async Task DominykasTest() {
            //var task = DominykasTestErrors("random str");
            //task.Wait();
            var erro = new AppLogger();
            erro.logErrorMessage("some value");
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


    }
}
