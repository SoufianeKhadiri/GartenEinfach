using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.PopUp
{
    public partial class DeletePopup 
    {
        public DeletePopup()
        {
            InitializeComponent();
            BindingContext = new DeletePopupViewModel();
        }
    }
    
}
