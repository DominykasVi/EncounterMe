using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiddlewareExamples.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;


        public UserController(
            ILogger<UserController> logger)
        {
            _logger = logger;
        }
        //we probably shouldn't send passwords as strings to webservice, but rather hash them in the app
        //public string SignIn(string username, string password)
        //{
        //    //DatabaseManager databaseManager = new DatabaseManager();
        //    //LogInManager logInManager = new LogInManager(databaseManager);
        //    //User user = logInManager.CheckPassword(username, password);
        //    //if (user != null)
        //    //    return null;
        //    //    //would be nice to have a serializer method ( maybe after we rework database? )
        //    //    // TODO : change null to serialized user
        //    //else
        //    //    return null;
        //}

        ////change to hash when we start hashing in app
        //public string SingUp(string username, string email, string password)
        //{
        //    //DatabaseManager databaseManager = new DatabaseManager();
        //    //LogInManager logInManager = new LogInManager(databaseManager);
        //    //User user = logInManager.CreateUser(username, email, password);
        //    //if (user != null)
        //    //    return null;
        //    //    // TODO : change null to serialized user
        //    //else
        //    //    return null;
        //}
    }
}
