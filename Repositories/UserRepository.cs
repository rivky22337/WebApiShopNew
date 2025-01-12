//using Entities;
using DTO;
using Entities.Models;
using System.Runtime.InteropServices;
using System.Text.Json;
namespace Repositories

{
    public class UserRepository : IUserRepository
    {
        MyShopContext _context;
        public UserRepository(MyShopContext context)
        {
            _context = context; 
        }
        //public User GetUserById(int id)
        //{
        //    using (StreamReader reader = System.IO.File.OpenText(filePath))
        //    {
        //        string? currentUserInFile;
        //        while ((currentUserInFile = reader.ReadLine()) != null)
        //        {
        //            User user = JsonSerializer.Deserialize<User>(currentUserInFile);
        //            if (user.UserId == id)
        //                return user;
        //        }
        //    }
        //    return null;
        //}


        public async Task<User> AddUserAsync(User user)
        {

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public User Login(LoginUserDTO loginUserDTO)
        {

            User user = _context.Users.FirstOrDefault(u =>  u.UserName == loginUserDTO.UserName && u.Password == loginUserDTO.Password );
           if(user == null) 
                return null;
            return user;

        }

        public async Task<User> UpdateUserAsync(int id, User userToUpdate)
        {

            if(userToUpdate == null)
            {
                return null;
            }
           _context.Update(userToUpdate);
            await _context.SaveChangesAsync();
            return userToUpdate;
        }
    }
}
