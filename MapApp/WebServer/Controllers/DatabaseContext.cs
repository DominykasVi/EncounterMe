using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EncounterMe;


namespace WebServer.Controllers
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}