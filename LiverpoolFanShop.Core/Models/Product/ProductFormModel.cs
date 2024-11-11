using System.ComponentModel.DataAnnotations;
using LiverpoolFanShop.Core.Models.Category;
using static LiverpoolFanShop.Infrastructure.Constants.DataConstants;

namespace LiverpoolFanShop.Core.Models.Product
{
    public class ProductFormModel
    {
        [Required]
        [StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int AmountInStock { get; set; }

        [Required]
        [Range(typeof(decimal),
            ProductPriceMinimum,
            ProductPriceMaximum,
            ConvertValueInInvariantCulture = true,
            ErrorMessage = "Price must be a positive number and less than {2}€")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<ProductCategoryModel> Categories { get; set; } =
            new List<ProductCategoryModel>();
    }
}
