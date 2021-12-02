using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;

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
        public ShrinkSearchCircle(MainPage main)
        {
            this.main = main;
            InitializeComponent();
            DisableTooBigSizes();
        }

        private void DisableTooBigSizes()
        {
            if (main.searchRadius.Meters < 2500)
                size2500.IsEnabled = false;
            if (main.searchRadius.Meters < 1000)
                size2500.IsEnabled = false;
            if (main.searchRadius.Meters < 500)
                size2500.IsEnabled = false;
            if (main.searchRadius.Meters < 50)
                size2500.IsEnabled = false;
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}