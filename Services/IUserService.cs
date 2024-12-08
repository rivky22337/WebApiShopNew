using Entities.Models;

namespace Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        int CheckPassword(string password);
        //User GetUserById(int id);
        User Login(string userName, string password);
        Task<User> UpdateUser(int id, User userToUpdate);
    }
}