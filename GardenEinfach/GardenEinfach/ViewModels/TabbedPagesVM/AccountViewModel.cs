
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

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }

        public AccountViewModel()
        {
            UserName = Preferences.Get("Username", "");
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



        #endregion
    }
}
