using NUnit.Framework;
using Moq;
using LiverpoolFanShop.Core.Services;
using LiverpoolFanShop.Core.Models.Admin.User;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Infrastructure.Data;

namespace LiverpoolFanShop.Tests.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private LiverpoolFanShopDbContext context = null!;
        private Repository repository = null!;
        private UserService userService = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<LiverpoolFanShopDbContext>()
                .UseInMemoryDatabase(databaseName: "LiverpoolFanShopTestDb")
                .Options;

            context = new LiverpoolFanShopDbContext(options);

            repository = new Repository(context);
            userService = new UserService(repository);
        }

        [Test]
        public async Task AllAsync_ShouldReturnListOfUserServiceModel()
        {
            var users = new[]
            {
                new ApplicationUser { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new ApplicationUser { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            var result = await userService.AllAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().FullName, Is.EqualTo("John Doe"));
            Assert.That(result.First().Email, Is.EqualTo("john.doe@example.com"));
        }

        [Test]
        public async Task UserFullNameAsync_ShouldReturnFullName_WhenUserExists()
        {
            var user = new ApplicationUser
            {
                Id = "userId",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var result = await userService.UserFullNameAsync("userId");

            Assert.That(result, Is.EqualTo("John Doe"));
        }

        [Test]
        public async Task UserFullNameAsync_ShouldReturnEmptyString_WhenUserDoesNotExist()
        {
            var result = await userService.UserFullNameAsync("invalidUserId");

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
