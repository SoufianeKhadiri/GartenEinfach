using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class Home : ContentPage
    {
        
        public Home()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation);
        }
    }
}
