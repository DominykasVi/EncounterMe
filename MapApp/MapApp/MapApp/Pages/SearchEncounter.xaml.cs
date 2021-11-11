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
    public partial class SearchEncounter : Rg.Plugins.Popup.Pages.PopupPage
    {
        MainPage main;
        public SearchEncounter(MainPage main)
        {
            this.main = main;
            InitializeComponent();
        }
        private async void GoBack(object sender, EventArgs e)
        {
            main.MoveMap();
            await Navigation.PopPopupAsync();
        }
        private void SliderValueChanged(Object sender, ValueChangedEventArgs e)
        {
            main.SliderValueChanged(sender as Slider);
            SliderValue.Text = "Selected radius is: " + RadiusSlider.Value.ToString() + " m.";
        }
        public async void ShowLocation(Location location)
        {
            await Navigation.PopPopupAsync();
            await Navigation.PushPopupAsync(new Pages.LocationPopup(main, location));
        }
        private void Search(Object sender, EventArgs e)
        {
            main.SearchForPlace();
        }
    }
}