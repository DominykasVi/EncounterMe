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
            main.ChangeButtonToViewLocation(sender, e);
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

            CreateGrid(10, pickedAttributes);
        }

        private void CreateGrid(int columnNum, List<EncounterMe.Classes.Attribute> attributeList)
        {
            categoryGrid.RowDefinitions.Add(new RowDefinition() { Height = 30 });
            for (int i = 0; i < columnNum; i++)
                categoryGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            FillGridWithElements(categoryGrid, columnNum, attributeList);
        }

        private void FillGridWithElements(Grid grid, int columnNum, List<EncounterMe.Classes.Attribute> attributeList)
        {
            int column = -1;
            int row = 0;
            foreach (var attribute in attributeList)
            {
                //assign to proper place in whole grid
                column++;
                if (column == columnNum)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    column = 0;
                    row++;
                }
                grid.Children.Add(new Image { Source = attribute.Image }, column, row);
            }
        }
    }
}