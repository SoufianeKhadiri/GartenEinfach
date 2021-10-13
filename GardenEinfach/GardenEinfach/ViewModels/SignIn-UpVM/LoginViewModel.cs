using Firebase.Auth;
using GardenEinfach.Views.SignIn_Up;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {


        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }


        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }


        public string WebApiKey = "AIzaSyD_rzOMR_cTLm9JYCo1WylNJqXOc56rTvI";

        //ctor

        public LoginViewModel()
        {

        }

        private DelegateCommand _LoginCommand;
        public DelegateCommand LoginCommand =>
        _LoginCommand ?? (_LoginCommand = new DelegateCommand(LoginMethode));

        public async void LoginMethode()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
            var token = auth.GetFreshAuthAsync();
            var SerializedContent = JsonConvert.SerializeObject(token);
            Preferences.Set("myFirebaseRefreshToken", SerializedContent);
            getUserInfo();

            await Shell.Current.GoToAsync("//HomePage");
            //NavigatoToPage("login", "MainTabbedPage", Email);


            //await App.Current.MainPage.DisplayAlert("Alert", gettoken, "ok");

        }

        async void getUserInfo()
        {

            var userInfo = await userService.GetUserInfo(Email);
            Preferences.Set("UserName", userInfo.FirstName);

        }

        private void saveUserInfo(string UserName)
        {
            Preferences.Set("Username", UserName);
        }

        private DelegateCommand _Register;
        public DelegateCommand RegisterC =>
        _Register ?? (_Register = new DelegateCommand(RegisterM));

        async void RegisterM()
        {

            await Shell.Current.GoToAsync($"{nameof(Register)}");
        }


        //private async void NavigatoToPage<T>(string titel, string page, T data)
        //{
        ////    var navigationParams = new NavigationParameters
        ////{
        ////     { titel, data }
        ////};

        //    await _nav.NavigateAsync(page, navigationParams);
        //}
    }
}

