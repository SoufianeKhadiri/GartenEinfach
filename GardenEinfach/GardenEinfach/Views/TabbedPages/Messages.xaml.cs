using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.TabbedPages
{
    public partial class Messages : ContentPage
    {
        public Messages()
        {
            InitializeComponent();
            BindingContext = new MessagesViewModel();
        }
    }
}
