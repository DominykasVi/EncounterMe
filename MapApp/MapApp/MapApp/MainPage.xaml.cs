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
using Windows.UI.Xaml.Controls.Maps;
using Windows.Storage.Streams;

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
        //Xamarin.Forms.Maps.Map MyMap = new Xamarin.Forms.Maps.Map();
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
            
            EncounterMe.Location location1 = new EncounterMe.Location("VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            EncounterMe.Location location2 = new EncounterMe.Location("VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            EncounterMe.Location location3 = new EncounterMe.Location("M. Mažvydo Nacionalinė Biblioteka", 54.690803584492194, 25.263577022718472, LocationAttributes.FarFromCityCenter);
            EncounterMe.Location location4 = new EncounterMe.Location("Jammi", 54.68446369057142, 25.273091438331683, LocationAttributes.DifficultToFind);
            EncounterMe.Location location5 = new EncounterMe.Location("Mo Muziejus", 54.6791655393238, 25.277288631477447, LocationAttributes.CloseToCityCenter);
            EncounterMe.Location location6 = new EncounterMe.Location("Reformatu Skveras", 54.6814502183355, 25.276301578559966, LocationAttributes.DifficultTerrain | LocationAttributes.DifficultToFind);

            List<EncounterMe.Location> locations = new List<EncounterMe.Location>();
            locations.Add(location1);
            locations.Add(location2);
            locations.Add(location3);
            locations.Add(location4);
            locations.Add(location5);
            locations.Add(location6);



            db.writeToFile(locations);


            var pinList = new List<CustomPin>();
            var locationList = db.readFromFile<EncounterMe.Location>();
            foreach (EncounterMe.Location location in locationList)
            {
                //MyMap.Pins.Add(
                var pin = new CustomPin
                {
                    Position = location.getPosition(),
                    Label = location.Name,
                    Address = location.Name,
                    Name = "Xamarin",
                    Url = "http://xamarin.com/about/"
                };
                pinList.Add(pin);
                //MyMap.Pins.Add(pin);
                // MyMap.CustomPins = new List<CustomPin> { pin };

            }
            MyMap.CustomPins = pinList;

            Task.Delay(2000);
            InitMap();
            //UpdateMap();
        }

        List<Place> placesList = new List<Place>();
        DatabaseManager db;
        Circle userSearchCircle;

        Position userPosition;
        Distance searchRadius;

        LocationAttributes filterList;
        List<LocationAttributes> attributeList;
        
        private async void InitMap()
        {
            //read saved locations and put them into object, that google maps can read
            try
            {

                GenerateFilterButtons();
                //Move view to current location
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var myPosition = await locator.GetPositionAsync();
                userPosition = new Position(myPosition.Latitude, myPosition.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(userPosition, Distance.FromKilometers(3)));

                // MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(47.6370891183, -122.123736172), Distance.FromKilometers(3)));
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
                if (dist <= searchRadius.Kilometers && ((location.attributes & filterList) > 0))
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
                    MyMap.Pins.Add(new CustomPin
                    {
                        Position = location.getPosition(),
                        Label = location.Name,
                        Address = location.Name,
                        Name = "Xamarin",
                        Url = "http://xamarin.com/about/"

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
            var pinList = new List<CustomPin>();
            var locationList = db.readFromFile<EncounterMe.Location>();
            foreach (EncounterMe.Location location in locationList)
            {
                //MyMap.Pins.Add(
                var pin = new CustomPin
                {
                    Position = location.getPosition(),
                    Label = location.Name,
                    Address = location.Name,
                    Name = "Xamarin",
                    Url = "http://xamarin.com/about/"
                };
                pinList.Add(pin);
                MyMap.Pins.Add(pin);
               // MyMap.CustomPins = new List<CustomPin> { pin };

            }
            //MyMap.CustomPins = pinList;
            foreach(var pin in MyMap.CustomPins)
            {
                Console.WriteLine(pin.Label);
            }

        }

        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SecondPage());
        }

        private void GenerateFilterButtons()
        {
            LocationAttributes[] array = (LocationAttributes[])Enum.GetValues(typeof(LocationAttributes));
            attributeList = new List<LocationAttributes>(array);
            attributeList.RemoveAt(0); //enum sucks

            this.FilterPanel.RowDefinitions.Add(new RowDefinition());
            this.FilterPanel.ColumnDefinitions.Add(new ColumnDefinition());
            this.FilterPanel.ColumnDefinitions.Add(new ColumnDefinition());
            this.FilterPanel.ColumnDefinitions.Add(new ColumnDefinition());
            this.FilterPanel.ColumnDefinitions.Add(new ColumnDefinition());
            this.FilterPanel.ColumnDefinitions.Add(new ColumnDefinition());

            int i = 0;
            foreach (var attribute in attributeList)
            {
                var newButton = new Button()
                {
                    Text = attribute.ToString(),
                    FontSize = 10,
                    BackgroundColor = Color.Default
                };
                newButton.Clicked += FilterButtonClicked;
                this.FilterPanel.Children.Add(newButton, i++, 0);
            }
        }

        async void FilterButtonClicked(object sender, EventArgs args)
        {
            Button button = (sender as Button);
            LocationAttributes tag = attributeList.Find(x => x.ToString() == button.Text);
            if (button.BackgroundColor == Color.Default)
            {
                //enabled for filtering
                button.BackgroundColor = Color.DarkOrange;
                filterList = filterList | tag;
            }
            else
            {
                //disable for filtering
                button.BackgroundColor = Color.Default;
                filterList = filterList & ~tag;
            }
        }

        async void RedirectPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }
    }
}
