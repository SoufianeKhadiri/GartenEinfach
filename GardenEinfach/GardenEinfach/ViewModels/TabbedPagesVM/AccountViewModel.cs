
using GardenEinfach.Model;
using GardenEinfach.Views.PageDetails;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }

        public AccountViewModel()
        {
            UserName = Preferences.Get("FirstName", "");
            MessagingCenter.Subscribe<RegisterViewModel, MyUser>(this, "Usr", (vm, user) =>
                {
                    UserName = user.FirstName;

                });
            MessagingCenter.Subscribe<LoginViewModel, MyUser>(this, "UsrLogin", (vm, user) =>
            {
                UserName = user.FirstName;

            });

        }
        #region Commands
        private DelegateCommand _GoToMyPosts;
        public DelegateCommand GoToMyPosts =>
        _GoToMyPosts ?? (_GoToMyPosts = new DelegateCommand(GoToMyPostsM));

        async void GoToMyPostsM()
        {
            await Shell.Current.GoToAsync($"{nameof(MyPosts)}");
        }


        private DelegateCommand _Logout;
        public DelegateCommand Logout =>
        _Logout ?? (_Logout = new DelegateCommand(LogoutM));

        async void LogoutM()
        {
            Preferences.Set("myFirebaseRefreshToken", "");
            await Shell.Current.GoToAsync("//Login");
        }


        private DelegateCommand _ProfileSettingC;
        public DelegateCommand ProfileSettingC =>
        _ProfileSettingC ?? (_ProfileSettingC = new DelegateCommand(ProfileSettingM));

        async void ProfileSettingM()
        {
            await Shell.Current.GoToAsync("ProfileSetting");
        }


        #endregion
    }
}
