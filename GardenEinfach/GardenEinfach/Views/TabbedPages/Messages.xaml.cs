using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class Messages : ContentPage
    {
        public Messages()
        {
            //NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = new MessagesViewModel();
        }
    }
}
