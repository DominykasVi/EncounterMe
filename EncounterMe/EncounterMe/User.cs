using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace EncounterMe
{
    class User
    {
        private string name;
        public string Name
        { get { return name; } }
        
        private string gmail;
        public string Gmail
        { get { return gmail; } }
        private byte[] hashpassword;

        public User(string name, string gmail, string password)
        {
            this.name = name;
            this.gmail = gmail;
            this.hashpassword = HashPassword(password);
        }
        private byte[] HashPassword(string password)
        {
            var provider = new SHA256CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return provider.ComputeHash(encoding.GetBytes(password));
        }

        public Boolean CompareHashPassword(string password)
        {
            return this.hashpassword.SequenceEqual(HashPassword(password));
        }

    }
}
