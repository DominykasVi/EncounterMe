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
            //mainpage.searchRadius = new Distance(RadiusSlider.Value);
            //mainpage.userSearchCircle.Radius = mainpage.searchRadius;
        }
    }
}