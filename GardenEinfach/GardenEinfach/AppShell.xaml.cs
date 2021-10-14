using GardenEinfach.Views.PageDetails;
using GardenEinfach.Views.SignIn_Up;
using GardenEinfach.Views.TabbedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GardenEinfach
{

    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PostDetail), typeof(PostDetail));
            Routing.RegisterRoute(nameof(Posts), typeof(Posts));
            Routing.RegisterRoute(nameof(MyPosts), typeof(MyPosts));
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(AddPost), typeof(AddPost));
            Routing.RegisterRoute(nameof(Messages), typeof(Messages));
            Routing.RegisterRoute(nameof(Search), typeof(Search));
            Routing.RegisterRoute(nameof(Account), typeof(Account));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(Register), typeof(Register));



        }

        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    if (!string.IsNullOrEmpty(Preferences.Get("myFirebaseRefreshTokenn", "")))
        //    {

        //    }
        //    else
        //    {
        //        await Shell.Current.GoToAsync("//Login");
        //    }
        //}
    }
}