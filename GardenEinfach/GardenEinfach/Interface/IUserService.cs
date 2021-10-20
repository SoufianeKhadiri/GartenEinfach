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
        //MyUser GetUserInfo(List<MyUser> users, string email);
        Task<List<MyUser>> GetAllUsers();
        Task AddUser(MyUser user);
        Task<string> CreateUserFirebaseAuth(string email, string password);
        ObservableCollection<MyUser> getUsers();

        Task<MyUser> GetUsr(string email);

        Task UpdateUserFoto(string email, string imageUrl, MyUser user);

        MyUser GetUserPreferences();
        Task<string> UploadUserImage(Stream stream, string Titel, string database);
        string SetUserImage(string gender);
    }
}
