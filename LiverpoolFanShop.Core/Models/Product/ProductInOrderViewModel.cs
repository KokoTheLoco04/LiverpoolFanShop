
namespace LiverpoolFanShop.Core.Models.Product
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using LiverpoolFanShop.Infrastructure.Data.Models;
    public class ProductInOrderViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}
