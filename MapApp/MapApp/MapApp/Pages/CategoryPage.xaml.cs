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
        List<EncounterMe.Classes.Attribute> pickedAttributes;
        List<EncounterMe.Classes.Attribute> attributes;

        public CategoryPage(MainPage main)
        {
            InitializeComponent();
            this.main = main;
            pickedAttributes = main.pickedAttributes;
            attributes = main.attributes;
            CreateGrid(3, attributes);
        }

        private void CreateGrid(int columnNum, List<EncounterMe.Classes.Attribute> attributeList)
        {
            gridLayout.RowDefinitions.Add(new RowDefinition());
            for(int i = 0; i<columnNum ; i++)
                gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            FillGridWithElements(gridLayout, columnNum, attributeList);
        }

        private void FillGridWithElements(Grid grid, int columnNum, List<EncounterMe.Classes.Attribute> attributeList)
        {
            int column = -1;
            int row = 0;
            foreach (var attribute in attributeList)
            {
                //add grid and Image in stacklayout
                StackLayout stackLayout = new StackLayout
                {
                    Children =
                    {
                        CreateGridForAttribute(attribute),
                        new Image {Source = attribute.Image }
                    }
                };

                //assign to proper place in whole grid
                column++;
                if (column == columnNum)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    column = 0;
                    row++;
                }
                grid.Children.Add(stackLayout, column, row);
            }
        }

        private Grid CreateGridForAttribute(EncounterMe.Classes.Attribute attribute)
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());
            newGrid.Children.Add(CreateCheckBoxFromAttribute(attribute), 0, 0);
            newGrid.Children.Add(CreateLabelFromAttribute(attribute), 1, 0);
            return newGrid;
        }

        private Label CreateLabelFromAttribute(EncounterMe.Classes.Attribute attribute)
        {
            return new Label { Text = attribute.Name, HorizontalOptions = LayoutOptions.Start };
        }

        private CheckBox CreateCheckBoxFromAttribute(EncounterMe.Classes.Attribute attribute)
        {
            CheckBox newCheckBox = new CheckBox()
            {
                ClassId = attribute.Name,
                BackgroundColor = Color.Default,
                IsChecked = pickedAttributes.Contains(attribute),
            };
            newCheckBox.CheckedChanged += CheckedAttribute;

            return newCheckBox;
        }

        private async void GoBack(object sender, EventArgs e)
        {
            main.pickedAttributes = pickedAttributes;
            await Navigation.PopAsync();
            main.PopupSearchEncounter(sender, e);
        }

        private void CheckedAttribute(object sender, EventArgs e)
        {
            String attributeName = (sender as CheckBox).ClassId;
            var attribute = pickedAttributes.Find(e => e.Name == attributeName);
            if (attribute == null)
                pickedAttributes.Add(attributes.Find(e => e.Name == attributeName));
            else
                pickedAttributes.Remove(attribute);
        } 
    } 
}