using GardenEinfach.Model;
using GardenEinfach.Service;
using GardenEinfach.Services;
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
        public IDataStore<Post> dataStore;
        public IUserService userService;
        public MyUser GetUserPreferences()
        {
            MyUser usr = new MyUser();
            Adress adress = new Adress();

            adress.Street = Preferences.Get("Street", "");
            adress.City = Preferences.Get("City", "");
            adress.HouseNumber = Preferences.Get("HouseNumber", "");

            usr.FirstName = Preferences.Get("FirstName", "");
            usr.LastName = Preferences.Get("LastName", "");
            usr.Email = Preferences.Get("Email", "");
            usr.Phone = Preferences.Get("Phone", "");
            usr.FullyAdress = Preferences.Get("Adress", "");
            usr.Gender = Preferences.Get("Gender", "");
            usr.adress = adress;

            return usr;
        }
        public BaseViewModel()
        {
            dataStore = new MockDataStore();
            userService = new UserService();
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
