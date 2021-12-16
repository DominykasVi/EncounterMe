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
        int exp = 5000;
        public SearchEncounter(MainPage main)
        {
            this.main = main;
            InitializeComponent();
            RadiusSlider.Value = 5000; //set to max from the start
        }
        public void Update()
        {
            main.ChangeSearchRadius((float)RadiusSlider.Value);
        }
        private async void GoBack(object sender, EventArgs e)
        {
            //main.MoveMap();
            main.ChangeSearchRadius(0); //change if doesn't make sense
            await Navigation.PopPopupAsync();
        }
        private void SliderValueChanged(Object sender, ValueChangedEventArgs e)
        {
            main.ChangeSearchRadius((float)(sender as Slider).Value);
            //calculate experience
            exp = (int)RadiusSlider.Value;
            if (exp >= 1000)
            {
                exp = (exp / 1000) * 1000;
                Experience.TextColor = Color.FromHex("#5bd963");
            }
            else if (exp < 1000 && exp >= 200)
            {
                exp = (exp / 100) * 100;
                Experience.TextColor = Color.FromHex("#f29f30");
            }
            else
            {
                exp = 100;
                Experience.TextColor = Color.FromHex("#f03838");
            }
            Experience.Text = exp.ToString() + " EXP";
        }
        public async void ShowLocation(Location location)
        {
            await Navigation.PopPopupAsync();
            await Navigation.PushPopupAsync(new LocationPopup(main, location));
        }
        private void Search(Object sender, EventArgs e)
        {
            main.ChangeButtonToViewLocation(sender, e);
            main.SearchForPlace(exp);
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