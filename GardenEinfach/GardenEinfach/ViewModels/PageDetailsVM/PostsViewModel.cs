using GardenEinfach.Model;
using GardenEinfach.Views.PageDetails;
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
    [QueryProperty(nameof(FilterKey), nameof(FilterKey))]
    public class PostsViewModel : BaseViewModel
    {
        private ObservableCollection<Post> _Posts;
        public ObservableCollection<Post> Posts
        {
            get { return _Posts; }
            set { SetProperty(ref _Posts, value);
            }
        }
        
        private string _FilterKey;
        public string FilterKey
        {
            get { return _FilterKey; }
            set { SetProperty(ref _FilterKey, value);
                LoadItemId(value);

            }
        }


        public async void LoadItemId(string key)
        {
            int LowerValue = Int32.Parse(Preferences.Get("LowerValue", ""));
            int UpperValue = Int32.Parse(Preferences.Get("UpperValue", ""));
            if (key == "empty")
            {
                var postsByPrice = await postService.PostsByPrice(LowerValue, UpperValue);

                
                Posts = new ObservableCollection<Post>(postsByPrice);
            }
            else
            {
                var postsByPrice = await postService.PostsByPrice(LowerValue, UpperValue);
                
                var posts = postsByPrice.Where(a => a.User.adress.PLZ == key).ToList();
                Posts = new ObservableCollection<Post>(posts);
                 
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

        public DelegateCommand Back { get; set; }

        public PostsViewModel()
        {

            LoadPosts();
            Back = new DelegateCommand(BackM);
        }
        private async void BackM()
        {
            await Shell.Current.GoToAsync("..");
        }
        private void LoadPosts()
        {

            Posts = HomeViewModel.PtsList;
        }

    }
}
