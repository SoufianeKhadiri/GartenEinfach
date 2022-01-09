using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using GardenEinfach.Model;
using GardenEinfach.Services;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GardenEinfach.Service
{
    public class PostService : IPostService<Post>
    {
        public static FirebaseClient client;
        public static FirebaseStorage storage;
        public List<Post> Posts;
        public PostService()
        {

            client = new FirebaseClient("https://gardenservice-ec613-default-rtdb.firebaseio.com");
            storage = new FirebaseStorage("gardenservice-ec613.appspot.com");

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
                  User = item.Object.User,
                  Key = item.Key
                  



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

        public async Task<Post> GetPostById(string id)
        {

            var allPosts = await GetAllPosts();
            await client
              .Child("Posts")
              .OnceAsync<Post>();
            return allPosts.Where(a => a.Key == id).FirstOrDefault();
        }


        public async Task<List<Post>> PostsByPlz( string plz)
        {

            var allPosts = await GetAllPosts();
            await client
              .Child("Posts")
              .OnceAsync<Post>();
            
            return allPosts.Where(a => a.User.adress.PLZ == plz).ToList();
        }

        public async Task<List<Post>> GetMyPosts()
        {
           
            var allPosts = await GetAllPosts();
            await client
              .Child("Posts")
              .OnceAsync<Post>();

            return allPosts.Where(a => a.User.Key == Preferences.Get("userId", "")).ToList();
        }

        public async Task<List<Post>> PostsByPrice( int  minPrice , int maxPrice)
        {

            var allPosts = await GetAllPosts();
            await client
              .Child("Posts")
              .OnceAsync<Post>();

            return allPosts.Where(a => a.Price > minPrice && a.Price < maxPrice).ToList();
        }



        public async Task AddItemAsync(Post item)
        {
            await client.Child("Posts").PostAsync(item);

        }

        public async Task DeleteItemAsync(string id)
        {
            await client
              .Child("Posts/"+id).DeleteAsync();
        }

        public Post GetItemAsync(List<Post> items, string id)
        {
            Post Post = new Post();
            var post = items.Where(x => x.Titel == id);
            foreach (var item in post)
            {
                Post = item;
            }
            return Post;
        }

        public async Task<List<Post>> GetItemsAsync()
        {
            return (await client
              .Child("Posts")
              .OnceAsync<Post>()).Select(item => new Post
              {
                  Images = item.Object.Images,
                  Titel = item.Object.Titel,
                  Description = item.Object.Description,
                  Price = item.Object.Price,
                  User = item.Object.User,
                  Key = item.Key

              }).ToList();
        }

        public async Task UpdateItemAsync(Post item)
        {
            await client
              .Child("Posts/" + item.Key).PutAsync(item);
        }
    }
}

