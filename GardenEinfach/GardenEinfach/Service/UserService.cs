using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using GardenEinfach.Model;
using Newtonsoft.Json;
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
                   Key = item.Key,
                   Image = item.Object.Image,


               }).ToList();
        }
        public async Task<MyUser> GetUsr(string email)
        {
            var users = await GetAllUsers();

            await client
             .Child("Users")
             .OnceAsync<MyUser>();

            return users.Where(u => u.Email == email).FirstOrDefault();

        }


        public async Task AddUser(MyUser user)
        {

            await client.Child("Users").PostAsync(user);

        }

        public async Task UpdateUserFoto(string email, string imageUrl, MyUser user)
        {


            var toUpdateUser = (await client
                .Child("Users")
                .OnceAsync<MyUser>()).Where(a => a.Object.Email == email).FirstOrDefault();

            await client
             .Child("Users")
             .Child(toUpdateUser.Key)
             .PutAsync(user);

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

        public MyUser GetUserPreferences()
        {
            MyUser usr = new MyUser();
            Adress adress = new Adress();

            adress.Street = Preferences.Get("Street", "");
            adress.City = Preferences.Get("City", "");
            adress.HouseNumber = Preferences.Get("HouseNumber", "");
            usr.FirstName = Preferences.Get("FirstName", "");
            usr.LastName = Preferences.Get("LastName", "");
            usr.Email = Preferences.Get("Email", "");
            usr.Phone = Preferences.Get("Phone", "");
            usr.FullyAdress = Preferences.Get("Adress", "");
            usr.Gender = Preferences.Get("Gender", "");
            usr.adress = adress;
            usr.Image = Preferences.Get("UserImage", "");
            return usr;
        }

        public string SetUserImage(string gender)
        {
            string userImage = "";

            if (gender == "Male")
            {
                userImage = "man";
            }
            else if (gender == "Female")
            {
                userImage = "women";
            }
            else if (gender == "Other")
            {
                userImage = "other";
            }
            else
            {
                userImage = "other";
            }

            return userImage;
        }


        public async Task<string> UploadUserImage(Stream stream, string Titel, string database)
        {

            var storageImage = await storage.Child(database).Child(Titel).PutAsync(stream);
            string imgUrl = storageImage;
            return imgUrl;
        }
    }
}
