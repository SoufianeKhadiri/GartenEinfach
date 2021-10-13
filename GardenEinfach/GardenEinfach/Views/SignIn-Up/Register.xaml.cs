using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.SignIn_Up
{
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel();
        }
    }
}
