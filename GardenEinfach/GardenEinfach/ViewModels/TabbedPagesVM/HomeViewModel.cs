
using Firebase.Database;
using GardenEinfach.Model;
using GardenEinfach.Service;
using GardenEinfach.Views.PageDetails;
using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    [QueryProperty(nameof(Login), nameof(Login))]
    public class HomeViewModel : BaseViewModel
    {

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value);
                RefreshM();
            }
        }


        #region Refresh

        public DelegateCommand Refresh { get; set; }

        public static DelegateCommand RefreshFromPostDetail { get; set; }

        private bool _IsRegreshing;
        public bool IsRefreshing
        {
            get { return _IsRegreshing; }
            set { SetProperty(ref _IsRegreshing, value); }
        }

        #endregion


        public Command LoadItemsCommand { get; }
        public ObservableCollection<Post> Items { get; }
        //FirebaseClient client;
        private string _PostsNumber;
        public string PostsNumber
        {
            get { return _PostsNumber; }
            set { SetProperty(ref _PostsNumber, value); }
        }


        private Post _MyPostDetail;
        public Post MyPostDetail
        {
            get { return _MyPostDetail; }
            set
            {
                SetProperty(ref _MyPostDetail, value);

                if (MyPostDetail != null)
                {
                    OnItemSelected(MyPostDetail);
                    MyPostDetail = null;

                }
            }
        }


        private Post _Post;
        public Post Post
        {
            get { return _Post; }
            set
            {
                SetProperty(ref _Post, value);

                if (Post != null)
                {
                    OnItemSelected(Post);
                    Post = null;

                }
            }
        }

        async void OnItemSelected(Post post)
        {
            if (post == null)
            {
                return;
            }
            else
            {

                // This will push the ItemDetailPage onto the navigation stack
                await Shell.Current
                    .GoToAsync($"{nameof(PostDetail)}?{nameof(PostDetailViewModel.Key)}={post.Key}");

            }

        }
        private ObservableCollection<Post> _Posts;
        public ObservableCollection<Post> PostsList
        {
            get { return _Posts; }
            set
            {
                SetProperty(ref _Posts, value);
                PostsNumber = PostsList.Count().ToString();
            }
        }


        private int _PostsNTYNumber;
        public int PostsNTYNumber
        {
            get { return _PostsNTYNumber; }
            set { SetProperty(ref _PostsNTYNumber, value); }
        }

        private ObservableCollection<Post> _PostsNearToyou;
        public ObservableCollection<Post> PostsNearToyou
        {
            get { return _PostsNearToyou; }
            set { SetProperty(ref _PostsNearToyou, value); }
        }


        public DelegateCommand ShowAllMyPosts { get; set; }
        public DelegateCommand ShowAllPosts { get; set; }
        public string DetectedPlz { get; set; }
        public DelegateCommand NavigateForwardCommand { get; set; }

        private DelegateCommand _ShowAllPostsNTY;
        public DelegateCommand ShowAllPostsNTY =>
            _ShowAllPostsNTY ?? (_ShowAllPostsNTY = new DelegateCommand(ExecuteShowAllPostsNTY));

        async void ExecuteShowAllPostsNTY()
        {
            if (!string.IsNullOrEmpty(DetectedPlz)) {

                await Shell.Current
              .GoToAsync($"{nameof(Posts)}?{nameof(PostsViewModel.FilterKey)}={DetectedPlz}");
            }
        }

        //ctor
        public HomeViewModel()
        {

            getPostsData();
            ShowAllMyPosts = new DelegateCommand(ShowMyPosts);
            ShowAllPosts = new DelegateCommand(ShowPosts);
            Refresh = new DelegateCommand(RefreshM);

            RefreshFromPostDetail = new DelegateCommand(RefreshM);

            getPostsNearToYou();
        }

        private async void getPostsNearToYou()
        {
            await OnGetPosition();
        }

        async Task OnGetPosition()
        {

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    double lat = location.Latitude;
                    double longt = location.Longitude;
                   
                }

                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);

                Placemark placemark = placemarks.FirstOrDefault();


                if (placemark == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to detect your position", "ok");

                }
                else
                {
                    

                    var posts = await postService.PostsByPlz(placemark.PostalCode);


                    if (posts.Count > 0)
                    {
                        DetectedPlz = placemark.PostalCode;
                        PostsNearToyou = new ObservableCollection<Post>(posts);
                        PostsNTYNumber = PostsNearToyou.Count();
                        //await Shell.Current
                        //   .GoToAsync($"{nameof(Posts)}?{nameof(PostsViewModel.FilterKey)}={placemark.PostalCode}");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Search", "No posts found!", "ok");
                    }


                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to detect your position", "ok");
            }

        }

        private void RefreshM()
        {
            IsRefreshing = true;

            getPostsData();
            IsRefreshing = false;
        }

        private IEnumerable<Post> _PostList;
        public IEnumerable<Post> PostList
        {
            get { return _PostList; }
            set { SetProperty(ref _PostList, value); }
        }
        public static ObservableCollection<Post> PtsList;

        public async void getPostsData()
        {
            var posts = await postService.GetMyPosts();
            PostsList = new ObservableCollection<Post>(posts);
            
            PtsList = PostsList;
            PostsNumber = PostsList.Count().ToString();
        }

        private async void ShowPosts()
        {
            await Shell.Current.GoToAsync($"{ nameof(Posts)}");

        }



        private DelegateCommand _GoAccount;
        public DelegateCommand GoAccount =>
        _GoAccount ?? (_GoAccount = new DelegateCommand(GoAccountM));

        async void GoAccountM()
        {
            await Shell.Current.GoToAsync("//HomePage/Account");
        }



        public void OnAppearing()
        {
            IsBusy = true;

        }
        private async void ShowMyPosts()
        {
            await Shell.Current.GoToAsync($"{ nameof(MyPosts)}");


        }
    }
}


