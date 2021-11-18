using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMe;

namespace MapApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage(User user)
        {
            InitializeComponent();
            username.Text = user.Name;
            
            int levelNum;
            float perc;
            user.CalculateLevel(out levelNum, out perc);
            level.Text = "level " + levelNum.ToString();
            levelMeter.Progress = perc;

            locationsFound.Text = user.FoundLocationNum.ToString();
            achievements.Text = user.AchievementNum.ToString();
        }
        async void GoBack(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            await Navigation.PopAsync();
        }
    }

}
	