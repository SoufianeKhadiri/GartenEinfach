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
                   Email = item.Object.Email,
                   FirstName = item.Object.FirstName

               }).ToList();
        }

        public MyUser GetUserInfo(List<MyUser> users, string email)
        {
            MyUser myUser = new MyUser();

            var user = users.Where(x => x.Email == email);
            foreach (var item in user)
            {
                myUser = item;
            }
            return myUser;
        }

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
