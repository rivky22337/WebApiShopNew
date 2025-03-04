using Entities.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{

    public class UserRepositoryIntegrationTest
    {
        private readonly DataBaseFixture _DBFixture;

        public UserRepositoryIntegrationTest()
        {
            _DBFixture = new DataBaseFixture();
        }

        [Fact]
        public async Task CreateUser_Should_Add_User_To_Database()
        {
            // Arrange
            var logger = NullLogger<UserRepository>.Instance;

            var repository = new UserRepository(_DBFixture.Context,logger);

            var user = new User { FirstName = "aa", LastName = "bb", UserName = "Chana@new", Password = "Rzfdsxf!@2" };
            var DbUser = await repository.AddUserAsync(user);

            // Assert
            Assert.NotNull(DbUser);
            Assert.NotEqual(0, DbUser.UserId);
            Assert.Equal("Chana@new", DbUser.UserName);
            _DBFixture.Dispose();
        }
    }
}
