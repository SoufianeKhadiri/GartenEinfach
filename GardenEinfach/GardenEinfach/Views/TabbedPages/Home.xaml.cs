using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class Home : ContentPage
    {
        HomeViewModel homeViewModel;
        public Home()
        {
            //NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = homeViewModel = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            homeViewModel.OnAppearing();
        }
    }
}
