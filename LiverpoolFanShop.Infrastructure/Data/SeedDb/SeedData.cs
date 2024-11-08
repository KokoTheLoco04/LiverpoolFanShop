using LiverpoolFanShop.Infrastructure.Data.Models;
using static LiverpoolFanShop.Infrastructure.Constants.CustomClaims;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Infrastructure.Data.SeedDb
{
    public class SeedData
    {
        public ApplicationUser CustomerUser { get; set; }

        public IdentityUserClaim<string> CustomerUserClaim { get; set; }
        public IdentityUserClaim<string> GuestUserClaim { get; set; }
        public IdentityUserClaim<string> AdminUserClaim { get; set; }

        public ApplicationUser GuestUser { get; set; }

        public ApplicationUser AdminUser { get; set; }

        public Customer Customer { get; set; }

        public Customer AdminCustomer { get; set; }

        public Category JerseyCategory { get; set; }

        public Category TrainingGearCategory { get; set; }

        public Category AccessoriesCategory { get; set; }

        public Category EquipmentCategory   { get; set; }

        public Category HomewareCategory { get; set; }

        public Category CollectionsCategory { get; set; }
        public Product Jersey { get; set; }

        public Product Scarf { get; set; }

        public Product Jacket { get; set; }

        public Product Cup {  get; set; }

        public Product Bottle { get; set; }

        public Product BadgeSet { get; set; }

        public ShoppingCart CustomerShoppingCart { get; set; }
        public ShoppingCart AdminShoppingCart { get; set; }

        public SeedData()
        {
            SeedUsers();
            SeedCategories();
            SeedProducts();
            SeedShoppingCart();
            SeedCustomer();
        }

        private void SeedCategories()
        {
            JerseyCategory = new Category
            {
                Id = 1,
                Name = "Jerseys"
            };

            TrainingGearCategory = new Category
            {
                Id = 2,
                Name = "Training Gear"
            };

            AccessoriesCategory = new Category
            {
                Id = 3,
                Name = "Accessories"
            };

            EquipmentCategory = new Category
            {
                Id = 4,
                Name = "Equipment"
            };

            HomewareCategory = new Category
            {
                Id = 5,
                Name = "Homeware"
            };

            CollectionsCategory = new Category
            {
                Id = 6,
                Name = "Collections"
            };
        }

        private void SeedProducts()
        {
            Jersey = new Product
            {
                Id = 1,
                Name = "2024/2025 Home Jersey",
                Description = "Official Liverpool FC 2024/2025 Home Jersey with iconic red color.",
                Price = 89.99M,
                AmountInStock = 50,
                ImageUrl = "https://cdn.fifakitcreator.com/kits/2024/06/25/667af874ed5a6.jpg",
                isDeleted = false,
                CategoryId = 1  // Jerseys
            };
            Scarf = new Product
            {
                Id = 2,
                Name = "Liverpool Scarf",
                Description = "Liverpool FC scarf with the club's logo and colors.",
                Price = 19.99M,
                AmountInStock = 100,
                ImageUrl = "https://flagman.ie/flags/wp-content/uploads/2021/12/Liverpool-FC-Official-Scarf.jpg",
                isDeleted = false,
                CategoryId = 3  // Accessories
            };

            Jacket = new Product
            {
                Id = 3,
                Name = "Liverpool FC Training Jacket",
                Description = "Liverpool FC training jacket, lightweight and perfect for training.",
                Price = 59.99M,
                AmountInStock = 30,
                ImageUrl = "https://media.karousell.com/media/photos/products/2021/11/16/liverpool_windbreaker_1637027553_ed4210a9_progressive.jpg",
                isDeleted = false,
                CategoryId = 2  // Training Gear
            };

            Cup = new Product
            {
                Id = 4,
                Name = "Liverpool FC Cup",
                Description = "Liverpool FC Cup with golden badge.",
                Price = 15.99M,
                AmountInStock = 120,
                ImageUrl = "https://d3j2s6hdd6a7rg.cloudfront.net/v2/uploads/media/default/0002/28/9aaab721b8477e69bc4d8bf0ccba51b812ac1b0f.jpeg",
                isDeleted = false,
                CategoryId = 5  // Homeware
            };

            Bottle = new Product
            {
                Id = 5,
                Name = "Liverpool Thermo Bottle",
                Description = "Liverpool FC thermo bottle with the club's logo.",
                Price = 9.99M,
                AmountInStock = 75,
                ImageUrl = "https://m.media-amazon.com/images/I/41PYj9U8ShL._AC_SL1024_.jpg",
                isDeleted = false,
                CategoryId = 4  // Equipment
            };

            BadgeSet = new Product
            {
                Id = 6,
                Name = "Liverpool FC Crest Badge Set",
                Description = "Liverpool FC crest badge set with all the badges from the history of the club.",
                Price = 119.99M,
                AmountInStock = 15,
                ImageUrl = "https://store.liverpoolfc.com/media/catalog/product/cache/6e0c7b53c0ed72fe014b8d12b60d479c/a/2/a21bd03_4_1.jpg",
                isDeleted = false,
                CategoryId = 6  // Collections
            };
        }

        private void SeedShoppingCart()
        {
            CustomerShoppingCart = new ShoppingCart
            {
                Id = 1,
                UserId = CustomerUser.Id
            };

            AdminShoppingCart = new ShoppingCart
            {
                Id = 2,
                UserId = AdminUser.Id
            };
        }

        private void SeedCustomer()
        {
            Customer = new Customer()
            {
                Id = 1,
                UserId = CustomerUser.Id,
                ShoppingCartId = CustomerShoppingCart.Id
            };

            AdminCustomer = new Customer()
            {
                Id = 3,
                UserId = AdminUser.Id,
                ShoppingCartId = AdminShoppingCart.Id
            };
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            CustomerUser = new ApplicationUser()
            {
                Id = "6c5e5ff5-d61f-419d-8e0c-290b05e27e87",
                UserName = "customer@mail.com",
                NormalizedUserName = "customer@mail.com",
                Email = "customer@mail.com",
                NormalizedEmail = "customer@mail.com",
                FirstName = "Customer",
                LastName = "Customerov"
            };

            CustomerUserClaim = new IdentityUserClaim<string>()
            {
                Id = 1,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Customer Customerov",
                UserId = "6c5e5ff5-d61f-419d-8e0c-290b05e27e87"
            };

            CustomerUser.PasswordHash =
                 hasher.HashPassword(CustomerUser, "customer123");

            GuestUser = new ApplicationUser()
            {
                Id = "d8b8354a-d93e-4887-8390-3ba136739184",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com",
                FirstName = "Guest",
                LastName = "Guestov"
            };

            GuestUserClaim = new IdentityUserClaim<string>()
            {
                Id = 2,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Guest Guestov",
                UserId = "d8b8354a-d93e-4887-8390-3ba136739184"
            };

            GuestUser.PasswordHash =
            hasher.HashPassword(CustomerUser, "guest123");

            AdminUser = new ApplicationUser()
            {
                Id = "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                FirstName = "Great",
                LastName = "Admin"
            };

            AdminUserClaim = new IdentityUserClaim<string>()
            {
                Id = 3,
                ClaimType = UserFullNameClaim,
                UserId = "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944",
                ClaimValue = "Great Admin"
            };

            AdminUser.PasswordHash =
            hasher.HashPassword(AdminUser, "admin123");
        }

    }
}
