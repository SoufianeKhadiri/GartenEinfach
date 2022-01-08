using GardenEinfach.Model;
using GardenEinfach.Views;
using GardenEinfach.Views.SignIn_Up;
using System;
using System.IO;
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

        static SQLiteHelper db;

        public static SQLiteHelper SQLiteDb
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data.sqlite"));
                }
                return db;
            }
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
