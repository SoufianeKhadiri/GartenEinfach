using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.PageDetails
{
    public partial class MyPosts : ContentPage
    {
        public MyPosts()
        {
            InitializeComponent();
            BindingContext = new MyPostsViewModel();
        }
    }
}
