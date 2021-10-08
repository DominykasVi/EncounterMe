using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EncounterMe.Classes;

namespace EncounterMe.Functions
{
    public class LogIn
    {
        private UserManager users = new UserManager();
        public User CheckPassword(string username, string password)
        {
            User user = users.FindUser(username);
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
            User user = users.FindUser(username);

            if (user == null && ValidPassword(password) && ValidEmail(email))
            {
                user = new User(username, email, password);
                users.SaveUser(user);
                return user;
            }
            else
                return null;
        }

        public bool ValidPassword(string input)
        {
            Regex hasNumbers = new Regex("[0-9]");
            Regex hasLowercase = new Regex("[a-z]");
            Regex hasUppercase = new Regex("[A-Z]");

            return hasNumbers.IsMatch(input) && hasLowercase.IsMatch(input) && hasUppercase.IsMatch(input);
            //Currently doesn't check for other characters, couldn't figure it out
        }

        public bool ValidEmail(string input)
        {
            //not implemented yet
            return true;
        }
    }
}
