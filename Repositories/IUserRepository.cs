//using Entities;
using DTO;
using Entities.Models;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        //User GetUserById(int id);
        User Login(LoginUserDTO loginUserDTO);
        Task<User> UpdateUserAsync(int id, User userToUpdate);
    }
}