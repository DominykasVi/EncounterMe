using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapApp
{
    /// <summary>
    /// Interaction logic for main.xaml
	/// Generated by Export Kit - Lightning Storm.
    /// </summary>
    /// 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();
        }
        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            await Navigation.PopAsync();
        }
    }

}
	