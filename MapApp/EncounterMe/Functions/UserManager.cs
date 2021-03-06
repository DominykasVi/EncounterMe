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

        public void createXML ()
        {
            if (!File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                List<User> users = new List<User>();
                using (FileStream writer = File.Create(path))
                {
                    serializer.Serialize(writer, users);
                }
            }
        }
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
            User user = users.Where(x => x.Name == username).FirstOrDefault();
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

        public void printListOfUsers ()
        {
            List<User> users = GetUsersFromMemory();
            List<AccessRights> accessRights = new List<AccessRights>
            {
                new AccessRights { AccessLevel = AccessLevel.Admin, AccessName = "Admin"},
                new AccessRights { AccessLevel = AccessLevel.User, AccessName = "User"}
            };
            var query = accessRights.GroupJoin (users,
                                                rights => rights.AccessLevel,
                                                user => user.AccessLevel,
                                                (rights, userList) => new
                                                {
                                                    Users = userList,
                                                    AccessName = rights.AccessName
                                                });
            foreach (var item in query)
            {
                Console.WriteLine(item.AccessName);
                foreach (var user in item.Users)
                {
                    Console.WriteLine("\t" + user.Name);
                }
            }
        }
    }
}
