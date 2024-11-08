using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiverpoolFanShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataAndRolesSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ShoppingCarts_ShoppingCartId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_Products_ProductId",
                table: "ShoppingCartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6c5e5ff5-d61f-419d-8e0c-290b05e27e87", 0, "a2470cbc-5cea-4911-a665-a8c9f2c2aaa0", "customer@mail.com", false, "Customer", "Customerov", false, null, "customer@mail.com", "customer@mail.com", "AQAAAAIAAYagAAAAEE5dNC1jlCqrYR3goDsdC4AmeB+mXnUmxmgluH0TRvkNHLo2YEWu5CbhwrkD5KAhOg==", null, false, "5f1ad4d4-59d6-45d3-9eb9-95302c0d7327", false, "customer@mail.com" },
                    { "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944", 0, "168a495e-7a25-4485-9f7e-9a11b24ec37d", "admin@mail.com", false, "Great", "Admin", false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAIAAYagAAAAEKWrmbUvZ+ZO8Zmtsf/Lg8n80IcNHxQ1WeZztAcubMu7NF7DRH+S3O11jiPQJTwelA==", null, false, "2e22696a-b72d-40ac-bf4f-9f7c76823dbe", false, "admin@mail.com" },
                    { "d8b8354a-d93e-4887-8390-3ba136739184", 0, "028c6307-3dae-49ba-9c9e-4204884cd0e0", "guest@mail.com", false, "Guest", "Guestov", false, null, "guest@mail.com", "guest@mail.com", "AQAAAAIAAYagAAAAEJVDIl86eFNwWnsi8ew/5rEGKaXYx24U0gOad4r0bs1W5gs2wU7b+pKHN/Ch+hV0tg==", null, false, "63d764d7-a2c0-4a6f-a233-a7df72468499", false, "guest@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Jerseys" },
                    { 2, "Training Gear" },
                    { 3, "Accessories" },
                    { 4, "Equipment" },
                    { 5, "Homeware" },
                    { 6, "Collections" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "user:fullname", "Customer Customerov", "6c5e5ff5-d61f-419d-8e0c-290b05e27e87" },
                    { 2, "user:fullname", "Guest Guestov", "d8b8354a-d93e-4887-8390-3ba136739184" },
                    { 3, "user:fullname", "Great Admin", "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AmountInStock", "CategoryId", "Description", "ImageUrl", "Name", "Price", "isDeleted" },
                values: new object[,]
                {
                    { 1, 50, 1, "Official Liverpool FC 2024/2025 Home Jersey with iconic red color.", "https://cdn.fifakitcreator.com/kits/2024/06/25/667af874ed5a6.jpg", "2024/2025 Home Jersey", 89.99m, false },
                    { 2, 100, 3, "Liverpool FC scarf with the club's logo and colors.", "https://flagman.ie/flags/wp-content/uploads/2021/12/Liverpool-FC-Official-Scarf.jpg", "Liverpool Scarf", 19.99m, false },
                    { 3, 30, 2, "Liverpool FC training jacket, lightweight and perfect for training.", "https://media.karousell.com/media/photos/products/2021/11/16/liverpool_windbreaker_1637027553_ed4210a9_progressive.jpg", "Liverpool FC Training Jacket", 59.99m, false },
                    { 4, 120, 5, "Liverpool FC Cup with golden badge.", "https://d3j2s6hdd6a7rg.cloudfront.net/v2/uploads/media/default/0002/28/9aaab721b8477e69bc4d8bf0ccba51b812ac1b0f.jpeg", "Liverpool FC Cup", 15.99m, false },
                    { 5, 75, 4, "Liverpool FC thermo bottle with the club's logo.", "https://m.media-amazon.com/images/I/41PYj9U8ShL._AC_SL1024_.jpg", "Liverpool Thermo Bottle", 9.99m, false },
                    { 6, 15, 6, "Liverpool FC crest badge set with all the badges from the history of the club.", "https://store.liverpoolfc.com/media/catalog/product/cache/6e0c7b53c0ed72fe014b8d12b60d479c/a/2/a21bd03_4_1.jpg", "Liverpool FC Crest Badge Set", 119.99m, false }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, "6c5e5ff5-d61f-419d-8e0c-290b05e27e87" },
                    { 2, "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "ShoppingCartId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "6c5e5ff5-d61f-419d-8e0c-290b05e27e87" },
                    { 3, 2, "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ShoppingCarts_ShoppingCartId",
                table: "Customers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_Products_ProductId",
                table: "ShoppingCartProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartProducts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ShoppingCarts_ShoppingCartId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_Products_ProductId",
                table: "ShoppingCartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartProducts");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d8b8354a-d93e-4887-8390-3ba136739184");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6c5e5ff5-d61f-419d-8e0c-290b05e27e87");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ShoppingCarts_ShoppingCartId",
                table: "Customers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_Products_ProductId",
                table: "ShoppingCartProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartProducts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
