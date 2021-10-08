
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
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public Command LoadItemsCommand { get; }
        public ObservableCollection<Post> Items { get; }
        FirebaseClient client;
        private string _PostsNumber;
        public string PostsNumber
        {
            get { return _PostsNumber; }
            set { SetProperty(ref _PostsNumber, value); }
        }


        private Post _PostDetail;
        public Post MyPostDetail
        {
            get { return _PostDetail; }
            set
            {
                SetProperty(ref _PostDetail, value);

                if (MyPostDetail != null)
                {
                    //NavigatoToPage("MyPostDetail", "MyPostDetail", PostDetail);
                    //Navigate("MyPostDetail", Post);

                    MyPostDetail = null;

                }
            }
        }

        //private DelegateCommand _NavigateForwardCommand;
        //public DelegateCommand NavigateForwardCommand =>
        //_NavigateForwardCommand ?? (_NavigateForwardCommand = new DelegateCommand(CommandMthode));

        //async void CommandMthode()
        //{
        //    await NavigationService.NavigateAsync("/NavigationPage/TestContent");
        //}


        private Post _Post;
        public Post Post
        {
            get { return _Post; }
            set
            {
                SetProperty(ref _Post, value);
                OnItemSelected(Post);
                //if (Post != null)
                //{
                //    //NavigatoToPage("MyPostDetail", "MyPostDetail", Post);
                //    //Navigate("MyPostDetail", Post);
                //    Post = null;

                //}
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

        public DelegateCommand ShowAllMyPosts { get; set; }
        public DelegateCommand ShowAllPosts { get; set; }

        INavigation Navigation;

        public DelegateCommand NavigateForwardCommand { get; set; }

        //ctor
        public HomeViewModel()
        {



            getPostsData();
            ShowAllMyPosts = new DelegateCommand(ShowMyPosts);
            ShowAllPosts = new DelegateCommand(ShowPosts);

        }


        public static PostService postService;

        private IEnumerable<Post> _PostList;
        public IEnumerable<Post> PostList
        {
            get { return _PostList; }
            set { SetProperty(ref _PostList, value); }
        }
        public static ObservableCollection<Post> PtsList;

        public async void getPostsData()
        {
            var posts = await dataStore.GetItemsAsync();
            PostsList = new ObservableCollection<Post>(posts);
            PtsList = PostsList;
            PostsNumber = PostsList.Count().ToString();
        }

        private async void ShowPosts()
        {
            await Shell.Current.GoToAsync($"{ nameof(Posts)}");


            //NavigatoToPage("posts", "Posts", Posts);

            //var navigationParams = new NavigationParameters
            //{
            //     { "posts", Posts }
            //};

            //await NavigationService.NavigateAsync("Posts", navigationParams);
        }

        //private async void NavigatoToPage<T>(string titel, string page, T data)
        //{
        //    //var navigationParams = new NavigationParameters
        //    //{
        //    //     { titel, data }
        //    //};



        //    //await  NavigationService.NavigateAsync("MainTabbedPage?selectedTab=Home" , navigationParams);
        //    //await NavigationService.NavigateAsync(page, navigationParams);
        //}
        //private async void Navigate<T>(string titel, T data)
        //{
        //    //var navigationParams = new NavigationParameters
        //    //{
        //    //     { titel, data }
        //    //};

        //    //await NavigationService.NavigateAsync($"{nameof(MyPostDetail)}", navigationParams);
        //}

        //private DelegateCommand _GoAccount;
        //public DelegateCommand GoAccount =>
        //_GoAccount ?? (_GoAccount = new DelegateCommand(GoAccountM));

        //async void GoAccountM()
        //{
        //    //await NavigationService.NavigateAsync("Account");
        //}

        public void OnAppearing()
        {
            IsBusy = true;

        }
        private async void ShowMyPosts()
        {
            await Shell.Current.GoToAsync($"{ nameof(MyPosts)}");
            //var navigationParams = new NavigationParameters
            //{
            //     { "posts", Posts }
            //};


            //await NavigationService.SelectTabAsync("Home");
            //await NavigationService.NavigateAsync("MyPosts", navigationParams);

        }
    }
}

//        private DelegateCommand _test;
//        public DelegateCommand Test =>
//        _test ?? (_test = new DelegateCommand(CommandMthode));

//        async void CommandMthode()
//        {
//            await Navigation.PushAsync(new PostDetail());
//        }

//        INavigation Navigation;
//        public HomeViewModel(INavigation navigation)
//        {
//            Navigation = navigation;
//        }
//    }
//}
