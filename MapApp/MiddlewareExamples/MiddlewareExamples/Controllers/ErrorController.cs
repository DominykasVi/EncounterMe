using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace MiddlewareExamples.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            using (StreamWriter writer = new StreamWriter("AppLogs.txt"))
            {
                writer.WriteLine("Error");
                writer.WriteLine(value);
                writer.WriteLine(DateTime.Now);
            }
            return value;
        }


    }
}
