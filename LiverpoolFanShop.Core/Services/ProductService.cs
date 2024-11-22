using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Category;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Core.Models.ServiceModels;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LiverpoolFanShop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository repository;

        public ProductService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task DecreaseProductAmountAsync(int productId, int amount)
        {
            var product = await repository
                .All<Product>()
                .FirstOrDefaultAsync(p => p.Id == productId); 
            
            if (product != null)
            {
                product.AmountInStock -= amount; 
                
                if (product.AmountInStock < 0)
                {
                    product.AmountInStock = 0; 
                } 
                
                await repository.SaveChangesAsync(); 
            }
            
        }

        public async Task<bool> DoesProductExistByIdAsync(int id)
        {
            return await repository.AllReadOnly<Product>().AnyAsync(p => p.Id == id);
        }

        public async Task<AllProductsFilteredAndPagedServiceModel> GetAllProductsAsync(AllProductsQueryModel queryModel)
        {
            var productQuery = repository.AllReadOnly<Product>(); 

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm)) 
            { 
                productQuery = productQuery.Where(p => p.Name.Contains(queryModel.SearchTerm) 
                                                    || p.Description.Contains(queryModel.SearchTerm)); 
            }
            
            if (!string.IsNullOrWhiteSpace(queryModel.Category)) 
            { 
                productQuery = productQuery.Where(p => p.Category.Name == queryModel.Category); 
            }
            
            var totalProducts = await productQuery.CountAsync(); 
            var products = await productQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
                .Take(queryModel.ProductsPerPage)
                .ToListAsync(); 
            
            return new AllProductsFilteredAndPagedServiceModel 
            { 
                TotalProducts = totalProducts, 
                Products = products.Select(p => new ProductViewModel 
                    {  
                        Id = p.Id, 
                        Name = p.Name, 
                        Price = p.Price, 
                        ImageUrl = p.ImageUrl 
                })
                .ToList() };
        }

        public async Task<ProductDetailsViewModel?> GetProductByIdAsync(int id)
        {
            var product = await repository.AllReadOnly<Product>()
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsViewModel
                { 
                    Id = p.Id, 
                    Name = p.Name, 
                    Description = p.Description, 
                    Price = p.Price, 
                    ImageUrl = p.ImageUrl,
                    AmountInStock = p.AmountInStock,
                    Category = new ProductCategoryModel
                    {
                        Id = p.Category.Id,
                        Name = p.Category.Name
                    }
                })
                .FirstOrDefaultAsync();

            return product ?? null;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await repository.AllReadOnly<Product>()
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductViewModel 
                { 
                    Id = p.Id, 
                    Name = p.Name, 
                    Price = p.Price, 
                    ImageUrl = p.ImageUrl 
                })
                .ToListAsync(); 
            
            return products;
        }

        public async Task<bool> DoesProductExistByNameAsync(string name)
        {
            return await repository.AllReadOnly<Product>()
                .AnyAsync(p => p.Name.ToLower() == name.ToLower());
        }
    }
}
