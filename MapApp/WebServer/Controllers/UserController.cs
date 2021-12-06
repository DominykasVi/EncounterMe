using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using EncounterMe;
using EncounterMe.Functions;
using WebServer;

namespace WebServer.Controllers
{
    public class UserController : ApiController
    {
        [Route("api/User/SignIn")]
        [HttpPost]
        public string SignIn(string username, byte[] password)
        {


            return null;
        }
    }
}


//DatabaseManager databaseManager = new DatabaseManager();
//LogInManager logInManager = new LogInManager(databaseManager);
//User user = logInManager.CheckPassword(username, password);
//if (user != null)
//    return null;
////would be nice to have a serializer method ( maybe after we rework database? )
//// TODO : change null to serialized user
//else
//    return null;

//change to hash when we start hashing in app
//    public string SingUp(string username, string email, string password)
//    {
//        LogInManager logInManager = new LogInManager(databaseManager);
//        User user = logInManager.CreateUser(username, email, password);
//        if (user != null)
//            return null;
//        // TODO : change null to serialized user
//        else
//            return null;
//    }
//}