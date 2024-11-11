using LiverpoolFanShop.Core.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Contracts
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryModel>> AllCategoriesAsync();
    }
}
