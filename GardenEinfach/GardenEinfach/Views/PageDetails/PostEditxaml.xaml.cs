using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.PageDetails
{
    public partial class PostEditxaml : ContentPage
    {
        public PostEditxaml()
        {
            InitializeComponent();
            BindingContext = new PostEditxamlViewModel();
        }
    }
}
