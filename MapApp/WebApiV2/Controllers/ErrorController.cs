using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebApiV2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : Controller
    {

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
