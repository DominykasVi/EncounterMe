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
    public partial class LocationPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        MainPage main;
        Location location;
        public LocationPopup(MainPage main, Location location)
        {
            this.location = location;
            this.main = main;
            InitializeComponent();

            LocationName.Text = location.Name;
        }
        private async void GoBack(object sender, EventArgs e)
        {
            main.MoveMap();
            await Navigation.PopPopupAsync();
        }
    }
}