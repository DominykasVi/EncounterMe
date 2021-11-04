using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace EncounterMe.Functions
{
    public class UserManager
    {
        private string path = "users.xml";
        List<User> GetUsersFromMemory()
        {
            List<User> users;
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            using (FileStream reader = File.OpenRead(path))
            {
                users = (List<User>)serializer.Deserialize(reader);
            }
            return users;
        }

        public User FindUser (string username)
        {
            List<User> users = GetUsersFromMemory();
            User user = users.Where(x => x.name == username).FirstOrDefault();
            return user;
        }

        public void SaveUser(User user)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            List<User> users = GetUsersFromMemory();
            users.Add(user);
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, users);
            }
        }
    }
}
