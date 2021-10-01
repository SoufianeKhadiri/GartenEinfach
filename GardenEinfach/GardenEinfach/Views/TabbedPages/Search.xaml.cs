using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class Search : ContentPage
    {
        public Search()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel();
        }
    }
}
