using GardenEinfach.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class PostsViewModel : BaseViewModel
    {
        private ObservableCollection<Post> _Posts;
        public ObservableCollection<Post> Posts
        {
            get { return _Posts; }
            set { SetProperty(ref _Posts, value); }
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
