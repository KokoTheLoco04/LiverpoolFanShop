using LiverpoolFanShop.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Models.ServiceModels
{
    public class AllProductsFilteredAndPagedServiceModel
    {
        public int TotalProducts { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();
    }
}
