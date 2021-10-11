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

            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.csv");

            //left for first time initialization, remove later
            this.db = new DatabaseManager(_fileName, "Test");//it works i think// I made it work ;)
            EncounterMe.Location location1 = new EncounterMe.Location(001, "VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            EncounterMe.Location location2 = new EncounterMe.Location(002, "VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            EncounterMe.Location location3 = new EncounterMe.Location(003, "Test", 54.729775633971855, 25.263535399566603);
            List<EncounterMe.Location> locations = new List<EncounterMe.Location>();
            locations.Add(location1);
            locations.Add(location2);
            locations.Add(location3);
            db.writeToFile(locations);


            Task.Delay(2000);
            UpdateMap();
        }

        List<Place> placesList = new List<Place>();
        DatabaseManager db;
        Circle userSearchCircle;

        private async void UpdateMap()
        {
            try
            {
                List < string > dispList= new List<string>();

                 
                var locationList = db.readSavedLocations();


                //read saved locations and put them into object, that google maps can read
                foreach (EncounterMe.Location location in locationList)
                {
                    placesList.Add(new Place
                    {
                        PlaceName = location.Name,
                        Address = location.Name,
                        //Location = location.geometry.location,
                        Position = new Position(location.Latitude, location.Longtitude),
                        //Icon = place.icon,
                        //Distance = $"{GetDistance(lat1, lon1, place.geometry.location.lat, place.geometry.location.lng, DistanceUnit.Kiliometers).ToString("N2")}km",
                        //OpenNow = GetOpenHours(place?.opening_hours?.open_now)
                    });
                }

                foreach (Place place in placesList)
                {
                    dispList.Add(place.PlaceName);
                }
                //Move view to current location
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var myPosition = await locator.GetPositionAsync();
                Position _position = new Position(myPosition.Latitude, myPosition.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromKilometers(3)));
                
                MyMap.ItemsSource = placesList;
                MyMap.IsShowingUser = true;
                //create circle around user
                userSearchCircle = new Circle
                {
                    Center = _position,
                    Radius = new Distance(250),
                    StrokeColor = Color.FromHex("#88FF0000"),
                    StrokeWidth = 8,
                    FillColor = Color.FromHex("#88FFC0CB")
                };

                // Add the Circle to the map's MapElements collection
                MyMap.MapElements.Add(userSearchCircle);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void SliderValueChanged(Object sender, ValueChangedEventArgs e)
        {
            SliderValue.Text = "Selected radius is: " + RadiusSlider.Value.ToString();
            userSearchCircle.Radius = new Distance(RadiusSlider.Value);
        }

    }
}
