using GardenEinfach.ViewModels;
using Prism.Events;
using Xamarin.Forms;

namespace GardenEinfach.Views.PopUp
{
    public partial class ImagePopup
    {
        IEventAggregator eventAggregator;
        public ImagePopup()
        {
            InitializeComponent();
            BindingContext = new ImagePopupViewModel(eventAggregator);
        }
    }
}
