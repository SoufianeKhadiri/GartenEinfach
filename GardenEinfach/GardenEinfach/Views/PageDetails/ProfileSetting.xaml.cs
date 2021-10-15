using GardenEinfach.ViewModels;
using Xamarin.Forms;

namespace GardenEinfach.Views.PageDetails
{
    public partial class ProfileSetting : ContentPage
    {
        public ProfileSetting()
        {
            InitializeComponent();
            BindingContext = new ProfileSettingViewModel();
        }
    }
}
