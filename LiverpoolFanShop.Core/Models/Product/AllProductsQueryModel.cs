using LiverpoolFanShop.Core.Enumerations;
using LiverpoolFanShop.Core.Models.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Models.Product
{
    public class AllProductsQueryModel
    {
        public string? SearchTerm { get; set; }

        public int? CategoryId { get; set; }

        public string? Category { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int ProductsPerPage { get; set; } = 3;

        public int TotalProductsCount { get; set; }

        public ProductSorting Sorting { get; init; }
        public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

    }
}
