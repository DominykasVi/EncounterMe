using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using EncounterMe.Functions;
using EncounterMe;
using Xamarin.Essentials;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Extensions;
using EncounterMe.Classes;
using EncounterMe.Interfaces;
using System.Net.Http;
using MapApp.Pages;
using MapApp.Notification;
using Rg.Plugins.Popup.Services;

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

        //user data
        public User user;
        Circle userSearchCircle;
        public Position searchCircleCentre;
        public Position userPosition;
        public Distance searchRadius;

        //popup pages
        public SearchEncounter searchEncounterPage;
        public HintPage hintPage;

        //attribute variables
        public List<EncounterMe.Classes.Attribute> attributes;
        public List<EncounterMe.Classes.Attribute> pickedAttributes = new List<EncounterMe.Classes.Attribute>();
        
        public MainPage()
        {
            //Request accesto to location and storage
            InitializeComponent();
            var status = Permissions.RequestAsync<Permissions.StorageWrite>();
            var locStatus = Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            var errorLogger = new AppLogger();

            InitMap(errorLogger);
            //UpdateMap();

            //only for testing, all info should be in databse, delete later
            user = new User("Mr. Hamster", "mrhamster@gmail.com", "ilovehamsters");
            user.LevelPoints = 8520;
            user.AchievementNum = 10;
            user.FoundLocationNum = 23;

            
        }

        private async void TestNotif(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new Notification.NotificationPage());
        }

        private void ShrinkCircleHint(object sender, EventArgs e)
        {
            EncounterMe.Location location = new EncounterMe.Location("M. Mažvydo Nacionalinė Biblioteka", 54.690803584492194, 25.263577022718472, 10);
            var circle = new Circle
            {
                Center = location.getPosition(),
                Radius = new Distance(50),
                StrokeColor = Color.FromHex("#6CD4FF"),
                StrokeWidth = 8,
                FillColor = Color.FromRgba(108, 212, 255, 57)
            };
            MyMap.MapElements.Add(circle);
            ChangeSearchRadius(1500);
            //Navigation.PushPopupAsync(new Hints.ShrinkSearchCircle(this, location.Latitude, location.Longtitude, userPosition.Latitude, userPosition.Longitude));
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

        //redundant, can delete;; delete when database works
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

        //should be put in database;; delete when database works
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
                //creating attributes, only for testing now, delete when database works
                attributes = AddAttributes();

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
                    Radius = new Distance(0), //changed for testing
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

        async void RedirectUserPage(object sender, EventArgs e)
        {
            //Open UserPage
            if(PopupNavigation.Instance.PopupStack.Count > 0)
                await Navigation.PopAllPopupAsync();
            await Navigation.PushAsync(new UserPage(user));
        }

        public async void PopupSearchEncounter(object sender, EventArgs e)
        {
            //there is an error when you click this button too early
            //not sure why?
            //SearchEncounter page pops out
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await Navigation.PopAllPopupAsync();
            //MoveMap(-0.015, 0, 2);
            if (searchEncounterPage == null)
            {
                searchEncounterPage = new SearchEncounter(this);
                await Navigation.PushPopupAsync(searchEncounterPage);
            }
            else
            {
                searchEncounterPage.GeneratePickedAttributes();
                searchEncounterPage.Update();
                await Navigation.PushPopupAsync(searchEncounterPage);
            }
        }

        public async void MoveMap(double latitude = 0, double longitude = 0, int kilometers = 3)
        {
            //moving map for optimal view
            var myPosition = await CrossGeolocator.Current.GetPositionAsync();
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(myPosition.Latitude + latitude, myPosition.Longitude + longitude), Distance.FromKilometers(kilometers)));
        }

        public void ChangeSearchRadius(float size)
        {
            searchRadius = new Distance(size);
            userSearchCircle.Radius = searchRadius;
        }

        public void ChangeSearchCentre(Position position)
        {
            userSearchCircle.Center = position;
            searchCircleCentre = position;
        }

        private static readonly HttpClient client = new HttpClient();
        public async void SearchForPlace()
        {
            //read database and save locations locally
            //in the future might use stream, so as not to store locations locally, or do calculation on sql
            MyMap.Pins.Clear();
            var myPosition = await CrossGeolocator.Current.GetPositionAsync();
            searchCircleCentre = new Position(myPosition.Latitude, myPosition.Longitude);

            //change this when database works
            hintPage = new HintPage(this, new EncounterMe.Location("M. Mažvydo Nacionalinė Biblioteka", 54.690803584492194, 25.263577022718472, 100));
            await Navigation.PopPopupAsync();
            await Navigation.PushPopupAsync(hintPage);
            //Dominykas TODO: Add handling of null exception, also republish webserver
            //externel classs


            //*****************************************************************
            //removed for debug, return in main app
            //LocationToFind sendLocation = new LocationToFind(userPosition.Latitude, userPosition.Longitude, searchRadius.Kilometers);

            //EncounterMe.Location locationToFind = await sendPostrequestAsync(sendLocation);

            //if (locationToFind == null)
            //    await DisplayAlert("Could Not Find Location", "We could not find any locations in your selected area. Please change your distance or location.", "OK");
            //else
            //{
            //    MyMap.Pins.Add(new Pin
            //    {
            //      Position = locationToFind.getPosition(),
            //      Label = locationToFind.Name,
            //      Address = locationToFind.Name,
            //    });
            //    searchEncounterPage.ShowLocation(locationToFind);
            //}
        }

        private async Task<EncounterMe.Location> sendPostrequestAsync(LocationToFind sendLocation)
        {
            var json = JsonConvert.SerializeObject(sendLocation);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            //var response = await client.PostAsync("https://localhost:44355/api/FindLocation", content);

            var url = "https://testwebserverapi.azurewebsites.net/api/Location/FindLocation";
            using HttpClient client = new HttpClient();

            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;
            EncounterMe.Location locationToFind = JsonConvert.DeserializeObject<EncounterMe.Location>(result);

            return locationToFind;

            //for debug
            //var responseString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseString);
        }

        private void MainButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "Search for Encounter")
                PopupSearchEncounter(sender, e);
            else if ((sender as Button).Text == "View Location")
                ViewLocation();
        }

        public async void ViewLocation()
        {
            if (PopupNavigation.Instance.PopupStack.Count > 0)
                await Navigation.PopAllPopupAsync();
            await Navigation.PushPopupAsync(hintPage);
        }

        public void ChangeButtonToViewLocation(object sender, EventArgs e)
        {
            mainButton.Text = "View Location";
            mainButton.BackgroundColor = Color.FromHex("#EC9F2B");
        }

        public void ChangeButtonToSearchForEncounter(object sender, EventArgs e)
        {
            mainButton.Text = "Search for Encounter";
            mainButton.BackgroundColor = Color.FromHex("#6CD4FF");
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
