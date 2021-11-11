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
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using EncounterMe.Classes;
using EncounterMe.Interfaces;
using System.Net.Http;
using MapApp.Pages;

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
        DatabaseManager db;
        Circle userSearchCircle;

        Position userPosition;
        Distance searchRadius;

        //popup pages
        public SearchEncounter searchEncounterPage;

        public List<EncounterMe.Classes.Attribute> attributes;
        //LocationAttributes filterList;
        //List<LocationAttributes> attributeList;

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

            //db.writeToFile(AddLocations());
            //MyMap.CustomPins = AddPins();
            //Task.Delay(2000);
            var errorLogger = new AppLogger();

            InitMap(errorLogger);
            //UpdateMap();
        }

        //Dominykas: redudndant but left for future reference
        private List<CustomPin> AddPins()
        {
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
            return pinList;
        }

        //redundant, can delete
        private List<EncounterMe.Location> AddLocations()
        {
            //left for first time initialization, remove later
            this.db = new DatabaseManager(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test", new DatabaseLogger());//it works i think// I made it work ;)
            IDGenerator idg = IDGenerator.Instance;
            idg.setID(new List<EncounterMe.Location> { });

            EncounterMe.Location location1 = new EncounterMe.Location("VU MIF Naugardukas", 54.67518129701089, 25.273545582365784);
            EncounterMe.Location location2 = new EncounterMe.Location("VU MIF Baltupiai", 54.729775633971855, 25.263535399566603);
            EncounterMe.Location location3 = new EncounterMe.Location("M. Mažvydo Nacionalinė Biblioteka", 54.690803584492194, 25.263577022718472);
            EncounterMe.Location location4 = new EncounterMe.Location("Jammi", 54.68446369057142, 25.273091438331683);
            EncounterMe.Location location5 = new EncounterMe.Location("Mo Muziejus", 54.6791655393238, 25.277288631477447);
            EncounterMe.Location location6 = new EncounterMe.Location("Reformatu Skveras", 54.6814502183355, 25.276301578559966);

            List<EncounterMe.Location> locations = new List<EncounterMe.Location>();
            locations.Add(location1);
            locations.Add(location2);
            locations.Add(location3);
            locations.Add(location4);
            locations.Add(location5);
            locations.Add(location6);

            return locations;
        }

        //should be put in database
        private List<EncounterMe.Classes.Attribute> AddAttributes()
        {
            List<EncounterMe.Classes.Attribute> attributes = new List<EncounterMe.Classes.Attribute>();
            EncounterMe.Classes.Attribute att1 = new EncounterMe.Classes.Attribute("Cafe", "coffee.png");
            EncounterMe.Classes.Attribute att2 = new EncounterMe.Classes.Attribute("Forest", "tree.png");
            EncounterMe.Classes.Attribute att3 = new EncounterMe.Classes.Attribute("Water Object", "waterfall.png");
            EncounterMe.Classes.Attribute att4 = new EncounterMe.Classes.Attribute("Restaurant", "coffee.png");
            attributes.Add(att1);
            attributes.Add(att2);
            attributes.Add(att3);
            attributes.Add(att4);
            return attributes;
        }


        private async void InitMap(ILogger errorLogger)
        {
            //refactor code
            //read saved locations and put them into object, that google maps can read
            try
            {
                //creating attributes, only for testing now
                attributes = AddAttributes();

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
                    //StrokeColor = Color.FromHex("#88FF0000"),
                    StrokeColor = Color.FromHex("#6CD4FF"),
                    StrokeWidth = 8,
                    FillColor = Color.FromRgba(108,212,255,57)
                    //FillColor = Color.FromHex("#88FFC0CB")
                };
                MyMap.MapElements.Add(userSearchCircle);

                MyMap.IsShowingUser = true;
            }
            catch (Exception ex)
            {
                errorLogger.logErrorMessage("AppError when getting user location" + "\n" + ex.ToString());
                Debug.WriteLine(ex);
            }
        }

        async void RedirectPage(object sender, EventArgs e)
        {
            //beta version
            await Navigation.PushAsync(new UserPage());
        }

        public async void PopupSearchEncounter(object sender, EventArgs e)
        {
            //SearchEncounter page pops out
            MoveMap(-0.015, 0, 2);
            if(searchEncounterPage == null)
                await Navigation.PushPopupAsync(searchEncounterPage = new Pages.SearchEncounter(this));
            else
                await Navigation.PushPopupAsync(searchEncounterPage);

        }

        public async void MoveMap(double latitude = 0, double longitude = 0, int kilometers = 3)
        {
            //moving map for optimal view
            var myPosition = await CrossGeolocator.Current.GetPositionAsync();
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(myPosition.Latitude + latitude, myPosition.Longitude + longitude), Distance.FromKilometers(kilometers)));
        }

        public void SliderValueChanged(Slider RadiusSlider)
        {
            //When the slider value is changed on SearchEncounter the map display changes
            searchRadius = new Distance(RadiusSlider.Value);
            userSearchCircle.Radius = searchRadius;
        }

        private static readonly HttpClient client = new HttpClient();
        public async void SearchForPlace()
        {
            //read database and save locations locally
            //in the future might use stream, so as not to store locations locally, or do calculation on sql
            MyMap.Pins.Clear();
            //Dominykas TODO: Add handling of null exception, also republish webserver
            LocationToFind sendLocation = new LocationToFind(userPosition.Latitude, userPosition.Longitude, searchRadius.Kilometers);
            
            var json = JsonConvert.SerializeObject(sendLocation);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://testwebserverapi.azurewebsites.net/api/Location/FindLocation";
            using HttpClient client = new HttpClient();

            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;
            EncounterMe.Location locationToFind = JsonConvert.DeserializeObject<EncounterMe.Location>(result);

            //var response = await client.PostAsync("https://localhost:44355/api/FindLocation", content);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);

            if (locationToFind == null)
                await DisplayAlert("Could Not Find Location", "We could not find any locations in your selected area. Please change your distance or location.", "OK");
            else
            {
                MyMap.Pins.Add(new Pin
                {
                  Position = locationToFind.getPosition(),
                  Label = locationToFind.Name,
                  Address = locationToFind.Name,
                });
                searchEncounterPage.ShowLocation(locationToFind);
            }

            //var locationList = db.readFromFile<EncounterMe.Location>();
            //foreach (EncounterMe.Location location in locationList)
            //{
            //    var dist = (double) location.distanceToUser((float)userPosition.Latitude, (float)userPosition.Longitude);
            //    //if (dist <= searchRadius.Kilometers && ((location.attributes & filterList) > 0))
            //    if (dist <= searchRadius.Kilometers)
            //    {
                    
            //    }
            //}
            //MyMap.ItemsSource = placesList;
            //UpdateMap();
            //text.Text = new EncounterMe userPosition.Latitude.ToString();
        }
        /*
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
        */
        /*
        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {   
            await Navigation.PushAsync(new  UserPage());
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
        }
        */
        /*
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
        }*/
    }
}
