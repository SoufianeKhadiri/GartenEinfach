using GardenEinfach.Model;
using GardenEinfach.Service;
using GardenEinfach.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //public IDataStore<Post> DataStore => DependencyService.Get<IDataStore<Post>>();
        public IPostService<Post> postService;
        public IUserService userService;

        public BaseViewModel()
        {
            postService = new PostService();
            userService = new UserService();
            GetCurrentUserInfo();
            GetUserInfoFromDb();
            //MessageSubscribe();
        }

        #region User Props

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set { SetProperty(ref _FirstName, value); }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { SetProperty(ref _LastName, value); }
        }


        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }
        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set { SetProperty(ref _Phone, value); }
        }
        private string _HouseNumber;
        public string HouseNumber
        {
            get { return _HouseNumber; }
            set { SetProperty(ref _HouseNumber, value); }
        }
        private string _City;
        public string City
        {
            get { return _City; }
            set { SetProperty(ref _City, value); }
        }
        private string _Street;
        public string Street
        {
            get { return _Street; }
            set { SetProperty(ref _Street, value); }
        }
        private string _UserImage;
        public string UserImage
        {
            get { return _UserImage; }
            set { SetProperty(ref _UserImage, value); }
        }

        private string _FullyAdress;
        public string FullyAdress
        {
            get { return _Street + " " + _HouseNumber + " " + _City; }
            set { SetProperty(ref _FullyAdress, value); }
        }

        private ImageSource _ImgSource;
        public ImageSource ImgSource
        {
            get { return _ImgSource; }
            set { SetProperty(ref _ImgSource, value); }
        }

        private string _Gender;
        public string Gender
        {
            get { return _Gender; }
            set { SetProperty(ref _Gender, value); }
        }

        #endregion
        public void GetCurrentUserInfo()
        {
            var user = userService.GetUserPreferences();

            //Street = user.adress.Street;
            //City = user.adress.City;
            //HouseNumber = user.adress.HouseNumber;
            //FirstName = user.FirstName;
            //LastName = user.LastName;
            Email = user.Email;
            //Phone = user.Phone;
            //UserImage = user.Image;
            //Gender = user.Gender;
        }


        public async void GetUserInfoFromDb()
        {
            var user = await userService.GetUsr(Email);

            Street = user.adress.Street;
            City = user.adress.City;
            HouseNumber = user.adress.HouseNumber;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.Phone;
            UserImage = user.Image;
            ImgSource = ImageSource.FromUri(new Uri(UserImage));
            Gender = user.Gender;

        }

        private void MessageSubscribe()
        {
            MessagingCenter.Subscribe<RegisterViewModel, MyUser>(this, "Usr", (vm, user) =>
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                UserImage = user.Image;
                Street = user.adress.Street;
                City = user.adress.City;
                HouseNumber = user.adress.HouseNumber;
                Email = user.Email;
                Phone = user.Phone;
                Gender = user.Gender;

            });
            MessagingCenter.Subscribe<LoginViewModel, MyUser>(this, "UsrLogin", (vm, user) =>
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                UserImage = user.Image;
                Street = user.adress.Street;
                City = user.adress.City;
                HouseNumber = user.adress.HouseNumber;
                Email = user.Email;
                Phone = user.Phone;
                Gender = user.Gender;
            });
            //MessagingCenter.Subscribe<AccountViewModel, MyUser>(this, "UpdateUser", (vm, user) =>
            //{
            //    FirstName = user.FirstName;
            //    LastName = user.LastName;
            //    UserImage = user.Image;
            //    Street = user.adress.Street;
            //    City = user.adress.City;
            //    HouseNumber = user.adress.HouseNumber;
            //    Email = user.Email;
            //    Phone = user.Phone;
            //});
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
