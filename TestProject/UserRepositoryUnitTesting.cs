using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Entities.Models;
using Repositories;
using Moq.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Logging;
using DTO;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TestProject
{
    public class UserRepositoryUnitTesting
    {
        Mock<ILogger<UserRepository>> logger = new Mock<ILogger<UserRepository>>();


        [Fact]
        public async Task ValidLoginTest()
        {
            var user = new User { UserName = "cz6778685@gmail.com", Password = "aaaAAA!!!12" };
            var mockContext = new Mock<MyShopContext>();
            var users = new List<User> { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object, logger.Object);

            var result = await userRepository.Login(user.Password, user.UserName);

            Assert.Equal(user, result);
        }

        [Fact]
        public async void InvaidUserNameLoginTest()
        {
            var user = new User { FirstName = "c", LastName = "c", UserName = "C@gmail.com", Password = "aaaAAA!!!456" };
            var users = new List<User>() { user };
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object,logger.Object);

            var result = await userRepository.Login("aaa@gmail.com", user.Password);

            Assert.Null(result);
        }
        [Fact]
        public async void InvaidPasswordLoginTest()
        {
            var user = new User { FirstName = "c", LastName = "c", UserName = "C@gmail.com", Password = "aaaAAA!!!456" };
            var users = new List<User>() { user };
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object, logger.Object);

            var result = await userRepository.Login(user.UserName, "pass");

            Assert.Null(result);
        }
        [Fact]
        public async void UpdatingExistingUserTest()
        {
            var user = new User { FirstName = "c", LastName = "c", UserName = "C@gmail.com", Password = "aaaAAA!!!456" };
            var users = new List<User>() { user };
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object, logger.Object);

            var result = await userRepository.Login(user.UserName, "pass");

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_ExistingUser_UpdatesUser()
        {
            var user = new User { UserId = 20, FirstName = "nnn", LastName = "bbb" ,UserName="aaa@gmail.com",Password="aaaAAA!!!123"};
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>() { user });
            mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            mockContext.Setup(x => x.Users.FindAsync(20)).ReturnsAsync(user);

            var userRepository = new UserRepository(mockContext.Object,logger.Object);
            var updatedUser = new User { FirstName = "updated", LastName = "user" };

            user = await userRepository.UpdateUserAsync(20, updatedUser);

            Assert.Equal("updated", user.FirstName);
            Assert.Equal("user", user.LastName);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

    }

}
