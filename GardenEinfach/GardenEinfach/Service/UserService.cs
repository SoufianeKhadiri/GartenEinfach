using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using GardenEinfach.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GardenEinfach.Service
{
    public class UserService : IUserService
    {
        public static FirebaseClient client;
        public static FirebaseStorage storage;
        public string WebApiKey;
        public UserService()
        {
            client = new FirebaseClient("https://gardenservice-ec613-default-rtdb.firebaseio.com");
            storage = new FirebaseStorage("gardenservice-ec613.appspot.com");
            WebApiKey = "AIzaSyD_rzOMR_cTLm9JYCo1WylNJqXOc56rTvI";
        }
        public async Task<List<MyUser>> GetAllUsers()
        {
            return (await client
               .Child("Users")
               .OnceAsync<MyUser>()).Select(item => new MyUser
               {
                   FirstName = item.Object.FirstName,
                   LastName = item.Object.LastName,
                   Email = item.Object.Email,
                   adress = item.Object.adress,
                   Phone = item.Object.Phone,
                   Gender = item.Object.Gender,
                   Key = item.Key

               }).ToList();
        }
        public async Task<MyUser> GetUsr(string email)
        {
            var users = await GetAllUsers();

            await client
             .Child("Users")
             .OnceAsync<MyUser>();

            return users.Where(u => u.Email == email).FirstOrDefault();
            //.Child("Users").Child("Email").EqualTo(email)
            //.OnceAsync<MyUser>());
        }


        //public MyUser GetUserInfo(List<MyUser> users, string email)
        //{
        //    //MyUser myUser = new MyUser();

        //    //var user = users.Where(x => x.Email == email);
        //    //foreach (var item in user)
        //    //{
        //    //    myUser = item;
        //    //}
        //    //return myUser;
        //}
        //public async Task<List<MyUser>> getUsersInfo()
        //{
        //    return (await client
        //      .Child("Users")
        //      .OnceAsync<MyUser>()).Select(item => new MyUser
        //      {
        //          FirstName = item.Object.FirstName,
        //          LastName = item.Object.LastName,
        //          Email = item.Object.Email,
        //          adress = item.Object.adress,
        //          Phone = item.Object.Phone,
        //          Key = item.Key

        //      }).ToList();
        //}

        public async Task AddUser(MyUser user)
        {

            await client.Child("Users").PostAsync(user);

        }

        async Task<string> IUserService.CreateUserFirebaseAuth(string email, string password)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            var token = auth.FirebaseToken;

            return token;
        }

        public ObservableCollection<MyUser> getUsers()
        {

            var UserData = client.Child("Users").AsObservable<MyUser>()
                                                .AsObservableCollection();

            return UserData;
        }

        //Task<string> CreateUserFirebaseAuth(string email, string password)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
