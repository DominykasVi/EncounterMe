using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using EncounterMe.Interfaces;

delegate bool ValidationDel (string toBeValidated);

namespace EncounterMe.Functions
{
    public class LogInManager
    {
        private IDatabaseManager um;
        public LogInManager(IDatabaseManager db)
        {
            um = db;
        }
        public User CheckPassword(string username, string password)
        {
            var user = um.readFromFile<User>().Where(x => x.Name == username).FirstOrDefault();
            if (user != null && user.CompareHashPassword(password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User CreateUser (string username, string email, string password)
        {
            var user = um.readFromFile<User>().Where(x => x.Name == username).FirstOrDefault();

            ValidationDel vd = delegate (string x)
            {
                // regex that validates whether the password has more than 8 characters and at least one capital, non-capital and number character
                Regex validPassword = new Regex("^.*(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).*$");
                return validPassword.IsMatch(x);
            };
            ValidationDel ve = delegate (string x)
            {
                // standard email verification regex
                Regex validEmail = new Regex("^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$");
                return validEmail.IsMatch(x);
            };

            if (user == null && vd(password) && ve(email))
            {
                user = new User(username, email, password);
                um.writeToFile<User>(new List<User>() { user });
                return user;
            }
            else if (user != null)
            {
                throw new Exception("User already exists!");
            }
            else if (!vd(password))
            {
                throw new Exception("Password should be at least 8 characters long and contain at least one number, one non-capital and one capital letter.");
            }
            else if (!ve(email))
            {
                throw new Exception("Invalid email.");
            }
            else
            {
                throw new Exception("Unknown error");
            }
        }
    }
}
