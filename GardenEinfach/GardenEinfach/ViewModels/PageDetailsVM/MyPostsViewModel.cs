using GardenEinfach.Model;
using GardenEinfach.Views.PageDetails;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class MyPostsViewModel : BaseViewModel
    {
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

        private DelegateCommand _Back;
        public DelegateCommand Back =>
        _Back ?? (_Back = new DelegateCommand(BackM));

        async void BackM()
        {
            await Shell.Current.GoToAsync("..");
        }



        private ObservableCollection<Post> _MyPosts;
        public ObservableCollection<Post> MyPosts
        {
            get { return _MyPosts; }
            set { SetProperty(ref _MyPosts, value); }
        }
        public MyPostsViewModel()
        {
            LoadPosts();
        }

        private void LoadPosts()
        {

            MyPosts = HomeViewModel.PtsList;
        }
    }
}
