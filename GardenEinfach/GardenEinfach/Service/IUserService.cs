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
        Task<MyUser> GetUserInfo(string email);
        Task<List<MyUser>> GetAllUsers();
        Task AddUser(MyUser user);

        ObservableCollection<MyUser> getUsers();
    }
}
