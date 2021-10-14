using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using System.Runtime.Serialization;

namespace EncounterMe
{
    public enum AccessLevel
    {
        User, Admin
    }

    [Serializable]
    public class User : ISerializable
    {
        public string name { get; set; }
        public string email { get; set; }
        public byte[] hashpassword { get; set; }
        public AccessLevel accessLevel { get; set; }



        public User() { }

        public User(string name, string email, string password)
        {
            this.name = name;
            this.email = email;
            hashpassword = HashPassword(password);
            accessLevel = AccessLevel.User;
        }

        public User(SerializationInfo info, StreamingContext context)
        {
            name = (string)info.GetValue("Username", typeof(string));
            email = (string)info.GetValue("Email", typeof(string));
            hashpassword = (byte[])info.GetValue("Password", typeof(byte[]));
            accessLevel = (AccessLevel)info.GetValue("Password", typeof(AccessLevel));
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Username", name);
            info.AddValue("Email", email);
            info.AddValue("Password", hashpassword);
            info.AddValue("AccessLevel", accessLevel);
        }


    }

    public class AccessRights
    {
        public AccessLevel accessLevel { get; set; }
        public String accessName { get; set; }
    }
}
