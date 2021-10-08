using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using GardenEinfach.Model;
using GardenEinfach.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenEinfach.Service
{
    public class PostService : IPostService
    {
        public static FirebaseClient client;
        public static FirebaseStorage storage;
        public List<Post> Posts;
        public PostService()
        {

            client = new FirebaseClient("https://gardenservice-ec613-default-rtdb.firebaseio.com");
            storage = new FirebaseStorage("gardenservice-ec613.appspot.com");
            //LoadAllPosts();
        }

        private async void LoadAllPosts()
        {
            Posts = await GetAllPosts();
            Posts = new List<Post>();
        }

        public async Task AddPost(Post post)
        {

            await client.Child("Posts").PostAsync(post);

        }

        public async Task AddUser(MyUser user)
        {

            await client.Child("Users").PostAsync(user);

        }

        public async Task<string> UploadImage(Stream stream, string imageNumber, string Titel, string database)
        {

            var storageImage = await storage.Child(database).Child(Titel).Child(imageNumber).PutAsync(stream);
            string imgUrl = storageImage;
            return imgUrl;
        }

        public async Task<Post> GetPostAsync(List<Post> Posts, string titel)
        {

            return await Task.FromResult(Posts.FirstOrDefault(s => s.Titel == titel));
        }
        //public ObservableCollection<Post> getPosts()
        //{

        //    var postData = client.Child("Posts").AsObservable<Post>()
        //                                        .AsObservableCollection();

        //    return postData;

        //}

        public async Task<List<Post>> GetAllPosts()
        {

            return (await client
              .Child("Posts")
              .OnceAsync<Post>()).Select(item => new Post
              {
                  Images = item.Object.Images,
                  Titel = item.Object.Titel,
                  Description = item.Object.Description,
                  Price = item.Object.Price,
                  //User = item.Object.User



              }).ToList();
        }


        public async Task<List<MyUser>> GetAllUsers()
        {

            return (await client
              .Child("Users")
              .OnceAsync<MyUser>()).Select(item => new MyUser
              {
                  Email = item.Object.Email,
                  FirstName = item.Object.FirstName

              }).ToList();
        }


        public async Task<MyUser> GetUserInfo(string email)
        {

            var allUsers = await GetAllUsers();
            await client
              .Child("Users")
              .OnceAsync<MyUser>();
            return allUsers.Where(a => a.Email == email).FirstOrDefault();
        }


        //public ObservableCollection<MyUser> getUsers()
        //{

        //    var UserData = client.Child("Users").AsObservable<MyUser>()
        //                                        .AsObservableCollection();

        //    return UserData;
        //}
    }
}
