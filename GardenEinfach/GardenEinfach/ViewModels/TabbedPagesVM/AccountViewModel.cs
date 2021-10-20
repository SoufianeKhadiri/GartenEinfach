
using GardenEinfach.Model;
using GardenEinfach.Views.PageDetails;
using GardenEinfach.Views.PopUp;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        #region Props


        //private string _UserImage;
        //public string UserImage
        //{
        //    get { return _UserImage; }
        //    set { SetProperty(ref _UserImage, value); }
        //}
        //private string _Email;
        //public string Email
        //{
        //    get { return _Email; }
        //    set { SetProperty(ref _Email, value); }
        //}

        //private string _UserName;
        //public string UserName
        //{
        //    get { return _UserName; }
        //    set { SetProperty(ref _UserName, value); }
        //}
        #endregion
        public static DelegateCommand FotoCamera { get; set; }
        public static DelegateCommand FotoGallery { get; set; }

        //ctor
        public AccountViewModel()
        {
            //FirstName = Preferences.Get("FirstName", "");
            //UserImage = userService.SetUserImage(Preferences.Get("Gender", ""));
            // MessageSubscribe();
            FotoCamera = new DelegateCommand(TakeFotoFromCamera);
            FotoGallery = new DelegateCommand(TakeFotoFromGalery);
        }
        public async void TakeFotoFromCamera()
        {

            try
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                    //PhotoSize = PhotoSize.Small,
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 10,
                    Directory = "Xamarin",
                    SaveToAlbum = true
                });
                if (photo != null)
                {

                    UserImage = await userService.UploadUserImage(photo.GetStream(), Preferences.Get("Email", ""), "UsersImages");
                    Preferences.Set("UserImage", UserImage);
                    await userService.UpdateUserFoto(Email, UserImage, userService.GetUserPreferences());
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "ok");

            }
        }
        public async void TakeFotoFromGalery()
        {

            try
            {
                var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()
                {
                    //PhotoSize = PhotoSize.Small
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 10

                });
                if (photo != null)
                {

                    UserImage = await userService.UploadUserImage(photo.GetStream(), Preferences.Get("Email", ""), "UsersImages");
                    Preferences.Set("UserImage", UserImage);
                    await userService.UpdateUserFoto(Email, UserImage, userService.GetUserPreferences());
                    //MessagingCenter.Send(this, "UpdateUser", userService.GetUserPreferences());
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "ok");

            }
        }
        //private void MessageSubscribe()
        //{
        //    MessagingCenter.Subscribe<RegisterViewModel, MyUser>(this, "Usr", (vm, user) =>
        //    {
        //        FirstName = user.FirstName;
        //        UserImage = userService.SetUserImage(user.Gender);

        //    });
        //    MessagingCenter.Subscribe<LoginViewModel, MyUser>(this, "UsrLogin", (vm, user) =>
        //    {
        //        FirstName = user.FirstName;
        //        UserImage = userService.SetUserImage(user.Gender);
        //    });
        //}

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


        private DelegateCommand _ChangeUserImage;
        public DelegateCommand ChangeUserImage =>
        _ChangeUserImage ?? (_ChangeUserImage = new DelegateCommand(ChangeUserImageM));

        async void ChangeUserImageM()
        {
            await PopupNavigation.Instance.PushAsync(new UserImagePopUp());
        }




        #endregion
    }
}
