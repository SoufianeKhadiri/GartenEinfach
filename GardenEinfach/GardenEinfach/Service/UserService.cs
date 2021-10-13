using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using GardenEinfach.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenEinfach.Service
{
    public class UserService : IUserService
    {
        public static FirebaseClient client;
        public static FirebaseStorage storage;
        public UserService()
        {
            client = new FirebaseClient("https://gardenservice-ec613-default-rtdb.firebaseio.com");
            storage = new FirebaseStorage("gardenservice-ec613.appspot.com");
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
        public async Task AddUser(MyUser user)
        {

            await client.Child("Users").PostAsync(user);

        }
        public ObservableCollection<MyUser> getUsers()
        {

            var UserData = client.Child("Users").AsObservable<MyUser>()
                                                .AsObservableCollection();

            return UserData;
        }
    }
}
