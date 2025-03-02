//using Entities;
using DTO;
using Entities.Models;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        //User GetUserById(int id);
        Task<User> Login(string Password,string UserName);
        Task<User> UpdateUserAsync(int id, User userToUpdate);
    }
}