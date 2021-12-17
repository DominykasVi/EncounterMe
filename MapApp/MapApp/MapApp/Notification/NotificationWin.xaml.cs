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
using MapApp.Hints;
using EncounterMe;

namespace MapApp.Notification
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationWin : Rg.Plugins.Popup.Pages.PopupPage
    {

        public NotificationWin(int exp)
        {
            
            InitializeComponent();
            Experience.Text = exp.ToString() + " EXP";
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}