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
            gridLayout.RowDefinitions.Add(new RowDefinition());

            List<RadioButton> radioButtons = new List<RadioButton>();

            int i = -1;
            int j = 0;
            foreach (var attribute in attributeList)
            {
                var newRadioButton = new RadioButton()
                {
                    Content = attribute.Name,
                    GroupName = "buttons",
                    FontSize = 15,
                    BackgroundColor = Color.Default
                };
                radioButtons.Add(newRadioButton);

                StackLayout stackLayout = new StackLayout
                {
                    Children =
                    {
                        newRadioButton,
                        new Image {Source = attribute.Image }
                    }
                };

                i++;
                if (i == 3)
                {
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