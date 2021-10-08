
using GardenEinfach.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    [QueryProperty(nameof(Key), nameof(Key))]
    public class PostDetailViewModel : BaseViewModel
    {
        #region Post props



        private string _Key;
        public string Key
        {
            get { return _Key; }
            set
            {
                SetProperty(ref _Key, value);
                LoadItemId(value);
            }
        }



        private string _Username;
        public string Username
        {
            get { return _Username; }
            set { SetProperty(ref _Username, value); }
        }


        private string _Adress;
        public string Adress
        {
            get { return _Adress; }
            set { SetProperty(ref _Adress, value); }
        }

        private string _Titel;
        public string Titel
        {
            get { return _Titel; }
            set
            {
                _Titel = value;


            }
        }

        private string _Price;
        public string Price
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


        private string _Category;
        public string Category
        {
            get { return _Category; }
            set { SetProperty(ref _Category, value); }
        }


        private string _Time;
        public string Time
        {
            get { return _Time; }
            set { SetProperty(ref _Time, value); }
        }


        private List<string> _Images;
        public List<string> Images
        {
            get { return _Images; }
            set { SetProperty(ref _Images, value); }
        }


        #endregion
        public Post p;
        //ctor
        public PostDetailViewModel()
        {
            p = new Post();

        }


        public async void LoadItemId(string key)
        {

            try
            {

                var AllPosts = await dataStore.GetItemsAsync();
                var post = await Task.FromResult(AllPosts.FirstOrDefault(p => p.Key == key));
                p = post;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                FillPostData(p);
            }

        }

        private void FillPostData(Post PostDetail)
        {
            Images = new List<string>();
            Images = PostDetail.Images;
            Description = PostDetail.Description;
            Titel = PostDetail.Titel;
            Price = PostDetail.Price.ToString();
            //Username = PostDetail.User.FirstName.ToString();
        }
    }
}
