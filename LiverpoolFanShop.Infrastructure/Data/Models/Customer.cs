using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LiverpoolFanShop.Infrastructure.Constants.DataConstants;

namespace LiverpoolFanShop.Infrastructure.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(NameMaxLength)]
        public string Email { get; set; } = string.Empty;

        //[Required]
        //[MaxLength(PasswordMaxLength)]
        //public string Password { get; set; } = string.Empty;

        public IEnumerable<Order> Orders { get; set; } = new HashSet<Order>();
        public ShoppingCart ShoppingCart { get; set; } = null!;
    }
}
