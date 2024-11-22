using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Core.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Contracts
{
    public interface IProductService
    {
        Task<AllProductsFilteredAndPagedServiceModel> GetAllProductsAsync(AllProductsQueryModel queryModel);

        Task<ProductDetailsViewModel?> GetProductByIdAsync(int id);

        Task<bool> DoesProductExistByIdAsync(int id);

        Task DecreaseProductAmountAsync(int productId, int amount);

        Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId);
        Task<bool> DoesProductExistByNameAsync(string name);
    }
}
