using EncounterMe;
using EncounterMe.Classes;
using EncounterMe.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        DatabaseManager db;
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            FistInit();
            return View();
        }

        private void FistInit()
        {
            db.writeToFile(AddLocations());

        }

        private List<EncounterMe.Location> AddLocations()
        {
            //left for first time initialization, remove later
            Debug.Write("#################################################################");
            Debug.Write(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            Debug.Write("#################################################################");

            this.db = new DatabaseManager(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test", new DatabaseLogger());//it works i think// I made it work ;)
            IDGenerator idg = IDGenerator.Instance;
            idg.setID(new List<EncounterMe.Location> { });

            EncounterMe.Location location1 = new EncounterMe.Location("VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            EncounterMe.Location location2 = new EncounterMe.Location("VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            EncounterMe.Location location3 = new EncounterMe.Location("M. Mažvydo Nacionalinė Biblioteka", 54.690803584492194, 25.263577022718472);
            EncounterMe.Location location4 = new EncounterMe.Location("Jammi", 54.68446369057142, 25.273091438331683);
            EncounterMe.Location location5 = new EncounterMe.Location("Mo Muziejus", 54.6791655393238, 25.277288631477447);
            EncounterMe.Location location6 = new EncounterMe.Location("Reformatu Skveras", 54.6814502183355, 25.276301578559966);
            EncounterMe.Location location7 = new EncounterMe.Location("Pilaite Jammi", 54.7073118, 25.1846521);

            List<EncounterMe.Location> locations = new List<EncounterMe.Location>();
            locations.Add(location1);
            locations.Add(location2);
            locations.Add(location3);
            locations.Add(location4);
            locations.Add(location5);
            locations.Add(location6);
            locations.Add(location7);

            return locations;
        }
    }
}
