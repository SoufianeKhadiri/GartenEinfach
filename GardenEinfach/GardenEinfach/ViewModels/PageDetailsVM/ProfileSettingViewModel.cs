using GardenEinfach.Model;
using GardenEinfach.Service;
using GardenEinfach.Views.PopUp;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class ProfileSettingViewModel : BaseViewModel
    {

        private bool _Loading;
        public bool Loading
        {
            get { return _Loading; }
            set { SetProperty(ref _Loading, value); }
        }
        private System.Func<Stream> _ImgStream;
        public System.Func<Stream> ImgStream
        {
            get { return _ImgStream; }
            set { SetProperty(ref _ImgStream, value); }
        }

        private string _NewUserImage;
        public string NewUserImage
        {
            get { return _NewUserImage; }
            set { SetProperty(ref _NewUserImage, value); }
        }

        private string _EmailInput;
        public string EmailInput
        {
            get { return _EmailInput; }
            set { SetProperty(ref _EmailInput, value); }
        }
        public static DelegateCommand FotoCamera { get; set; }
        public static DelegateCommand FotoGallery { get; set; }

        public ProfileSettingViewModel()
        {
            EmailInput = Email;
            FotoCamera = new DelegateCommand(TakeFotoFromCamera);
            FotoGallery = new DelegateCommand(TakeFotoFromGalery);
        }

        private DelegateCommand _ChangeUserImage;
        public DelegateCommand ChangeUserImage =>
        _ChangeUserImage ?? (_ChangeUserImage = new DelegateCommand(ChangeUserImageM));


        private DelegateCommand _Back;
        public DelegateCommand Back =>
        _Back ?? (_Back = new DelegateCommand(BackM));

        void BackM()
        {
            Shell.Current.GoToAsync("..");
        }

        private DelegateCommand _SaveUserInfo;
        public DelegateCommand SaveUserInfo =>
        _SaveUserInfo ?? (_SaveUserInfo = new DelegateCommand(SaveUserInfoM));

        async void SaveUserInfoM()
        {
            Loading = true;

            try
            {
                if (ImgStream != null)
                {
                    NewUserImage = await userService.UploadUserImage(ImgStream(), Email, "UsersImages");
                    var newUsr = userService.CreateUser(FirstName, LastName, EmailInput, Phone,
                                                        Gender, NewUserImage, Street, HouseNumber, City);
                    await userService.UpdateUserInfo(newUsr, Email);

                    userService.SetUserPreferences(newUsr);
                }
                else
                {

                    var newUser = userService.CreateUser(FirstName, LastName, EmailInput, Phone,
                                                        Gender, UserImage, Street, HouseNumber, City);
                    await userService.UpdateUserInfo(newUser, Email);

                    userService.SetUserPreferences(newUser);
                }
            }
            catch (Exception)
            {

                throw;
            }



            Loading = false;
            HomeViewModel.RefreshUserInfo.Execute();
            AccountViewModel.RefreshUserInfo.Execute();

            await Shell.Current.GoToAsync("..");
            await Application.Current.MainPage.DisplayToastAsync("user info updated", 1500);
        }


        async void ChangeUserImageM()
        {
            UserImagePopUpViewModel.FromAccount = false;
            await PopupNavigation.Instance.PushAsync(new UserImagePopUp());
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
                    SaveToAlbum = false
                });
                if (photo != null)
                {
                    ImgStream = photo.GetStream;
                    ImgSource = ImageSource.FromStream(ImgStream);
                    //NewUserImage = photo.GetStream;
                    //NewUserImage = await userService.UploadUserImage(photo.GetStream(), Preferences.Get("Email", ""), "UsersImages");

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
                    ImgStream = photo.GetStream;
                    ImgSource = ImageSource.FromStream(ImgStream);
                    //NewUserImage = await userService.UploadUserImage(ImgStream(), Preferences.Get("Email", ""), "UsersImages");
                    //Preferences.Set("UserImage", UserImage);
                    //await userService.UpdateUserFoto(Email, UserImage, userService.GetUserPreferences());
                    //HomeViewModel.RefreshUserInfo.Execute();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "ok");

            }
        }

    }
}
