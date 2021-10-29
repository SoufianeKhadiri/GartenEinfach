using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.PageDetails
{
    public partial class PostChat : ContentPage
    {
        public PostChat()
        {
            InitializeComponent();
            BindingContext = new PostChatViewModel();
        }
    }
}
