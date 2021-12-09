using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        // GET api/<controller>
        
        // GET api/<controller>/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

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
