﻿using System;
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
using EncounterMe;

namespace MapApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShrinkSearchCircle : Rg.Plugins.Popup.Pages.PopupPage
    {
        MainPage main;
        double locX;
        double locY;
        double bigX;
        double bigY;

        double locR = 50; //location circle radius
        double newR; //new circle radius
        double bigR; //big circle radius

        public ShrinkSearchCircle(MainPage main, double locY, double locX, double bigY, double bigX)
        {
            this.main = main;
            this.locX = locX;
            this.locY = locY;
            this.bigX = bigX;
            this.bigY = bigY;
            this.bigR = main.searchRadius.Meters;
            InitializeComponent();
            DisableTooBigSizes();
        }

        private void DisableTooBigSizes()
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
        }
        private void ShrinkCircle(object sender, EventArgs e)
        {
            newR = SelectRadius();
            if(newR == 0)
            {
                DisplayAlert("Radius Not Selected", "Please select the radius you want to shrink.", "OK");
                return;
            }

            //Latitude = Y, Longtitude = X
            double circleX = 0, circleY = 0;
            CalculateNewCircleCentre(out circleX, out circleY);

            main.ChangeSearchRadius((float)newR);
            main.ChangeSearchCentre(new Position(circleY, circleX));
            Navigation.PopPopupAsync();
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