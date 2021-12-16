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

namespace MapApp.Notification
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        //MainPage main;
        //HintPage hintPage;

        public NotificationPage()
        {
            //this.main = main;
            //this.hintPage = hintPage;

            /*
            this.locX = locX;
            this.locY = locY;
            this.bigX = main.searchCircleCentre.Longitude;
            this.bigY = main.searchCircleCentre.Latitude;
            this.bigR = main.searchRadius.Meters;*/
            InitializeComponent();
            //DisableTooBigSizes();
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
            //await Navigation.PushPopupAsync(hintPage);
        }
    }
}