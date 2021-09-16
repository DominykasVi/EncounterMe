using System;

namespace EncounterMe
{
    class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User("bildukas", "bildukas@gmail.com","beldziuosi");
            Console.WriteLine(user1.CompareHashPassword("nesibeldziu"));
            Console.WriteLine(user1.CompareHashPassword("beldziuosi"));
        }
    }
}
