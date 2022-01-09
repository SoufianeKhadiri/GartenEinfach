using GardenEinfach.ViewModels;
using Prism.Commands;
using Prism.Events;
using Rg.Plugins.Popup.Services;

namespace GardenEinfach.ViewModels
{
    
    public class EditPopupViewModel : BaseViewModel
    {
        IEventAggregator _ea;
        public EditPopupViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }
        private DelegateCommand _TakeFoto;
        public DelegateCommand TakeFoto =>
        _TakeFoto ?? (_TakeFoto = new DelegateCommand(TakeFotoM));

        void TakeFotoM()
        {
            PostEditxamlViewModel.FotoFromCamera.Execute();

            PopupNavigation.Instance.PopAsync(true);
        }

        private DelegateCommand _Gallery;
        public DelegateCommand Gallery =>
        _Gallery ?? (_Gallery = new DelegateCommand(GalleryM));

        void GalleryM()
        {
            PostEditxamlViewModel.FotoFromGallery.Execute();
            PopupNavigation.Instance.PopAsync(true);
        }

    }
}
