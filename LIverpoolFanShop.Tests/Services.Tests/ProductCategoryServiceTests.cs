using LiverpoolFanShop.Core.Services;
using LiverpoolFanShop.Core.Models.Category;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiverpoolFanShop.Infrastructure.Data;

namespace LiverpoolFanShop.Tests.Services.Tests
{
    [TestFixture]
    public class ProductCategoryServiceTests
    {
        private LiverpoolFanShopDbContext context = null!;
        private Repository repository = null!;
        private ProductCategoryService productCategoryService = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<LiverpoolFanShopDbContext>()
                .UseInMemoryDatabase(databaseName: "LiverpoolFanShopTestDb")
                .Options;

            context = new LiverpoolFanShopDbContext(options);
            repository = new Repository(context);
            productCategoryService = new ProductCategoryService(repository);
        }


        [Test]
        public async Task AllCategoriesAsync_ShouldReturnAllCategories()
        {
            var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Clothing" },
            new Category { Id = 2, Name = "Accessories" },
            new Category { Id = 3, Name = "Equipment" }
        };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            var result = await productCategoryService.AllCategoriesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(3));

            var resultList = result.ToList();
            Assert.That(resultList[0].Name, Is.EqualTo("Clothing"));
            Assert.That(resultList[1].Name, Is.EqualTo("Accessories"));
            Assert.That(resultList[2].Name, Is.EqualTo("Equipment"));
        }

        [Test]
        public async Task AllCategoriesAsync_ShouldReturnEmptyList_WhenNoCategoriesExist()
        {
            var result = await productCategoryService.AllCategoriesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}

