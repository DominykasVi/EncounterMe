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
    public partial class LocationFoundPage : ContentPage
    {
        public LocationFoundPage()
        {
            InitializeComponent();
        }
        async void GoBack(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            await Navigation.PopAsync();
        }
    }
}