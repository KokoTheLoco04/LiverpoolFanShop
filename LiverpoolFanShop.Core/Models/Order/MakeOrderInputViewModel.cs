using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LiverpoolFanShop.Infrastructure.Constants.DataConstants;

namespace LiverpoolFanShop.Core.Models.Order
{
    public class MakeOrderInputViewModel
    {
        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string ShoppingCartId { get; set; } = string.Empty;
    }
}
