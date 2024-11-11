using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Category;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IRepository repository;

        public ProductCategoryService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<IEnumerable<ProductCategoryModel>> AllCategoriesAsync()
        {
            var categories = await repository.AllReadOnly<Category>()
                .Select(c => new ProductCategoryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return categories;
        }
    }
}
