using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using GardenEinfach.Model;
using GardenEinfach.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GardenEinfach.Services
{

    //public class MockDataStore : IPostService<Post>
    //{
    //    public static FirebaseClient client;
    //    public static FirebaseStorage storage;
    //    public MockDataStore()
    //    {
    //        client = new FirebaseClient("https://gardenservice-ec613-default-rtdb.firebaseio.com");
    //        storage = new FirebaseStorage("gardenservice-ec613.appspot.com");
    //    }
    //    public async Task AddItemAsync(Post item)
    //    {
    //        await client.Child("Posts").PostAsync(item);

    //    }

    //    public Task DeleteItemAsync(string id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Post GetItemAsync(List<Post> items, string id)
    //    {
    //        Post Post = new Post();
    //        var post = items.Where(x => x.Titel == id);
    //        foreach (var item in post)
    //        {
    //            Post = item;
    //        }
    //        return Post;
    //    }

    //    public async Task<List<Post>> GetItemsAsync()
    //    {
    //        return (await client
    //          .Child("Posts")
    //          .OnceAsync<Post>()).Select(item => new Post
    //          {
    //              Images = item.Object.Images,
    //              Titel = item.Object.Titel,
    //              Description = item.Object.Description,
    //              Price = item.Object.Price,
    //              User = item.Object.User,
    //              Key = item.Key

    //          }).ToList();
    //    }

    //    public Task UpdateItemAsync(Post item)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}