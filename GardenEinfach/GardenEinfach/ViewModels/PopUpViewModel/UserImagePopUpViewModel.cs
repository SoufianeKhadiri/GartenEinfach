using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class UserImagePopUpViewModel : BaseViewModel
    {
        public UserImagePopUpViewModel()
        {

        }


        private DelegateCommand _Camera;
        public DelegateCommand Camera =>
        _Camera ?? (_Camera = new DelegateCommand(CameraM));

        private void CameraM()
        {
            AccountViewModel.FotoCamera.Execute();
        }

        private DelegateCommand _Gallery;
        public DelegateCommand Gallery =>
        _Gallery ?? (_Gallery = new DelegateCommand(Gallerym));

        void Gallerym()
        {
            AccountViewModel.FotoGallery.Execute();
        }

    }
}
