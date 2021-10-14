using Firebase.Auth;
using GardenEinfach.Model;
using GardenEinfach.Views.SignIn_Up;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {

        public string WebApiKey = "AIzaSyD_rzOMR_cTLm9JYCo1WylNJqXOc56rTvI";

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


        private string _FistName;
        public string FirstName
        {
            get { return _FistName; }
            set { SetProperty(ref _FistName, value); }
        }


        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { SetProperty(ref _LastName, value); }
        }


        private DelegateCommand _Login;
        public DelegateCommand LoginC =>
        _Login ?? (_Login = new DelegateCommand(CommandMthode));

        async void CommandMthode()
        {

            await Shell.Current.GoToAsync("..");
        }


        private DelegateCommand _Register;
        public DelegateCommand Register =>
        _Register ?? (_Register = new DelegateCommand(RegisterM));

        async void RegisterM()
        {
            Emails = userService.getUsers();
            if (Emails.Count() <= 0)
            {
                CreateUserFirebaseAuth();
                RegisterUserRealtimeDatabase();
            }

            foreach (var item in Emails)
            {
                if (Email == item.Email)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Email already exist", "ok");
                    break;
                }
                else
                {
                    CreateUserFirebaseAuth();
                    RegisterUserRealtimeDatabase();
                    break;

                }
            }




        }

        private async void CreateUserFirebaseAuth()
        {
            //var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));

            //var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);

            //var token = auth.FirebaseToken;
            string token = await userService.CreateUserFirebaseAuth(Email, Password);
            var SerializedContent = JsonConvert.SerializeObject(token);
            Preferences.Set("myFirebaseRefreshToken", SerializedContent);
            saveUserInfo(FirstName);
            RegisterUserRealtimeDatabase();
            await App.Current.MainPage.DisplayAlert("Success", "Account created!", "ok");
            await Shell.Current.GoToAsync("//HomePage");
            //NavigatoToPage("login", "MainTabbedPage", Email);
        }

        private async void RegisterUserRealtimeDatabase()
        {
            MyUser myUser = new MyUser() { Email = Email, FirstName = FirstName, LastName = LastName };
            await userService.AddUser(myUser);
        }

        private void saveUserInfo(string UserName)
        {
            Preferences.Set("Username", UserName);
        }

        //private async void NavigatoToPage<T>(string titel, string page, T data)
        //{
        //    //var navigationParams = new NavigationParameters
        //    //{
        //    //     { titel, data }
        //    //};

        //    await _nav.NavigateAsync(page, navigationParams);
        //}
        //ctor
        //PostService postService;

        private ObservableCollection<MyUser> _Emails;
        public ObservableCollection<MyUser> Emails
        {
            get { return _Emails; }
            set { SetProperty(ref _Emails, value); }
        }
        public RegisterViewModel()
        {
            //_nav = navigationService;
            //postService = new PostService();
            Emails = new ObservableCollection<MyUser>();

        }
    }
}
