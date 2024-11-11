
namespace LiverpoolFanShop.Core.Models.Product
{
    using System.ComponentModel.DataAnnotations.Schema;
    using LiverpoolFanShop.Infrastructure.Data.Models;

    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AmountInStock { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
