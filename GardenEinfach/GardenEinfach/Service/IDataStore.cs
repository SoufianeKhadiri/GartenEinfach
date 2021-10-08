using GardenEinfach.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GardenEinfach.Services
{
    public interface IDataStore<T>
    {

        Task AddItemAsync(T item);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(string id);
        T GetItemAsync(List<T> items, string id);
        Task<List<T>> GetItemsAsync();

    }
}
