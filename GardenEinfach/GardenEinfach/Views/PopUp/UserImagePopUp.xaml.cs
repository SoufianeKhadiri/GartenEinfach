using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.PopUp
{
    public partial class UserImagePopUp
    {
        public UserImagePopUp()
        {
            InitializeComponent();
            BindingContext = new UserImagePopUpViewModel();
        }
    }
}
