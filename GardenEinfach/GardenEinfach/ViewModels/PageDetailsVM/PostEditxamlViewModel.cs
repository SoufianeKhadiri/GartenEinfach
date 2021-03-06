
using GardenEinfach.Model;
using GardenEinfach.Service;
using GardenEinfach.Views.PopUp;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{




    [QueryProperty(nameof(Key), nameof(Key))]
    public class PostEditxamlViewModel : BaseViewModel
    {
      


        private string _Key;
        public string Key
        {
            get { return _Key; }
            set { SetProperty(ref _Key, value);
                LoadItemId(value);
            }
        }

        public List<string> imagesPostPath { get; set; }

        private async void LoadItemId(string value)
        {
           var post =  await postService.GetPostById(value);
           
                Titel = post.Titel;
                Price = post.Price;
                Description = post.Description;
            
                Images.Clear();
                imagesPostPath.Clear();
               
                foreach (var item in post.Images)
                {
                Images.Add(new MyImage() { Source = item });
                imagesPostPath.Add(item);
                }

        }



        #region Post Props

        private string _Todo;
        public string Todo
        {
            get { return _Todo; }
            set { SetProperty(ref _Todo, value); }
        }

        private string _Titel;
            public string Titel
            {
                get { return _Titel; }
                set { SetProperty(ref _Titel, value); }
            }

            private string _Category;
            public string Category
            {
                get { return _Category; }
                set { SetProperty(ref _Category, value); }
            }

            private string[] _Categories;
            public string[] Categories
            {
                get { return _Categories; }
                set { SetProperty(ref _Categories, value); }
            }


            private string[] _ImagesPath;
            public string[] ImagesPath
            {
                get { return _ImagesPath; }
                set { SetProperty(ref _ImagesPath, value); }
            }


            private int _Price;
            public int Price
            {
                get { return _Price; }
                set { SetProperty(ref _Price, value); }
            }



            private string _Description;
            public string Description
            {
                get { return _Description; }
                set { SetProperty(ref _Description, value); }
            }

            #endregion


            private bool _Loading;
            public bool Loading
            {
                get { return _Loading; }
                set { SetProperty(ref _Loading, value); }
            }



            public static DelegateCommand FotoFromCamera { get; set; }
            public static DelegateCommand FotoFromGallery { get; set; }

            public static DelegateCommand CleanForm { get; set; }
            //ctor
            public PostEditxamlViewModel()
            {
                GetCurrentUserInfo();
                imagesPostPath = new List<string>();
                 Images = new ObservableCollection<MyImage>();
                ImgSource = "addImage.svg";
                DeleteFotoVisibility = false;
                ImagesStream = new List<Stream>();
                postService = new PostService();
                InitPicker();
                FotoFromCamera = new DelegateCommand(TakeFotoFromCamera);
                FotoFromGallery = new DelegateCommand(TakeFotoFromGalery);
                CleanForm = new DelegateCommand(EmptyForm);
                Loading = false;

                //MessagingSub();


            }

            private void GetCurrentUserInfo()
            {
                //var user = userService.;
                //Adress = user.FullyAdress;
                //Phone = user.Phone;
            }

            //private void MessagingSub()
            //{
            //    MessagingCenter.Subscribe<LoginViewModel, MyUser>(this, "UsrLogin", (vm, user) =>
            //    {
            //        if (user != null)
            //        {
            //            Adress = user.adress.Street + " " + user.adress.HouseNumber + " " + user.adress.City;
            //            Phone = user.Phone;
            //        }

            //    });
            //}

            private void InitPicker()
            {
                Categories = new string[] { "Category1", "Category2", "Category3", "Category4", "Category5", "Category6" };
            }

            private DelegateCommand _AddPost;
            public DelegateCommand EditPost =>
            _AddPost ?? (_AddPost = new DelegateCommand(EditPostM));

            async void EditPostM()
            {
                Loading = true;
                //UploadIMages
                List<string> imagesUrl = new List<string>();
                int imageNumber = 0;
                foreach (var item in ImagesStream)
                {
                    imagesUrl.Add(await postService.UploadImage(item, imageNumber.ToString(), Titel, "Posts"));
                    imageNumber++;
                }
            //Prepare Post to push to the Db
            foreach (var item in imagesPostPath)
            {               
                imagesUrl.Add(item);
            }
           
            Post p = new Post()
                {
                    Key = Key,
                    Images = imagesUrl,
                    Todo = Todo,
                    Price = Price,
                    Titel = Titel,
                    Description = Description,
                    Time = getCurrentTime(),
                    User = await userService.GetUsr(Email)
                };
                await postService.UpdateItemAsync(p);
                Loading = false;

            await Shell.Current.GoToAsync("..");
            await Application.Current.MainPage.DisplayToastAsync("Post updated", 1500);

        }

            private string getCurrentTime()
            {
                DateTime localDate = DateTime.Now;
                return localDate.ToString();

            }

            private bool _DeleteFotoVisibility;
            public bool DeleteFotoVisibility
            {
                get { return _DeleteFotoVisibility; }
                set { SetProperty(ref _DeleteFotoVisibility, value); }
            }


            private MyImage _Image;
            public MyImage Image
            {
                get { return _Image; }
                set
                {
                    SetProperty(ref _Image, value);
                    if (Image != null)
                    {
                        ImgSource = Image.Source;
                        imgStream = Image.ImageStream;
                        DeleteFotoVisibility = true;
                    }
                }
            }

            private ImageSource _ImgSource;
            public ImageSource ImgSource
            {
                get { return _ImgSource; }
                set { SetProperty(ref _ImgSource, value); }
            }




            private ObservableCollection<MyImage> _Images;
            public ObservableCollection<MyImage> Images
            {
                get { return _Images; }
                set { SetProperty(ref _Images, value); }
            }


            private DelegateCommand _DeleteFoto;
            public DelegateCommand DeleteFoto =>
            _DeleteFoto ?? (_DeleteFoto = new DelegateCommand(DeleteFotoM));

            void DeleteFotoM()
            {
                Images.Remove(Image);
                ImagesStream.Remove(Image.ImageStream);
                ImgSource = "addImage.svg";
                DeleteFotoVisibility = false;
            }



            private DelegateCommand _TakeFoto;
            public DelegateCommand TakeFoto =>
            _TakeFoto ?? (_TakeFoto = new DelegateCommand(TakeFotoM));

            void TakeFotoM()
            {
                PopupNavigation.Instance.PushAsync(new EditPopup());

            }


            private Stream _img;
            public Stream imgStream
            {
                get { return _img; }
                set { SetProperty(ref _img, value); }
            }


            private List<Stream> _ImagesStream;
            public List<Stream> ImagesStream
            {
                get { return _ImagesStream; }
                set { SetProperty(ref _ImagesStream, value); }
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

                        var src = ImageSource.FromStream(() => { return photo.GetStream(); });
                        //var path = photo.AlbumPath;
                        MyImage img = new MyImage { Source = src, ImageStream = photo.GetStream() };

                        imgStream = img.ImageStream;
                        ImagesStream.Add(imgStream);
                        Images.Add(img);

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

                        var src = ImageSource.FromStream(() => { return photo.GetStream(); });
                        MyImage img = new MyImage { Source = src, ImageStream = photo.GetStream() };

                        imgStream = img.ImageStream;
                        ImagesStream.Add(imgStream);
                        Images.Add(img);

                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "ok");

                }
            }

            private void EmptyForm()
            {
                Price = 0;
                Titel = Description = Category = "";
                Images.Clear();
            }


        private DelegateCommand _EditAddress;
        public DelegateCommand EditAddress =>
            _EditAddress ?? (_EditAddress = new DelegateCommand(ExecuteEditAddress));

        async void ExecuteEditAddress()
        {
            await Shell.Current.GoToAsync("ProfileSetting");
        }
    }
    }

