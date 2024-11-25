using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Enumerations;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Core.Services;
using LiverpoolFanShop.Infrastructure.Data;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;



namespace LiverpoolFanShop.Tests.Services.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private LiverpoolFanShopDbContext dbContext = null!;
        private Repository repository = null!;
        private ProductService productService = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<LiverpoolFanShopDbContext>()
        .UseInMemoryDatabase("TestDatabase")
        .Options;

            dbContext = new LiverpoolFanShopDbContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Products.RemoveRange(dbContext.Products);
            dbContext.Categories.RemoveRange(dbContext.Categories);

            dbContext.SaveChanges();

            repository = new Repository(dbContext);
            productService = new ProductService(repository);
        }


        [Test]
        public async Task DecreaseProductAmountAsync_ShouldDecreaseStockCorrectly()
        {
            var category = new Category
            {
                Id = 1,
                Name = "Test Category"
            };

            var product = new Product
            {
                Id = 1,
                Name = "Sample Product",
                Description = "Test Description",
                Price = 20.00M,
                AmountInStock = 50,
                ImageUrl = "https://example.com/image.jpg",
                CategoryId = category.Id,
                Category = category
            };

            dbContext.Categories.Add(category);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            int amountToDecrease = 43;

            await productService.DecreaseProductAmountAsync(product.Id, amountToDecrease);

            await dbContext.Entry(product).ReloadAsync();

            var updatedProduct = await repository.GetByIdAsync<Product>(product.Id);
            Assert.That(updatedProduct, Is.Not.Null);
            Assert.That(updatedProduct.AmountInStock, Is.EqualTo(7));
        }

        [Test]
        public async Task DoesProductExistByIdAsync_ShouldReturnTrueWhenProductExists()
        {
            var category = new Category
            {
                Id = 1,
                Name = "Test Category"
            };

            var product = new Product
            {
                Id = 1,
                Name = "Sample Product",
                Description = "Test Description",
                Price = 20.00M,
                AmountInStock = 50,
                ImageUrl = "https://example.com/image.jpg",
                CategoryId = category.Id,
                Category = category
            };

            dbContext.Categories.Add(category);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            var productExists = await productService.DoesProductExistByIdAsync(1);

            Assert.That(productExists, Is.True);
        }


        [Test]
        public async Task DoesProductExistByIdAsync_ShouldReturnFalseWhenProductDoesNotExist()
        {
            var productExists = await productService.DoesProductExistByIdAsync(999);

            Assert.That(productExists, Is.False);
        }

        [Test]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            var category = new Category
            {
                Id = 1,
                Name = "Test Category"
            };

            var product = new Product
            {
                Id = 1,
                Name = "Sample Product",
                Description = "Test Description",
                Price = 20.00M,
                AmountInStock = 50,
                ImageUrl = "https://example.com/image.jpg",
                CategoryId = category.Id,
                Category = category
            };

            dbContext.Categories.Add(category);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            var result = await productService.GetProductByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Sample Product"));
        }

        [Test]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            var result = await productService.GetProductByIdAsync(999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnFilteredBySearchTerm()
        {
            var category = new Category { Id = 1, Name = "Test Category" };
            dbContext.Categories.Add(category);

            var product1 = new Product
            {
                Id = 1,
                Name = "Product One",
                Description = "Description One",
                Price = 100,
                CategoryId = category.Id,
                Category = category
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Product Two",
                Description = "Description Two",
                Price = 150,
                CategoryId = category.Id,
                Category = category
            };

            dbContext.Products.AddRange(product1, product2);
            await dbContext.SaveChangesAsync();

            var queryModel = new AllProductsQueryModel { SearchTerm = "One", CurrentPage = 1, ProductsPerPage = 10 };
            var result = await productService.GetAllProductsAsync(queryModel);

            var productList = result.Products.ToList();

            Assert.That(result.TotalProducts, Is.EqualTo(1));
            Assert.That(result.Products.Count, Is.EqualTo(1));
            Assert.That(productList[0].Name, Is.EqualTo("Product One"));
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnFilteredByCategory()
        {
            var category1 = new Category { Id = 1, Name = "Category One" };
            var category2 = new Category { Id = 2, Name = "Category Two" };
            dbContext.Categories.AddRange(category1, category2);

            var product1 = new Product
            {
                Id = 1,
                Name = "Product One",
                Description = "Description One",
                Price = 100,
                CategoryId = category1.Id,
                Category = category1
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Product Two",
                Description = "Description Two",
                Price = 150,
                CategoryId = category2.Id,
                Category = category2
            };

            dbContext.Products.AddRange(product1, product2);
            await dbContext.SaveChangesAsync();

            var queryModel = new AllProductsQueryModel { CategoryId = 1, CurrentPage = 1, ProductsPerPage = 10 };
            var result = await productService.GetAllProductsAsync(queryModel);

            var productList = result.Products.ToList();

            Assert.That(result.TotalProducts, Is.EqualTo(1));
            Assert.That(result.Products.Count, Is.EqualTo(1));
            Assert.That(productList[0].Name, Is.EqualTo("Product One"));
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnSortedByPriceAscending()
        {
            var category = new Category { Id = 1, Name = "Test Category" };
            dbContext.Categories.Add(category);

            var product1 = new Product
            {
                Id = 1,
                Name = "Product One",
                Description = "Description One",
                Price = 100,
                CategoryId = category.Id,
                Category = category
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Product Two",
                Description = "Description Two",
                Price = 50,
                CategoryId = category.Id,
                Category = category
            };

            dbContext.Products.AddRange(product1, product2);
            await dbContext.SaveChangesAsync();

            var queryModel = new AllProductsQueryModel { Sorting = ProductSorting.PriceAscending, CurrentPage = 1, ProductsPerPage = 10 };
            var result = await productService.GetAllProductsAsync(queryModel);

            var productList = result.Products.ToList();

            Assert.That(productList[0].Price, Is.EqualTo(50));
            Assert.That(productList[1].Price, Is.EqualTo(100));
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnSortedByNameDescending()
        {
            var category = new Category { Id = 1, Name = "Test Category" };
            dbContext.Categories.Add(category);

            var product1 = new Product
            {
                Id = 1,
                Name = "Apple",
                Description = "Description One",
                Price = 100,
                CategoryId = category.Id,
                Category = category
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Banana",
                Description = "Description Two",
                Price = 150,
                CategoryId = category.Id,
                Category = category
            };

            dbContext.Products.AddRange(product1, product2);
            await dbContext.SaveChangesAsync();

            var queryModel = new AllProductsQueryModel { Sorting = ProductSorting.NameDescending, CurrentPage = 1, ProductsPerPage = 10 };
            var result = await productService.GetAllProductsAsync(queryModel);

            var productList = result.Products.ToList();

            Assert.That(productList.Count, Is.GreaterThanOrEqualTo(2));

            Assert.That(productList[0].Name, Is.EqualTo("Banana"));
            Assert.That(productList[1].Name, Is.EqualTo("Apple"));
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldPaginateCorrectly()
        {
            var category = new Category { Id = 1, Name = "Test Category" };
            dbContext.Categories.Add(category);

            var products = new List<Product>();
            for (int i = 1; i <= 15; i++)
            {
                products.Add(new Product
                {
                    Id = i,
                    Name = $"Product {i}",
                    Description = $"Description {i}",
                    Price = 100 + i,
                    CategoryId = category.Id,
                    Category = category
                });
            }
            dbContext.Products.AddRange(products);
            await dbContext.SaveChangesAsync();

            var queryModel = new AllProductsQueryModel { CurrentPage = 2, ProductsPerPage = 5 };
            var result = await productService.GetAllProductsAsync(queryModel);

            var productList = result.Products.ToList();

            Assert.That(result.TotalProducts, Is.EqualTo(15));
            Assert.That(result.Products.Count, Is.EqualTo(5));
            Assert.That(productList[0].Name, Is.EqualTo("Product 6"));
            Assert.That(productList[4].Name, Is.EqualTo("Product 10"));
        }

        [Test]
        public async Task GetProductsByCategoryAsync_ShouldReturnCorrectProductsForCategory()
        {
            var category1 = new Category { Id = 1, Name = "Electronics" };
            var category2 = new Category { Id = 2, Name = "Clothing" };

            dbContext.Categories.AddRange(category1, category2);

            var product1 = new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 1000,
                ImageUrl = "https://example.com/laptop.jpg",
                CategoryId = category1.Id, 
                Category = category1
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "T-shirt",
                Price = 20,
                ImageUrl = "https://example.com/tshirt.jpg",
                CategoryId = category2.Id,
                Category = category2
            };

            dbContext.Products.AddRange(product1, product2);
            await dbContext.SaveChangesAsync();

            var result = await productService.GetProductsByCategoryAsync(1);

            Assert.That(result.Count(), Is.EqualTo(1)); 
            Assert.That(result.First().Name, Is.EqualTo("Laptop")); 
            Assert.That(result.First().Price, Is.EqualTo(1000));
            Assert.That(result.First().ImageUrl, Is.EqualTo("https://example.com/laptop.jpg"));
        }

        [Test]
        public async Task GetProductsByCategoryAsync_ShouldReturnEmptyWhenNoProductsInCategory()
        {
            var result = await productService.GetProductsByCategoryAsync(999);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task DoesProductExistByNameAsync_ShouldReturnTrueWhenProductExists()
        {
            var category = new Category { Id = 1, Name = "Test Category" };
            dbContext.Categories.Add(category);

            var product = new Product
            {
                Id = 1,
                Name = "Banana",
                Price = 10.00M,
                ImageUrl = "https://example.com/banana.jpg",
                CategoryId = category.Id,
                Category = category
            };

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            var result = await productService.DoesProductExistByNameAsync("Banana");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DoesProductExistByNameAsync_ShouldReturnFalseWhenProductDoesNotExist()
        {
            var result = await productService.DoesProductExistByNameAsync("NonExistentProduct");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DoesProductExistByNameAsync_ShouldReturnTrueWhenProductExistsIgnoringCase()
        {
            var category = new Category { Id = 1, Name = "Test Category" };
            dbContext.Categories.Add(category);

            var product = new Product
            {
                Id = 1,
                Name = "Banana",
                Price = 10.00M,
                ImageUrl = "https://example.com/banana.jpg",
                CategoryId = category.Id,
                Category = category
            };

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            var result = await productService.DoesProductExistByNameAsync("banana");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GetAllProductsEditAsync_ShouldReturnCorrectProductDetailsViewModel()
        {
            var category1 = new Category { Id = 1, Name = "Category 1" };
            var category2 = new Category { Id = 2, Name = "Category 2" };

            dbContext.Categories.Add(category1);
            dbContext.Categories.Add(category2);

            var product1 = new Product
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description of Product 1",
                Price = 10.00M,
                AmountInStock = 5,
                ImageUrl = "https://example.com/product1.jpg",
                CategoryId = category1.Id,
                Category = category1
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Product 2",
                Description = "Description of Product 2",
                Price = 20.00M,
                AmountInStock = 10,
                ImageUrl = "https://example.com/product2.jpg",
                CategoryId = category2.Id,
                Category = category2
            };

            dbContext.Products.Add(product1);
            dbContext.Products.Add(product2);

            await dbContext.SaveChangesAsync();

            var result = await productService.GetAllProductsEditAsync();


            Assert.That(result, Has.Count.EqualTo(2));

            var firstProduct = result.FirstOrDefault(p => p.Id == 1);
            Assert.That(firstProduct, Is.Not.Null, "Product 1 not found");
            Assert.That(firstProduct.Id, Is.EqualTo(1));
            Assert.That(firstProduct.Name, Is.EqualTo("Product 1"));
            Assert.That(firstProduct.Description, Is.EqualTo("Description of Product 1"));
            Assert.That(firstProduct.Price, Is.EqualTo(10.00M));
            Assert.That(firstProduct.AmountInStock, Is.EqualTo(5));
            Assert.That(firstProduct.ImageUrl, Is.EqualTo("https://example.com/product1.jpg"));
            Assert.That(firstProduct.Category.Id, Is.EqualTo(1));
            Assert.That(firstProduct.Category.Name, Is.EqualTo("Category 1"));

            var secondProduct = result.FirstOrDefault(p => p.Id == 2);
            Assert.That(secondProduct, Is.Not.Null, "Product 2 not found");
            Assert.That(secondProduct.Id, Is.EqualTo(2));
            Assert.That(secondProduct.Name, Is.EqualTo("Product 2"));
            Assert.That(secondProduct.Description, Is.EqualTo("Description of Product 2"));
            Assert.That(secondProduct.Price, Is.EqualTo(20.00M));
            Assert.That(secondProduct.AmountInStock, Is.EqualTo(10));
            Assert.That(secondProduct.ImageUrl, Is.EqualTo("https://example.com/product2.jpg"));
            Assert.That(secondProduct.Category.Id, Is.EqualTo(2));
            Assert.That(secondProduct.Category.Name, Is.EqualTo("Category 2"));
        }

        [Test]
        public async Task UpdateProductAsync_ShouldReturnTrue_WhenProductExists()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Old Product Name",
                Description = "Old description",
                ImageUrl = "https://oldimageurl.com",
                Price = 20.00m,
                AmountInStock = 10,
                CategoryId = 1
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            var model = new ProductFormModel
            {
                Name = "Updated Product Name",
                Description = "Updated description",
                ImageUrl = "https://updatedimageurl.com",
                Price = 25.00m,
                AmountInStock = 50,
                CategoryId = 2
            };

            var result = await productService.UpdateProductAsync(product.Id, model);

            Assert.That(result, Is.True);

            var updatedProduct = await dbContext.Products.FindAsync(product.Id);
            Assert.That(updatedProduct.Name, Is.EqualTo("Updated Product Name"));
            Assert.That(updatedProduct.Description, Is.EqualTo("Updated description"));
            Assert.That(updatedProduct.ImageUrl, Is.EqualTo("https://updatedimageurl.com"));
            Assert.That(updatedProduct.Price, Is.EqualTo(25.00m));
            Assert.That(updatedProduct.AmountInStock, Is.EqualTo(50));
            Assert.That(updatedProduct.CategoryId, Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateProductAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            var model = new ProductFormModel
            {
                Name = "Updated Product Name",
                Description = "Updated description",
                ImageUrl = "https://updatedimageurl.com",
                Price = 25.00m,
                AmountInStock = 50,
                CategoryId = 2
            };

            var result = await productService.UpdateProductAsync(999, model);

            Assert.That(result, Is.False);

            var productCount = await dbContext.Products.CountAsync();
            Assert.That(productCount, Is.EqualTo(0));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
