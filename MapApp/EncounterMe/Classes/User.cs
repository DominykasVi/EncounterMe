using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace EncounterMe
{
    public class User
    {
        private string name;
        private string gmail;
        private byte[] hashpassword;

        public string Name
        { get { return name; } }
        
        public string Gmail
        { get { return gmail; } }

        public User(string name, string gmail, string password)
        {
            this.name = name;
            this.gmail = gmail;
            hashpassword = HashPassword(password);
        }

        private byte[] HashPassword(string password)
        {
            var provider = new SHA256CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return provider.ComputeHash(encoding.GetBytes(password));
        }

        public bool CompareHashPassword(string password)
        {
            return hashpassword.SequenceEqual(HashPassword(password));
        }

    }
}
