using MapApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using MapApp;
using Xamarin.Forms.DetectUserLocationCange.Services;
using EncounterMe.Functions;

namespace MapApp.Hints
{
    internal class HintCompass : MvvmHelpers.BaseViewModel, IHint
    {
        bool isActive = false;
        ILocationUpdateService LocationUpdateService;
        double Bearing = 0;
        HintPage HintPage;
        public HintCompass(HintPage hintPage, GameLogic gameLogic)
        {
            if (!DesignMode.IsDesignModeEnabled)
                Start();
            HintPage = hintPage;
            isActive = true;
            LocationUpdateService = DependencyService.Get<ILocationUpdateService>();
            LocationUpdateService.LocationChanged += (object sender, ILocationEventArgs args) =>
            {
                updateBearing(sender, args, gameLogic);
            };

            LocationUpdateService.GetUserLocation();
            hintPage.hintImage.IsVisible = true;
            hintPage.hintImage.Source = "arrow.png";
        }

        void updateBearing (object sender, ILocationEventArgs args, GameLogic gameLogic)
        {
            Bearing = gameLogic.getBearingFromUser(HintPage.locationToFind.Latitude, HintPage.locationToFind.Longtitude, args.Latitude, args.Longitude);
            if (isActive)
                HintPage.hintImage.Rotation = Bearing;
        }

        void Stop()
        {
            if (!Compass.IsMonitoring)
                return;

            Compass.ReadingChanged -= Compass_ReadingChanged;
            Compass.Stop();
        }

        void Start()
        {
            if (Compass.IsMonitoring)
                return;


            Compass.ReadingChanged += Compass_ReadingChanged;
            Compass.Start(SensorSpeed.UI, true);

        }

        void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            if (isActive)
            {
                var z = e.Reading.HeadingMagneticNorth; // zmogaus krypties azimutas
                double imgRot;
                if (z > Bearing)
                {
                    imgRot = (360 - z + Bearing) % 360;
                }
                else imgRot = Bearing - z;
                HintPage.hintImage.Rotation = imgRot;
            }
        }

        public void hideHint(HintPage hintPage)
        {
            if (!DesignMode.IsDesignModeEnabled)
                Stop();
            isActive = false;
            //throw new NotImplementedException();
            hintPage.hintImage.IsVisible = false;
            hintPage.hintImage.Rotation = 0;

        }

        public void show(HintPage hintPage)
        {
            if (!DesignMode.IsDesignModeEnabled)
                Start();
            isActive = true;
            hintPage.hintImage.IsVisible = true;
            hintPage.hintImage.Source = "arrow.png";
            hintPage.hintImage.Rotation = Bearing;
        }


    }
}
