using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class AddPost : ContentPage
    {
        public AddPost()
        {
            InitializeComponent();
            BindingContext = new AddPostViewModel();
        }
    }
}
