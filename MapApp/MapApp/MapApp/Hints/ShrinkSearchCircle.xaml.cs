using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using MapApp.Pages;
using EncounterMe;
using MapApp.Notification;

namespace MapApp.Hints
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShrinkSearchCircle : Rg.Plugins.Popup.Pages.PopupPage
    {
        MainPage main;
        HintPage hintPage;
        double locX;
        double locY;
        double bigX;
        double bigY;

        double locR = 50; //location circle radius
        double newR; //new circle radius
        double bigR; //big circle radius

        public ShrinkSearchCircle(MainPage main, HintPage hintPage, double locY, double locX)
        {
            this.main = main;
            this.hintPage = hintPage;        

            this.locX = locX;
            this.locY = locY;
            this.bigX = main.searchCircleCentre.Longitude;
            this.bigY = main.searchCircleCentre.Latitude;
            this.bigR = main.searchRadius.Meters;
            InitializeComponent();
            DisableTooBigSizes();
        }

        public void Update()
        {
            //update search radius
            bigR = main.searchRadius.Meters;
            //update display
            DisableTooBigSizes();
            //update search center
            bigX = main.searchCircleCentre.Longitude;
            bigY = main.searchCircleCentre.Latitude;
        }

        public void DisableTooBigSizes()
        {
            if (bigR <= 2500)
                size2500.IsEnabled = false;
            if (bigR <= 1000)
                size1000.IsEnabled = false;
            if (bigR <= 500)
                size500.IsEnabled = false;
            if (bigR <= 50)
                size50.IsEnabled = false;
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
            await Navigation.PushPopupAsync(hintPage);
        }
        private async void ShrinkCircle(object sender, EventArgs e)
        {
            newR = SelectRadius();
            if(newR == 0)
            {
                DisplayAlert("Radius Not Selected", "Please select the radius you want to shrink.", "OK");
                return;
            }

            int minusPerc = 0;
            if (newR == 2500)
                minusPerc = 20;
            else if (newR == 1000)
                minusPerc = 40;
            else if (newR == 500)
                minusPerc = 60;
            else if (newR == 50)
                minusPerc = 90;

            await Navigation.PushPopupAsync(
                   new Notification.NotificationPage(
                       hintPage,
                       hintPage.CalculateExp(minusPerc),
                       "Shrink Hint",
                       "Shrink hint will decrease the radius of the area where the location might be.",
                       "hamcircle1.png",
                       this));
        }

        public async void ActivateShrink(int minusExp)
        {
            //Latitude = Y, Longtitude = X
            double circleX = 0, circleY = 0;
            CalculateNewCircleCentre(out circleX, out circleY);

            main.ChangeSearchRadius((float)newR);
            main.ChangeSearchCentre(new Position(circleY, circleX));
            Update();

            hintPage.exp = hintPage.exp - minusExp;
            hintPage.UpdateExperienceButton();
            await Navigation.PopPopupAsync(); //doesn't work, don't know why
        }

        private int SelectRadius()
        {
            if (size2500.IsChecked)
                return 2500;
            else if (size1000.IsChecked)
                return 1000;
            else if (size500.IsChecked)
                return 500;
            else if (size50.IsChecked)
                return 50;
            else
                return 0;
        }

        private void CalculateNewCircleCentre(out double circleX, out double circleY)
        {
            circleX = 0;
            circleY = 0;

            //moving x and y from centre
            double lenx;
            double leny;

            //new circle coordinates
            double newx;
            double newy;          

            Boolean correct = false;

            while (correct == false)
            {
                correct = true;

                if (newR - locR < bigR - newR)
                {
                    lenx = new Random().Next(0, Convert.ToInt32(newR - locR));
                    leny = new Random().Next(0, Convert.ToInt32(newR - locR));


                    if (Math.Pow(Math.Pow(lenx, 2) + Math.Pow(leny, 2), 0.5) <= newR - locR)
                    {
                        newy = locY + ConvertYMetersToDegrees(leny);
                        newx = locX + ConvertXMetersToDegrees(lenx, newy);

                        Distance distance = Distance.BetweenPositions(new Position(newy, newx), new Position(bigY, bigX));
                        if (distance.Meters <= bigR - newR)
                        {
                            circleX = newx;
                            circleY = newy;
                        }
                        else
                            correct = false;
                    }
                    else
                        correct = false;
                }
                else
                {
                    lenx = new Random().Next(0, Convert.ToInt32(bigR - newR));
                    leny = new Random().Next(0, Convert.ToInt32(bigR - newR));

                    if (Math.Pow(Math.Pow(lenx, 2) + Math.Pow(leny, 2), 0.5) <= bigR - newR)
                    {
                        newy = bigY + ConvertYMetersToDegrees(leny);
                        newx = bigX + ConvertXMetersToDegrees(lenx, newy);

                        Distance distance = Distance.BetweenPositions(new Position(newy, newx), new Position(locY, locX));
                        if (distance.Meters <= newR - locR)
                        {
                            circleX = newx;
                            circleY = newy;
                        }
                        else
                            correct = false;
                    }
                    else
                        correct = false;
                }
            }
        }
        

        private double ConvertYMetersToDegrees(double value)
        {
            const double R = 6378137; //earth radius in meters
            double rad = value / R; //get radians from value
            double result = rad / Math.PI *180;//result in degrees

            return result;
        }

        private double ConvertXMetersToDegrees(double value, double latitude)
        {
            double R = 6378137*Math.Cos(latitude/180*Math.PI); //earth radius in meters and changed by latitude
            double rad = value / R; //get radians from degrees
            double result = rad/Math.PI*180;//result in meters

            return result;
        }
    }
}