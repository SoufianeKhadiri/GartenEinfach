using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.PopUp
{
    public partial class AddPostPopup
    {
        public AddPostPopup()
        {
            InitializeComponent();
            BindingContext = new AddPostPopupViewModel();
        }
    }
}
