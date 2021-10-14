using GardenEinfach.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GardenEinfach.Service
{
    public interface IUserService
    {
        MyUser GetUserInfo(List<MyUser> users, string email);
        Task<List<MyUser>> GetAllUsers();
        Task AddUser(MyUser user);
        Task<string> CreateUserFirebaseAuth(string email, string password);
        ObservableCollection<MyUser> getUsers();
    }
}
