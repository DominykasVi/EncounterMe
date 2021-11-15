using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EncounterMe;
using EncounterMe.Functions;

namespace WebServer.Controllers
{
    public class UserController : ApiController
    {
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
