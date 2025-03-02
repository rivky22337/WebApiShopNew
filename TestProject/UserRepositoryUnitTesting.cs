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

namespace TestProject
{
    public class UserRepositoryUnitTesting
    {

        [Fact]
        public async Task LoginTest()
        {
            var user = new User { UserName = "cz6778685@gmail.com", Password = "aaaAAA!!!12" };
            var mockContext = new Mock<MyShopContext>();
            var users = new List<User> { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var logger = new Mock<ILogger<UserRepository>>();

            var userRepository = new UserRepository(mockContext.Object, logger.Object);

            var result = await userRepository.Login(user.Password, user.UserName);

            Assert.Equal(user, result);
        }

    }

}
