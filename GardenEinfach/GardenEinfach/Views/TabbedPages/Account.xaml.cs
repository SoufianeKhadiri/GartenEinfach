using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class Account : ContentPage
    {
        public Account()
        {
            //NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = new AccountViewModel();
        }
    }
}
