using GardenEinfach.Views;
using GardenEinfach.Views.SignIn_Up;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GardenEinfach
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Authentication();

        }

        public async void Authentication()
        {
            string FirebaseToken = Preferences.Get("myFirebaseRefreshToken", "");

            if (string.IsNullOrEmpty(FirebaseToken))
            {
                Preferences.Set("Email", "");
                await Shell.Current.GoToAsync("//Login");
            }
            else
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
