using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        MainPage main;
        public CategoryPage(MainPage main)
        {
            InitializeComponent();
            CreateGrid(main.attributes);
        }

        private void CreateGrid(List<EncounterMe.Classes.Attribute> attributeList)
        {
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());

            List<CheckBox> checkBoxes = new List<CheckBox>();

            int i = -1;
            int j = 0;
            foreach (var attribute in attributeList)
            {
                //create new checkbox
                var newCheckBox = new CheckBox()
                {
                    ClassId = attribute.Name,
                    BackgroundColor = Color.Default
                };
                checkBoxes.Add(newCheckBox);

                //grid for checkbox and label to be side by side
                Grid newGrid = new Grid();
                newGrid.RowDefinitions.Add(new RowDefinition());
                newGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = 30});
                newGrid.ColumnDefinitions.Add(new ColumnDefinition());
                newGrid.Children.Add(newCheckBox, 0, 0);
                newGrid.Children.Add(new Label { Text = attribute.Name, HorizontalOptions = LayoutOptions.Start }, 1, 0);

                //add grid and Image in stacklayout
                StackLayout stackLayout = new StackLayout
                {
                    Children =
                    {
                        newGrid,
                        new Image {Source = attribute.Image }
                    }
                };

                //assign to proper place in whole grid
                i++;
                if (i == 3)
                {
                    gridLayout.RowDefinitions.Add(new RowDefinition());
                    i = 0;
                    j++;
                }
                gridLayout.Children.Add(stackLayout, i, j);
                //gridLayout.Children.Add(newRadioButton, i, j);
            }

        }
        private async void GoBack(object sender, EventArgs e)
        {
            //main.PopupSearchEncounter(sender, e);
            //await Navigation.PushPopupAsync(main.searchEncounterPage);
            //doesnt work with calling SearchEncounter dont know why
            await Navigation.PopAsync();
        }

    }

}