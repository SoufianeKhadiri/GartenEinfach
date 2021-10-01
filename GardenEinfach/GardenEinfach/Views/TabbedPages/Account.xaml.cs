using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class Account : ContentPage
    {
        public Account()
        {
            InitializeComponent();
            BindingContext = new AccountViewModel();
        }
    }
}
