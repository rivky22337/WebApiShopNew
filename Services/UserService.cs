using Repositories;
using Entities.Models;
using System.Text.Json;
using Zxcvbn;
using DTO;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //public User GetUserById(int id)
        //{
        //    return _userRepository.GetUserById(id);
        //}

        public async Task<User> Login(string Password, string UserName)
        {
            return await _userRepository.Login(Password,UserName);
        }

        public async Task<User> UpdateUser(int id, User userToUpdate)
        {
            int passwordScore = CheckPassword(userToUpdate.Password);
            if (userToUpdate.UserName == "" || userToUpdate.FirstName == "" || userToUpdate.LastName == "" || passwordScore < 2)
            {
                return null;
            }
            return await _userRepository.UpdateUserAsync(id, userToUpdate);
        }

        public int CheckPassword(string password)
        {
            if (password == null || password == "")
            {
                return -1;
            }
            var result = Zxcvbn.Core.EvaluatePassword(password);

            return result.Score;
        }

        //public async Task<User> AddUser(User user)
        //{
        //    int passwordScore = CheckPassword(user.Password);
        //    try
        //    {
        //        ValidateUser(user, passwordScore);
        //        return await _userRepository.AddUserAsync(user);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        // Handle the validation exception and return appropriate message
        //        throw new Exception($"User validation failed: {ex.Message}");
        //    }
        //}
        public async Task<User> AddUser(User user)
        {
            int passwordScore = CheckPassword(user.Password);
            if (user.UserName == "" || user.FirstName == "" || user.LastName == "" || passwordScore < 2)
            {
                return null;
            }
            return await _userRepository.AddUserAsync(user);
        }

        //private void ValidateUser(User user, int passwordScore)
        //{
        //    if (string.IsNullOrEmpty(user.UserName))
        //    {
        //        throw new ValidationException("Username is required");
        //    }

        //    if (string.IsNullOrEmpty(user.FirstName))
        //    {
        //        throw new ValidationException("First name is required");
        //    }

        //    if (string.IsNullOrEmpty(user.LastName))
        //    {
        //        throw new ValidationException("Last name is required");
        //    }

        //    if (passwordScore < 2)
        //    {
        //        throw new ValidationException("Password score is too low");
        //    }

        //    if (!IsValidEmail(user.Email))
        //    {
        //        throw new ValidationException("Invalid email address");
        //    }
        //}
    }
}

