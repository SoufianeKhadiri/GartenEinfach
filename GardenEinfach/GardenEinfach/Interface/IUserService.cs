using GardenEinfach.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GardenEinfach.Service
{
    public interface IUserService
    {

        Task<List<MyUser>> GetAllUsers();
        Task AddUser(MyUser user);
        Task<string> CreateUserFirebaseAuth(string email, string password);
        Task<string> loginUser(string email, string password);
        ObservableCollection<MyUser> getUsers();

        Task<MyUser> GetUsr(string email);

        Task<string> UpdateUserInfo(MyUser newuser, string email);

        //MyUser GetUserPreferences();

        //void SetUserPreferences(MyUser newUsrInfo);
        Task<string> UploadUserImage(Stream stream, string Titel, string database);
        string SetUserImage(string gender);

        MyUser CreateUser(string firstName, string lastName, string email, string phone,
                                  string gender, string image, string street, string city, string houseNumber);


    }
}
