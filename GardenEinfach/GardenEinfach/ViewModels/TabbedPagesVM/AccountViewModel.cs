
using GardenEinfach.Views.PageDetails;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        public AccountViewModel()
        {

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

        void LogoutM()
        {

        }



        #endregion
    }
}
