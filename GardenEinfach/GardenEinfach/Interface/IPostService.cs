using GardenEinfach.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GardenEinfach.Service
{
    public interface IPostService<T>
    {
        Task AddItemAsync(T item);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(string id);
        T GetItemAsync(List<T> items, string id);
        Task<List<T>> GetItemsAsync();
        Task<Post> GetPostById(string id);
        Task<List<Post>> GetMyPosts();
        Task<List<Post>> PostsByPlz( string plz);
        Task<List<Post>> PostsByPrice(int minPrice, int maxPrice);
        Task<string> UploadImage(Stream stream, string imageNumber, string Titel, string database);


    }
}
