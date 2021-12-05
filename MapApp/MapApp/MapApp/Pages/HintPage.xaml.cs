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
using MapApp.Hints;

namespace MapApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HintPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        MainPage main;
        public Location locationToFind;
        GameLogic gameLogic;

        List<IHint> HintList = new List<IHint>();

        private int stackWidth = 0;
        private int bubbleWidth = 16;
        private int currentPosition = 0;



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

            //hintImage.GestureRecognizers.Add(leftSwipe);
            //hintImage.GestureRecognizers.Add(rightSwipe);

            shade.GestureRecognizers.Add(leftSwipe);
            shade.GestureRecognizers.Add(rightSwipe);



        }


        private void UpdateNavigation() 
        {
            //debugText.Text = (stackWidth%bubbleWidth).ToString();

            var unselectedIcons = stackWidth / bubbleWidth;
            stackWidth += bubbleWidth;
            currentPosition += 1;
            UpdateStack();
        }

        private void UpdateStack()
        {
            if (currentPosition > 4)
            {
                return;
            }

            horizontalStack.Children.Clear();
            horizontalStack.WidthRequest = stackWidth;

            if(currentPosition == 0)
            {
                currentPosition++;
            }

            for (int i = 0; i < stackWidth / bubbleWidth; i++)
            {
                if (currentPosition == i + 1)
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

            //debugText.Text = currentPosition.ToString();
        }
        
        private async void CheckMarkOneTapped(object sender, EventArgs e)
        {
            updateCheckMark(new HintCompass(this), checkMarkOne);

        }


        private async void CheckMarkTwoTapped(object sender, EventArgs e)
        {
            //check if it hasnt been clicked before
            updateCheckMark(new HintDistance(this, gameLogic), checkMarkTwo);
            //hintTwoPosition = currentPosition;        
        }

        
     
        

        private async void CheckMarkThreeTapped(object sender, EventArgs e)
        {
            updateCheckMark(new HintCircle(this), checkMarkThree);

        }

        private async void CheckMarkFourTapped(object sender, EventArgs e)
        {
            updateCheckMark(new HintCircle(this), checkMarkFour);
            //TODO: fourth Hint
        }

        private void updateCheckMark(IHint hint, Image image)
        {
            if (image.Source.ToString() != "File: check_mark_checked.png")
            {
                image.Source = "check_mark_checked.png";
                HintList.Insert(currentPosition, hint);
                UpdateNavigation();
                //HintList.Add(hint);
            }
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
            //var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;
            //var myPosition = await locator.GetPositionAsync();

            //LocationUpdateService = DependencyService.Get<ILocationUpdateService>();

            ////main.userPosition = new Position(myPosition.Latitude, myPosition.Longitude);
            //debugText.Text = myPosition.Latitude.ToString();
        }


        void OnSwiped(object sender, SwipedEventArgs e)
        {
            //horizontalStack.Children.Clear();
            if (HintList.Count() < 2)
            {
                return;
            }

            
            //debugText.Text = e.Direction.ToString();
            int newPosition;
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    newPosition = PositionBoundsCheck(1);
                    //checks if we need to update hint
                    if (newPosition != currentPosition)
                    {
                        UpdateHint(newPosition);
                        currentPosition = newPosition;
                        UpdateStack();
                    }
                    break;
                case SwipeDirection.Right:
                    newPosition = PositionBoundsCheck(-1);
                    //checks if we need to update hint
                    if (newPosition != currentPosition)
                    {
                        UpdateHint(newPosition);
                        currentPosition = newPosition;
                        UpdateStack();
                    }
                    break;
                case SwipeDirection.Up:
                    // Handle the swipe
                    break;
                case SwipeDirection.Down:
                    // Handle the swipe
                    break;
            }
            //debugText.Text = currentPosition.ToString();

        }

        void UpdateHint(int newPostion)
        {
            HintList[currentPosition - 1].hideHint(this);
            HintList[newPostion - 1].show(this);
            debugText.Text = currentPosition.ToString() + " " + newPostion.ToString() + " " + HintList[newPostion - 1].ToString();
        }
        int PositionBoundsCheck(int change)
        {

            if (((currentPosition + change) <= stackWidth / bubbleWidth) && (currentPosition + change) > 0)
            {
                var newPosition = currentPosition + change;
                return newPosition;
            }

            return currentPosition;
        }
    }
}