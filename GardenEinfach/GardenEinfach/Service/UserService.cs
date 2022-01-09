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
        public static FirebaseAuthProvider authProvider;
        public string WebApiKey;
        public UserService()
        {
            client = new FirebaseClient("https://gardenservice-ec613-default-rtdb.firebaseio.com");
            storage = new FirebaseStorage("gardenservice-ec613.appspot.com");
            WebApiKey = "AIzaSyD_rzOMR_cTLm9JYCo1WylNJqXOc56rTvI";
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
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
            return users.Where(u => u.Email == email).FirstOrDefault();

        }

        public async Task AddUser(MyUser user)
        {

            await client.Child("Users").PostAsync(user);

        }

        async Task<string> IUserService.CreateUserFirebaseAuth(string email, string password)
        {
            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            var token = auth.FirebaseToken;

            return token;
        }
        async Task<string> IUserService.loginUser(string email, string password)
        {
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            var token = auth.FirebaseToken;

            return token;
        }

        public ObservableCollection<MyUser> getUsers()
        {

            var UserData = client.Child("Users").AsObservable<MyUser>()
                                                .AsObservableCollection();

            return UserData;
        }

        
        public async Task<string> UpdateUserInfo(MyUser newuser, string email)
        {

            var toUpdateUser = (await client
                 .Child("Users")
                 .OnceAsync<MyUser>()).Where(a => a.Object.Email == email).FirstOrDefault();

            await client
             .Child("Users")
             .Child(toUpdateUser.Key)
             .PutAsync(newuser);

            return newuser.Image;
        }

        public MyUser CreateUser(string firstName, string lastName, string email, string phone,
                                 string gender, string image, string street, string city, string houseNumber,string plz)
        {
            MyUser usr = new MyUser();
            Adress adress = new Adress();
            usr.FirstName = firstName;
            usr.LastName = lastName;
            usr.Email = email;
            usr.Phone = phone;
            usr.Gender = gender;
            usr.Image = image;
            adress.Street = street;
            adress.City = city;
            adress.HouseNumber = houseNumber;
            adress.PLZ = plz;
            usr.adress = adress;

            return usr;
        }
        public string SetUserImage(string gender)
        {
            string userImage = "";

            if (gender == "Male")
            {
                userImage = "https://firebasestorage.googleapis.com/v0/b/gardenservice-ec613.appspot.com/o/UsersImages%2Fman.png?alt=media&token=334c1458-9f1c-495a-9043-12d8fe43cd97";
            }
            else if (gender == "Female")
            {
                userImage = "https://firebasestorage.googleapis.com/v0/b/gardenservice-ec613.appspot.com/o/UsersImages%2Fwomen.png?alt=media&token=148e8911-9a3d-4804-8206-5480ba60f52c";
            }
            else if (gender == "Other")
            {
                userImage = "https://firebasestorage.googleapis.com/v0/b/gardenservice-ec613.appspot.com/o/UsersImages%2Fother.png?alt=media&token=8d059a66-1394-4467-bcbc-fc13d8493727";
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
