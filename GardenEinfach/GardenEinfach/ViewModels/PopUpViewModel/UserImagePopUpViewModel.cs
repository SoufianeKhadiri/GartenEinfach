using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
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
        public static bool FromAccount { get; set; }

        private DelegateCommand _Camera;
        public DelegateCommand Camera =>
        _Camera ?? (_Camera = new DelegateCommand(CameraM));

        private void CameraM()
        {

            if (FromAccount == true)
            {
                AccountViewModel.FotoCamera.Execute();
            }
            else
            {
                ProfileSettingViewModel.FotoCamera.Execute();
            }

            PopupNavigation.Instance.PopAsync(true);
        }

        private DelegateCommand _Gallery;
        public DelegateCommand Gallery =>
        _Gallery ?? (_Gallery = new DelegateCommand(Gallerym));

        void Gallerym()
        {
            AccountViewModel.FotoGallery.Execute();
            PopupNavigation.Instance.PopAsync(true);
        }

    }
}
