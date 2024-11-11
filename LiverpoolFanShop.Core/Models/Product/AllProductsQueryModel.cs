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
        public int ProductsPerPage = 8;

        public string Category { get; init; } = null!;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; } = null!;

        public ProductSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalProductsCount { get; set; }

        public IEnumerable<ProductCategoryModel> Categories { get; set; } = null!;

        public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
