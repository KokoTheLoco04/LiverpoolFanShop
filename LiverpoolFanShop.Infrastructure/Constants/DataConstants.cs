using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Infrastructure.Constants
{
    public static class DataConstants
    {
        //Product
        public const int ProductNameMinLength = 15;
        public const int ProductNameMaxLength = 50;

        public const int DescriptionMinLength = 15;
        public const int DescriptionMaxLength = 100;

        public const string ProductPriceMinimum = "0";
        public const string ProductPriceMaximum = "300000";

        //Category
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 20;


        //ApplicationUser
        public const int FirstNameMinLength = 2;
        public const int FirstNameMaxLength = 15;

        public const int LastNameMinLength = 3;
        public const int LastNameMaxLength = 18;

        //Order
        public const int AddressMinLength = 20;
        public const int AddressMaxLength = 100;
    }
}
