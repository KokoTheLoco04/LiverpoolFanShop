
namespace LiverpoolFanShop.Core.Models.Product
{
    using System.ComponentModel.DataAnnotations.Schema;
    using LiverpoolFanShop.Core.Models.Category;
    using LiverpoolFanShop.Infrastructure.Data.Models;

    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AmountInStock { get; set; }

        public ProductCategoryModel Category { get; set; } = new ProductCategoryModel();
    }
}
