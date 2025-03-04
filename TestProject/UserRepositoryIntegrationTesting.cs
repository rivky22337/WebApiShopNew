using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Repositories;
using TestProject;
using Xunit;

namespace IntegrationTests
{
    public class UserRepositoryTests : IClassFixture<DataBaseFixture>
    {
        private readonly MyShopContext _context;
        private readonly UserRepository _userRepository;


        public UserRepositoryTests(DataBaseFixture fixture)
        {
            _context = fixture.Context;
            _userRepository = new UserRepository(_context, null);
        }
        [Fact]
        public async Task CreateUser_Should_Add_User_To_Database()
        {
            // Arrange
            var user = new User
            {
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Password = "password"
            };

            // Act
            var result = await _userRepository.AddUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.UserId); // Ensure the UserId is not 0
            var dbUser = await _context.Users.FindAsync(result.UserId);
            Assert.NotNull(dbUser);
            Assert.Equal(user.UserName, dbUser.UserName);
        }
        [Fact]
        public async Task AddUserAsync_ShouldAddUser_WhenUserIsValid()
        {
            // Arrange
            var user = new User { UserName = "testUser", FirstName = "Test", LastName = "User", Password = "testPassword" };

            // Act
            var result = await _userRepository.AddUserAsync(user);

            // Assert
            var addedUser = await _context.Users.FindAsync(result.UserId);
            Assert.NotNull(addedUser);
            Assert.Equal("testUser", addedUser.UserName);
        }

        [Fact]
        public async Task Login_ShouldReturnUser_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new User { UserName = "loginUser", FirstName = "Log", LastName = "In", Password = "loginPassword" };
            await _userRepository.AddUserAsync(user);

            // Act
            var loggedInUser = await _userRepository.Login("loginPassword", "loginUser");

            // Assert
            Assert.NotNull(loggedInUser);
            Assert.Equal("loginUser", loggedInUser.UserName);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Act
            var loggedInUser = await _userRepository.Login("wrongPassword", "nonExistentUser");

            // Assert
            Assert.Null(loggedInUser);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdateUser_WhenUserIsValid()
        {
            // Arrange
            var user = new User { UserName = "updateUser", FirstName = "Update", LastName = "User", Password = "oldPassword" };
            var addedUser = await _userRepository.AddUserAsync(user);

            var userToUpdate = new User { UserId = addedUser.UserId, UserName = "updatedUser", FirstName = "Updated", LastName = "User", Password = "newPassword" };

            // Act
            var updatedUser = await _userRepository.UpdateUserAsync(addedUser.UserId, userToUpdate);

            // Assert
            Assert.NotNull(updatedUser);
            Assert.Equal("updatedUser", updatedUser.UserName);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userToUpdate = new User { UserId = 999, UserName = "nonExistentUser", FirstName = "Non", LastName = "Existent", Password = "password" };

            // Act
            var result = await _userRepository.UpdateUserAsync(999, userToUpdate);

            // Assert
            Assert.Null(result);
        }
    }
}