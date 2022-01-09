using GardenEinfach.ViewModels;
using Prism.Events;
using Xamarin.Forms;

namespace GardenEinfach.Views.PopUp
{
    public partial class EditPopup
    {
        IEventAggregator eventAggregator;
        public EditPopup()
        {
            InitializeComponent();
            BindingContext = new EditPopupViewModel(eventAggregator);
        }
    }
    
}
