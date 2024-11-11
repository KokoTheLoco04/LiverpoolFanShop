using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        public Task<IEnumerable<ProductCategoryModel>> AllCategoriesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
