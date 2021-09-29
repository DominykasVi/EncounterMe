using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EncounterMe.Functions
{
    public class LogIn
    {
        public bool CheckPassword(string username, string password)             
            //Class for checking if the password is correct
        {
            return true;                                                         
            //Not sure how we're gonna store user cridentials, so didn't do this yet
        }

        public User CreateUser (string username, string email, string password)
        {
            if (ExistingUsername(username) && ValidPassword(password))
                //the email is not validated yet
                return new User(username, email, password);
            else
                return null;
        }

        public bool ExistingUsername(string input)                  
            //Class that should check if the entered username already exists or not
        {
            return false;                                        
            //Not sure how we're gonna store user cridentials, so didn't do this yet
        }

        //A class for verifying emails needs to be created

        public bool ValidPassword(string input)
        {
            Regex hasNumbers = new Regex("[0-9]");
            Regex hasLowercase = new Regex("[a-z]");
            Regex hasUppercase = new Regex("[A-Z]");

            return hasNumbers.IsMatch(input) && hasLowercase.IsMatch(input) && hasUppercase.IsMatch(input);
            //Currently doesn't check for other characters, couldn't figure it out
        }
    }
}
