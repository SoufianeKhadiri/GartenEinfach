using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.SignIn_Up
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}
