using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class AddPostPopupViewModel : BaseViewModel
    {
        public AddPostPopupViewModel()
        {

        }

        private DelegateCommand _Done;
        public DelegateCommand Done =>
        _Done ?? (_Done = new DelegateCommand(DoneM));

        void DoneM()
        {
            PopupNavigation.Instance.PopAsync(true);
            AddPostViewModel.CleanForm.Execute();
            Shell.Current.GoToAsync("//HomePage/Home");
        }


        private DelegateCommand _AnotherPost;
        public DelegateCommand AnotherPost =>
        _AnotherPost ?? (_AnotherPost = new DelegateCommand(AnotherPostM));

        void AnotherPostM()
        {
            PopupNavigation.Instance.PopAsync(true);
            AddPostViewModel.CleanForm.Execute();

        }


    }
}
