using EncounterMe.Functions;
using MapApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.DetectUserLocationCange.Services;


namespace MapApp.Hints
{
    internal class HintDistance : IHint
    {
        ILocationUpdateService LocationUpdateService;
        private double startingDistance = -1;
        private double lastDistance;

        private double opacity;
        private double startingOpacity = 0.7;

        bool isActive = false;

        float distance;

        public HintDistance(HintPage hintPage, GameLogic gameLogic)
        {

            isActive = true;

            LocationUpdateService = DependencyService.Get<ILocationUpdateService>();
            LocationUpdateService.LocationChanged += (object sender, ILocationEventArgs args) =>
            {
                UpdateLocation(sender, args, gameLogic, hintPage);
            };

            LocationUpdateService.GetUserLocation();
            //hintPage.hintTwoPosition = currentPostion;
            hintPage.hintImage.IsVisible = false;
            hintPage.animationView.IsVisible = true;
            //hintPage.shade.IsVisible = true;
        }

        void UpdateLocation(object sender, ILocationEventArgs args, GameLogic gameLogic, HintPage hintPage)
        {
            distance = gameLogic.distanceBetweenPoints((float)args.Latitude, (float)args.Longitude, hintPage.locationToFind.Latitude, hintPage.locationToFind.Longtitude);
            if (isActive)
            {
                UpdateDistanceVisual(distance, hintPage);
                //hintPage.debugText.Text = distance.ToString();
            }
        }

        public void hideHint(HintPage hintPage)
        {
            //hintPage.debugText.Text = "";
            hintPage.animationView.IsVisible = false;
            hintPage.shade.Opacity = 0;
            isActive = false;
        }

        public void show(HintPage hintPage)
        {
            //hintPage.debugText.Text = distance.ToString();
            hintPage.animationView.IsVisible = true;
            hintPage.shade.Opacity = opacity;
            isActive = true;

        }
        private int baseHeight = 100;
        private int baseWidth = 100;

        private int maxHeightAddition = 150;
        private int maxWidthAddition = 150;


        private void UpdateDistanceVisual(double distance, HintPage hintPage)
        {
            if (startingDistance == -1)
            {
                startingDistance = distance;
                lastDistance = distance;
                opacity = startingOpacity;

                hintPage.animationView.HeightRequest = baseHeight;
                hintPage.animationView.WidthRequest = baseWidth;

                hintPage.shade.Opacity = opacity;
                return;
            }
            //TODO fix opasito so never 0
            var differencePercentage = (startingDistance - distance) / startingDistance;
            opacity = startingOpacity - (startingOpacity * differencePercentage);
            hintPage.shade.Opacity = opacity;

            hintPage.animationView.HeightRequest = baseHeight + (maxHeightAddition * differencePercentage);
            hintPage.animationView.WidthRequest = baseWidth + (maxWidthAddition * differencePercentage);

        }
    }
}
