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
            await Navigation.PushPopupAsync(new LocationPopup(main, location));
        }
        private void Search(Object sender, EventArgs e)
        {
            main.SearchForPlace();
        }
        private async void Category(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategoryPage(main));
            await Navigation.PopPopupAsync();
        }
        public void GeneratePickedAttributes()
        {
            categoryGrid.Children.Clear();
            categoryGrid.RowDefinitions.Clear();
            categoryGrid.ColumnDefinitions.Clear();

            List<EncounterMe.Classes.Attribute> pickedAttributes = main.pickedAttributes;
            if (pickedAttributes.Count == 0)
                return;

            categoryGrid.RowDefinitions.Add(new RowDefinition() { Height = 30 });
            for(int l = 0; l < 10; l++) 
                categoryGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });

            int i = -1;
            int j = 0;
            foreach (var attribute in pickedAttributes)
            {
                //assign to proper place in whole grid
                i++;
                if (i == 10)
                {
                    categoryGrid.RowDefinitions.Add(new RowDefinition());
                    i = 0;
                    j++;
                }
                categoryGrid.Children.Add(new Image { Source = attribute.Image }, i, j);
            }
        }
    }
}