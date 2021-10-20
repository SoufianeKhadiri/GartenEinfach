using Firebase.Auth;
using GardenEinfach.Model;
using GardenEinfach.Views.SignIn_Up;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {


        private string _EmailInput;
        public string EmailInput
        {
            get { return _EmailInput; }
            set { SetProperty(ref _EmailInput, value); }
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


        private bool _RememberMe;
        public bool RememberMe
        {
            get { return _RememberMe; }
            set { SetProperty(ref _RememberMe, value); }
        }

        private DelegateCommand _LoginCommand;
        public DelegateCommand LoginCommand =>
        _LoginCommand ?? (_LoginCommand = new DelegateCommand(LoginMethode));

        public async void LoginMethode()
        {
            try
            {


                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(EmailInput, Password);
                var token = auth.GetFreshAuthAsync();
                var SerializedContent = JsonConvert.SerializeObject(token);

                if (RememberMe == true)
                {
                    Preferences.Set("myFirebaseRefreshToken", SerializedContent);
                }

                SetUserInfo();

                await Shell.Current.GoToAsync("//HomePage/Home");

            }
            catch (Exception)
            {

                await App.Current.MainPage.DisplayAlert("Alert", "Invalid Email or Password", "ok");
            }
        }

        async void SetUserInfo()
        {

            var user = await userService.GetUsr(EmailInput);


            MessagingCenter.Send(this, "UsrLogin", user);

            Preferences.Set("FirstName", user.FirstName);
            Preferences.Set("LastName", user.LastName);
            Preferences.Set("Email", user.Email);
            Preferences.Set("Phone", user.Phone);
            Preferences.Set("Adress", user.adress.Street + user.adress.HouseNumber + user.adress.City);
            Preferences.Set("Gender", user.Gender);
            Preferences.Set("Street", user.adress.Street);
            Preferences.Set("City", user.adress.City);
            Preferences.Set("HouseNumber", user.adress.HouseNumber);
            Preferences.Set("UserImage", user.Image);


        }



        private DelegateCommand _Register;
        public DelegateCommand RegisterC =>
        _Register ?? (_Register = new DelegateCommand(RegisterM));

        async void RegisterM()
        {

            await Shell.Current.GoToAsync("//Login/Register");
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

