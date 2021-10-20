using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GardenEinfach.ViewModels
{
    public class ProfileSettingViewModel : BaseViewModel
    {
        //#region Props

        //private string _FirstName;
        //public string FirstName
        //{
        //    get { return _FirstName; }
        //    set { SetProperty(ref _FirstName, value); }
        //}

        //private string _LastName;
        //public string LastName
        //{
        //    get { return _LastName; }
        //    set { SetProperty(ref _LastName, value); }
        //}


        //private string _Email;
        //public string Email
        //{
        //    get { return _Email; }
        //    set { SetProperty(ref _Email, value); }
        //}
        //private string _Phone;
        //public string Phone
        //{
        //    get { return _Phone; }
        //    set { SetProperty(ref _Phone, value); }
        //}
        //private string _HouseNumber;
        //public string HouseNumber
        //{
        //    get { return _HouseNumber; }
        //    set { SetProperty(ref _HouseNumber, value); }
        //}
        //private string _City;
        //public string City
        //{
        //    get { return _City; }
        //    set { SetProperty(ref _City, value); }
        //}
        //private string _Street;
        //public string Street
        //{
        //    get { return _Street; }
        //    set { SetProperty(ref _Street, value); }
        //}
        //#endregion


        public ProfileSettingViewModel()
        {
            //GetCurrentUserInfo();
        }
        private void GetCurrentUserInfo()
        {
            var user = userService.GetUserPreferences();

            Street = user.adress.Street;
            City = user.adress.City;
            HouseNumber = user.adress.HouseNumber;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.Phone;

        }
    }
}
