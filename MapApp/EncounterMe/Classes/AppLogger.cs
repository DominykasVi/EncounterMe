using EncounterMe.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMe.Classes
{
    //used in the app
    public class AppLogger : ILogger
    {

        //private static readonly HttpClient client = new HttpClient();
        public async void logErrorMessage(string message, HttpClient  client)
        {
            var task = SendErrorToServer(message, client);
            task.Wait();     
        }
        public async void logErrorMessage(string message)
        {
            var task = SendErrorToServer(message, new HttpClient());
            task.Wait();
        }

        private async Task<string> SendErrorToServer(string message, HttpClient client) 
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", message)
            });

            var response = await client.PostAsync("https://localhost:44355/api/error", content);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            return responseString;
        }
    }
}
