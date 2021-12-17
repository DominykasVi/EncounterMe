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
    public partial class NotificationPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        HintPage hintPage;
        int minusExp;
        String header;
        String text;
        String image;
        ShrinkSearchCircle shrink;

        public NotificationPage(HintPage hintPage, int minusExp, String header, String text, String image, ShrinkSearchCircle shrink = null)
        {
            this.hintPage = hintPage;
            this.minusExp = minusExp;
            this.header = header;
            this.text = text;
            this.image = image;
            this.shrink = shrink;

            InitializeComponent();
            UpdateInterface();
        }

        private void UpdateInterface()
        {
            Image.Source = image;
            Header.Text = header;
            Text.Text = text;
            Experience.Text = "-" + minusExp.ToString() + " EXP";
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private async void Selected(object sender, EventArgs e)
        {
            //dumb as heck but i dont care
            if (header == "Compass Hint")
                hintPage.ActivateCompass(minusExp);
            else if (header == "Distance Hint")
                hintPage.ActivateDistance(minusExp);
            else if (header == "Shrink Hint")
                shrink.ActivateShrink(minusExp);
            await Navigation.PopPopupAsync();
        }
    }
}