using LiverpoolFanShop.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string Address { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
