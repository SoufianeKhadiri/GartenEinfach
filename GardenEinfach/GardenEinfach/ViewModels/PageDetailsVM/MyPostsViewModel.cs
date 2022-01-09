using GardenEinfach.Model;
using GardenEinfach.Views.PageDetails;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.CommunityToolkit.Extensions;
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
        private DelegateCommand<Post> _EditCommand;
        public DelegateCommand<Post> EditCommand =>
            _EditCommand ?? (_EditCommand = new DelegateCommand<Post>(ExecuteEditCommand));

        async void ExecuteEditCommand(Post post)
        {
            await Shell.Current
                           .GoToAsync($"{nameof(PostEditxaml)}?{nameof(PostEditxamlViewModel.Key)}={post.Key}");
           

        }
        private DelegateCommand<Post> _DeleteCommand;
        public DelegateCommand<Post> DeleteCommand =>
            _DeleteCommand ?? (_DeleteCommand = new DelegateCommand<Post>(ExecuteDeleteCommand));

        async void ExecuteDeleteCommand(Post post)
        {
            await postService.DeleteItemAsync(post.Key);
            var posts = await postService.GetMyPosts();
            MyPosts = new ObservableCollection<Post>(posts);
            await Application.Current.MainPage.DisplayToastAsync("Post is deleted!", 1500);

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
