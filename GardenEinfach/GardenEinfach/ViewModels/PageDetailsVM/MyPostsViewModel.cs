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
