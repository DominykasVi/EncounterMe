using EncounterMe;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HintPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private int stackWidth = 0;
        private int currentPostion = 0;
        public HintPage()
        {
            InitializeComponent();
            var leftSwipe = new SwipeGestureRecognizer { Direction = SwipeDirection.Left};
            var rightSwipe = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };

            leftSwipe.Swiped += OnSwiped;
            rightSwipe.Swiped += OnSwiped;

            hintImage.GestureRecognizers.Add(leftSwipe);
            hintImage.GestureRecognizers.Add(rightSwipe);
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
            horizontalStack.Children.Clear();
            horizontalStack.WidthRequest = stackWidth;

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

        }

        private async void CheckMarkThreeTapped(object sender, EventArgs e)
        {
            checkMarkThree.Source = "check_mark_checked.png";
            UpdateNavigation();

        }

        private async void CheckMarkFourTapped(object sender, EventArgs e)
        {
            checkMarkFour.Source = "custom_checkmark.png";
            UpdateNavigation();

        }
        private async void GoBack(object sender, EventArgs e)
        {
            //main.MoveMap();
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
        private void Search(Object sender, EventArgs e)
        {
            //main.SearchForPlace();
        }
        private async void Category(Object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CategoryPage(main));
            //await Navigation.PopPopupAsync();
        }

        void OnSwiped(object sender, SwipedEventArgs e)
        {
            //horizontalStack.Children.Clear();
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

                    if ((currentPostion - 1) >= 0)
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