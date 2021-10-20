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

        Task<string> UploadImage(Stream stream, string imageNumber, string Titel, string database);


    }
}
