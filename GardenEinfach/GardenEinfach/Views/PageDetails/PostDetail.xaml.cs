using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.PageDetails
{
    public partial class PostDetail : ContentPage
    {
        public PostDetail()
        {
            InitializeComponent();
            BindingContext = new PostDetailViewModel();
        }
    }
}
