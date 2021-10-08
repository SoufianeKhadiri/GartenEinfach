using GardenEinfach.Model;
using Prism.Commands;
using Prism.Events;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GardenEinfach.ViewModels
{

    public class ImagePopupViewModel : BaseViewModel
    {
        IEventAggregator _ea;
        public ImagePopupViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }
        private DelegateCommand _TakeFoto;
        public DelegateCommand TakeFoto =>
        _TakeFoto ?? (_TakeFoto = new DelegateCommand(TakeFotoM));

        void TakeFotoM()
        {
            AddPostViewModel.FotoFromCamera.Execute();

            PopupNavigation.Instance.PopAsync(true);
        }

        private DelegateCommand _Gallery;
        public DelegateCommand Gallery =>
        _Gallery ?? (_Gallery = new DelegateCommand(GalleryM));

        void GalleryM()
        {
            AddPostViewModel.FotoFromGallery.Execute();
            PopupNavigation.Instance.PopAsync(true);
        }

    }
}

