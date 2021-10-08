using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class AddPost : ContentPage
    {
        public AddPost()
        {
            //NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = new AddPostViewModel();
        }
    }
}
