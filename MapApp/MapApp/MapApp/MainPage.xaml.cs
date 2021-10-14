using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using EncounterMe.Functions;
using EncounterMe;
using Xamarin.Essentials;
using Plugin.Geolocator;

namespace MapApp
{
    [DesignTimeVisible(false)]

    public static class MyExtendedMethods
    {
        public static Position getPosition(this EncounterMe.Location loc)
        {
            return new Position(loc.Latitude, loc.Longtitude);
            
        }
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            /*Dominykas TODO: clean up code
                              make search feature work
                              delete function in database
                              draw radius based and live location*/

            //Request accesto to location and storage
            InitializeComponent();
            var status = Permissions.RequestAsync<Permissions.StorageWrite>();
            var locStatus = Permissions.RequestAsync<Permissions.LocationWhenInUse>();


            //string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.csv");

            //left for first time initialization, remove later
            this.db = new DatabaseManager(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test");//it works i think// I made it work ;)
            IDGenerator idg = IDGenerator.Instance;
            idg.setID(new List<EncounterMe.Location> { });
            EncounterMe.Location location1 = new EncounterMe.Location(Name: "VU MIF Naugardukas", Latitude: 54.67518129701089, Longtitude: 25.273545582365784);
            EncounterMe.Location location2 = new EncounterMe.Location(Name: "VU MIF Baltupiai", Latitude: 54.729775633971855, Longtitude: 25.263535399566603);
            EncounterMe.Location location3 = new EncounterMe.Location(Name: "M. Mažvydo Nacionalinė Biblioteka", Latitude: 54.690803584492194, Longtitude: 25.263577022718472);
            EncounterMe.Location location4 = new EncounterMe.Location(Name: "Jammi", Latitude: 54.68446369057142, Longtitude: 25.273091438331683);
            
      

            List<EncounterMe.Location> locations = new List<EncounterMe.Location>();
            locations.Add(location1);
            locations.Add(location2);
            locations.Add(location3);
            locations.Add(location4);


   
            db.writeToFile(locations);


            Task.Delay(2000);
            InitMap();
            //UpdateMap();
        }

        List<Place> placesList = new List<Place>();
        DatabaseManager db;
        Circle userSearchCircle;

        Position userPosition;
        Distance searchRadius;


        private async void InitMap()
        {
            //read saved locations and put them into object, that google maps can read
            try 
            { 


                //Move view to current location
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var myPosition = await locator.GetPositionAsync();
                userPosition = new Position(myPosition.Latitude, myPosition.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(userPosition, Distance.FromKilometers(3)));

                //initiazlize search circle
                userSearchCircle = new Circle
                {
                    Center = userPosition,
                    Radius = new Distance(0),
                    StrokeColor = Color.FromHex("#88FF0000"),
                    StrokeWidth = 8,
                    FillColor = Color.FromHex("#88FFC0CB")
                };
                MyMap.MapElements.Add(userSearchCircle);

                MyMap.IsShowingUser = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private async void UpdateMap()
        {
            try
            {

                //List < string > dispList= new List<string>();


                //foreach (Place place in placesList)
                //{
                //    dispList.Add(place.PlaceName);
                //}

                
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //create circle around user


                // Add the Circle to the map's MapElements collection
                

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private void SliderValueChanged(Object sender, ValueChangedEventArgs e)
        {
            SliderValue.Text = "Selected radius is: " + RadiusSlider.Value.ToString() + " m.";
            searchRadius = new Distance(RadiusSlider.Value);
            userSearchCircle.Radius = searchRadius;
        }

        private void SearchForPlace(Object sender, EventArgs args)
        {
            //read database and save locations locally
            //in the future might use stream, so as not to store locations locally, or do calculation on sql
            //placesList.Clear();
            MyMap.Pins.Clear();

            var locationList = db.readFromFile<EncounterMe.Location>();
            foreach (EncounterMe.Location location in locationList)
            {
                var dist = (double) location.distanceToUser((float)userPosition.Latitude, (float)userPosition.Longitude);
                //Console.WriteLine(searchRadius.Kilometers.ToString());
                if (dist <= searchRadius.Kilometers)
                {
                    //placesList.Add(new Place
                    //{
                    //    PlaceName = location.Name,
                    //    Address = location.Name,
                    //    //Location = location.geometry.location,
                    //    Position = location.getPosition(),
                    //    //Icon = place.icon,
                    //    //Distance = $"{GetDistance(lat1, lon1, place.geometry.location.lat, place.geometry.location.lng, DistanceUnit.Kiliometers).ToString("N2")}km",
                    //    //OpenNow = GetOpenHours(place?.opening_hours?.open_now)
                    //});
                    MyMap.Pins.Add(new Pin
                    {
                        Position = location.getPosition(),
                        Label = location.Name,
                        Address = location.Name
                    });
                }

            }

            //MyMap.ItemsSource = placesList;
            //UpdateMap();

            //text.Text = new EncounterMe userPosition.Latitude.ToString();
        }

        private void ShowAll(Object sender, EventArgs args)
        {
            //read database and save locations locally
            //in the future might use stream, so as not to store locations locally, or do calculation on sql
            //placesList.Clear();
            MyMap.Pins.Clear();

            var locationList = db.readFromFile<EncounterMe.Location>();
            foreach (EncounterMe.Location location in locationList)
            {
                MyMap.Pins.Add(new Pin
                {
                    Position = location.getPosition(),
                    Label = location.Name,
                    Address = location.Name
                });
            }
        }


    }
}
