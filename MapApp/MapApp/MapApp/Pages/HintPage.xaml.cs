using EncounterMe;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncounterMe.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using System.Threading;
using Xamarin.Forms.DetectUserLocationCange.Services;

namespace MapApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HintPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        //16 as variable
        MainPage main;
        Location locationToFind;
        GameLogic gameLogic;

        private int stackWidth = 0;
        private int currentPostion = 0;

        private int imagePosition = 1;
        private int hintTwoPosition;

        ILocationUpdateService LocationUpdateService;

        public HintPage(MainPage main, Location loc)
        {
            this.main = main;
            this.locationToFind = loc;
            gameLogic = new GameLogic();
            InitializeComponent();
            var leftSwipe = new SwipeGestureRecognizer { Direction = SwipeDirection.Left};
            var rightSwipe = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };

            leftSwipe.Swiped += OnSwiped;
            rightSwipe.Swiped += OnSwiped;

            hintImage.GestureRecognizers.Add(leftSwipe);
            hintImage.GestureRecognizers.Add(rightSwipe);

            shade.GestureRecognizers.Add(leftSwipe);
            shade.GestureRecognizers.Add(rightSwipe);



        }

        private async void CheckMarkOneUntapped(object sender, EventArgs e)
        {
            checkMarkOneTapped.IsVisible = false;
            checkMarkOneUntapped.IsVisible = true;
            //main.MoveMap();
            //await Navigation.PopPopupAsync();
        }

        private void UpdateNavigation() 
        {
            //debugText.Text = (stackWidth%16).ToString();

            var unselectedIcons = stackWidth / 16;
            stackWidth += 16;
            currentPostion += 1;
            UpdateStack();
        }

        private void UpdateStack()
        {
            if (currentPostion > 4)
            {
                return;
            }

            horizontalStack.Children.Clear();
            horizontalStack.WidthRequest = stackWidth;

            if(currentPostion == 0)
            {
                currentPostion++;
            }

            for (int i = 0; i < stackWidth / 16; i++)
            {
                if (currentPostion == i + 1)
                {
                    horizontalStack.Children.Add(new Image
                    {
                        Source = "navigation_selected.png",
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Aspect = Aspect.Fill,
                        IsVisible = true,
                    });
                }
                else
                {
                    Debug.WriteLine(i);
                    horizontalStack.Children.Add(new Image
                    {
                        Source = "navigation_unselected.png",
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Aspect = Aspect.Fill,
                        IsVisible = true,
                    });
                }
            }

            debugText.Text = currentPostion.ToString();
        }
        
        private async void CheckMarkOneTapped(object sender, EventArgs e)
        {
            checkMarkOneTapped.IsVisible = true;
            checkMarkOneUntapped.IsVisible = false;
            UpdateNavigation();
            //Debug.WriteLine(horizontalStack.);
            //foreach (var child in horizontalStack)
            //{

            //};
            //horizontalStack.C


        }


        private async void CheckMarkTwoTapped(object sender, EventArgs e)
        {
            checkMarkTwo.Source = "check_mark_checked.png";
            UpdateNavigation();
            LocationUpdateService = DependencyService.Get<ILocationUpdateService>();
            LocationUpdateService.LocationChanged += (object sender, ILocationEventArgs args) =>
            {
                var distance = gameLogic.distanceBetweenPoints((float)args.Latitude, (float)args.Longitude, locationToFind.Latitude, locationToFind.Longtitude);
                UpdateDistanceVisual(distance);
                debugText.Text = distance.ToString();
            };

            LocationUpdateService.GetUserLocation();
            hintTwoPosition = currentPostion;
            hintImage.IsVisible = false;
            animationView.IsVisible = true;
            shade.IsVisible = true;

        }

        private double startingDistance = -1;
        private double lastDistance;

        private double opacity;
        private double startingOpacity = 0.7;
     
        private void UpdateDistanceVisual(double distance)
        {
            if(startingDistance == -1)
            {
                startingDistance = distance;
                lastDistance = distance;
                opacity = startingOpacity;

                animationView.HeightRequest = 100;
                animationView.WidthRequest = 100;

                shade.Opacity = opacity;
                return;
            }

            var differencePercentage = (startingDistance - distance) / startingDistance;
            opacity = startingOpacity - (startingOpacity * differencePercentage);
            shade.Opacity = opacity;

            animationView.HeightRequest = 100 + (150*differencePercentage);
            animationView.WidthRequest = 100 + (150 * differencePercentage);
    
        }

        private async void CheckMarkThreeTapped(object sender, EventArgs e)
        {
            checkMarkThree.Source = "check_mark_checked.png";
            UpdateNavigation();

        }

        private async void CheckMarkFourTapped(object sender, EventArgs e)
        {
            checkMarkFour.Source = "check_mark_checked.png";
            UpdateNavigation();

        }
        private async void GoBack(object sender, EventArgs e)
        {
            //function to close temporary HintPage
            await Navigation.PopPopupAsync();
        }
        private async void GiveUp(object sender, EventArgs e)
        {
            //function to go back to location search
            main.ChangeButtonToSearchForEncounter(sender, e);
            await Navigation.PopPopupAsync();
        }
        private void SliderValueChanged(Object sender, ValueChangedEventArgs e)
        {
            //main.SliderValueChanged(sender as Slider);
            //SliderValue.Text = "Selected radius is: " + RadiusSlider.Value.ToString() + " m.";
        }
        public async void ShowLocation(Location location)
        {
            //await Navigation.PopPopupAsync();
            //await Navigation.PushPopupAsync(new LocationPopup(main, location));
        }
        private async void Test(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var myPosition = await locator.GetPositionAsync();

            LocationUpdateService = DependencyService.Get<ILocationUpdateService>();

            //main.userPosition = new Position(myPosition.Latitude, myPosition.Longitude);
            debugText.Text = myPosition.Latitude.ToString();
        }

 

        void OnSwiped(object sender, SwipedEventArgs e)
        {
            //horizontalStack.Children.Clear();
            if (currentPostion == 0)
            {
                return;
            }
            hintImage.Source = "temp.jpg";
            //debugText.Text = e.Direction.ToString();

            switch (e.Direction)
            {
                case SwipeDirection.Left:

                    if ((currentPostion + 1) <= stackWidth/16)
                    {
                        currentPostion += 1;
                    }
                    UpdateStack();
                    break;
                case SwipeDirection.Right:

                    if ((currentPostion - 1) > 0)
                    {
                        currentPostion -= 1;
                    }
                    UpdateStack();
                    break;
                case SwipeDirection.Up:
                    // Handle the swipe
                    break;
                case SwipeDirection.Down:
                    // Handle the swipe
                    break;
            }
        }
    }
}