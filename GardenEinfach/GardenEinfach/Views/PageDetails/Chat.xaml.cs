using GardenEinfach.ViewModels;
using Prism.Events;
using Xamarin.Forms;

namespace GardenEinfach.Views.PageDetails
{
    public partial class Chat : ContentPage
    {

        public Chat()
        {
            InitializeComponent();

            BindingContext = new ChatViewModel();
        }
    }
}
