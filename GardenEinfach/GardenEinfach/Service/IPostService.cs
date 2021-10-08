using GardenEinfach.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GardenEinfach.Service
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPosts();
        Task<Post> GetPostAsync(List<Post> Posts, string titel);
        Task AddPost(Post post);
        Task<List<MyUser>> GetAllUsers();

        Task<MyUser> GetUserInfo(string email);

        Task AddUser(MyUser user);
        Task<string> UploadImage(Stream stream, string imageNumber, string Titel, string database);


    }
}
