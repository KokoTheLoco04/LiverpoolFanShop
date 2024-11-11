using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Core.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Services
{
    public class ProductService : IProductService
    {
        public Task DecreaseProductAmountAsync(int productId, int amount)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DoesProductExistByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AllProductsFilteredAndPagedServiceModel> GetAllProductsAsync(AllProductsQueryModel queryModel)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDetailsViewModel> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
