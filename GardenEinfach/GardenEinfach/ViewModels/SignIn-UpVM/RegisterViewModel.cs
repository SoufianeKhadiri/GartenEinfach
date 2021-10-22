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
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {

        #region Props

        private ObservableCollection<MyUser> _Emails;
        public ObservableCollection<MyUser> Emails
        {
            get { return _Emails; }
            set { SetProperty(ref _Emails, value); }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }



        private List<string> _GenderList;
        public List<string> GenderList
        {
            get { return _GenderList; }
            set { SetProperty(ref _GenderList, value); }
        }



        #endregion

        #region Commands
        private DelegateCommand _Login;
        public DelegateCommand LoginC =>
        _Login ?? (_Login = new DelegateCommand(CommandMthode));

        async void CommandMthode()
        {

            await Shell.Current.GoToAsync("..");
        }


        private DelegateCommand _Register;
        public DelegateCommand RegisterC =>
        _Register ?? (_Register = new DelegateCommand(RegisterM));

        #endregion

        //ctor
        public RegisterViewModel()
        {

            Emails = new ObservableCollection<MyUser>();
            GenderList = new List<string>();
            GenderList.Add("Male");
            GenderList.Add("Female");
            GenderList.Add("Other");

        }

        async void RegisterM()
        {

            string token;

            try
            {
                token = await userService.CreateUserFirebaseAuth(Email, Password);
                var SerializedContent = JsonConvert.SerializeObject(token);
                Preferences.Set("myFirebaseRefreshToken", SerializedContent);
                RegisterUserRealtimeDatabase();
                await App.Current.MainPage.DisplayAlert("Success", "Account created!", "ok");
                await Shell.Current.GoToAsync("//HomePage");


            }
            catch (FirebaseAuthException ex)
            {
                string message = "";
                if (ex.Reason == AuthErrorReason.InvalidEmailAddress)
                {
                    message = "Invalid email adress, please try again!";
                }
                else if (ex.Reason == AuthErrorReason.EmailExists)
                {
                    message = "Email already exisit, please try again!";
                }
                else
                {
                    message = ex.Reason.ToString();
                }
                await App.Current.MainPage.DisplayAlert("Alert", message, "ok");
            }

        }

        private void emptyForm()
        {
            FirstName = LastName = Phone = HouseNumber = Street = City =
            Email = Password = Gender = "";

        }



        private async void RegisterUserRealtimeDatabase()
        {

            MyUser user = new MyUser()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                adress = new Adress() { City = City, HouseNumber = HouseNumber, Street = Street },
                Gender = Gender,
                Image = userService.SetUserImage(Gender)

            };
            await userService.AddUser(user);
            Preferences.Set("Email", user.Email);
            MessagingCenter.Send(this, "Register", user.Email);
            emptyForm();
            // saveUserInfo(user);


        }

        //private void saveUserInfo(MyUser user)
        //{
        //    Preferences.Set("FirstName", user.FirstName);
        //    Preferences.Set("LastName", user.LastName);
        //    Preferences.Set("Email", user.Email);
        //    Preferences.Set("Phone", user.Phone);
        //    Preferences.Set("Adress", user.adress.Street + user.adress.HouseNumber + user.adress.City);
        //    Preferences.Set("Gender", user.Gender);
        //    Preferences.Set("Street", user.adress.Street);
        //    Preferences.Set("City", user.adress.City);
        //    Preferences.Set("HouseNumber", user.adress.HouseNumber);
        //    Preferences.Set("UserImage", user.Image);
        //}


    }
}
