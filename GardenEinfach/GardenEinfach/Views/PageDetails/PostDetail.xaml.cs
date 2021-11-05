using GardenEinfach.ViewModels;
using Prism.Events;
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
