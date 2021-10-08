using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.PageDetails
{
    public partial class Posts : ContentPage
    {
        public Posts()
        {
            InitializeComponent();
            BindingContext = new PostsViewModel();
        }
    }
}
