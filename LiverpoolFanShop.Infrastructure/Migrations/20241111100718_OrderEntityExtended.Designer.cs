﻿// <auto-generated />
using System;
using LiverpoolFanShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LiverpoolFanShop.Infrastructure.Migrations
{
    [DbContext(typeof(LiverpoolFanShopDbContext))]
    [Migration("20241111100718_OrderEntityExtended")]
    partial class OrderEntityExtended
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "6c5e5ff5-d61f-419d-8e0c-290b05e27e87",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "dcc552a0-b2ce-42e2-8760-2d3bd608b62c",
                            Email = "customer@mail.com",
                            EmailConfirmed = false,
                            FirstName = "Customer",
                            LastName = "Customerov",
                            LockoutEnabled = false,
                            NormalizedEmail = "customer@mail.com",
                            NormalizedUserName = "customer@mail.com",
                            PasswordHash = "AQAAAAIAAYagAAAAEKEsiHq5TYjDKUo2MHFgkrldskAPjuz9jKucORM52GSHKNpLEtuNCNKdTDeppfXYpg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e9510738-a2e7-4fff-a0bc-d88822a4eda4",
                            TwoFactorEnabled = false,
                            UserName = "customer@mail.com"
                        },
                        new
                        {
                            Id = "d8b8354a-d93e-4887-8390-3ba136739184",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fc9239a2-aaec-497a-8f7d-6999feb6119a",
                            Email = "guest@mail.com",
                            EmailConfirmed = false,
                            FirstName = "Guest",
                            LastName = "Guestov",
                            LockoutEnabled = false,
                            NormalizedEmail = "guest@mail.com",
                            NormalizedUserName = "guest@mail.com",
                            PasswordHash = "AQAAAAIAAYagAAAAEA3ZK3YB00Cl9h7jQzVAoBhzw5xQ76dtv271FGAcBDFbLKIdAceY1KUGqPY9XYIGsA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0b6e10df-e35c-4c8c-b2fe-5ed7e8e26ca5",
                            TwoFactorEnabled = false,
                            UserName = "guest@mail.com"
                        },
                        new
                        {
                            Id = "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "97336bed-f030-49d0-a611-8f4c89de066a",
                            Email = "admin@mail.com",
                            EmailConfirmed = false,
                            FirstName = "Great",
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MAIL.COM",
                            NormalizedUserName = "ADMIN@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEMwWy2ZEvCqIoGyUhH7rlKPhHgMdt/STq8KVjQkoYIe/K5acTwAXOCeVDzDylONVIw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ab1fc217-18c4-409e-99af-123b89eef0d4",
                            TwoFactorEnabled = false,
                            UserName = "admin@mail.com"
                        });
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Jerseys"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Training Gear"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Accessories"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Equipment"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Homeware"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Collections"
                        });
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ShoppingCartId = 1,
                            UserId = "6c5e5ff5-d61f-419d-8e0c-290b05e27e87"
                        },
                        new
                        {
                            Id = 3,
                            ShoppingCartId = 2,
                            UserId = "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944"
                        });
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AmountInStock")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AmountInStock = 50,
                            CategoryId = 1,
                            Description = "Official Liverpool FC 2024/2025 Home Jersey with iconic red color.",
                            ImageUrl = "https://cdn.fifakitcreator.com/kits/2024/06/25/667af874ed5a6.jpg",
                            Name = "2024/2025 Home Jersey",
                            Price = 89.99m,
                            isDeleted = false
                        },
                        new
                        {
                            Id = 2,
                            AmountInStock = 100,
                            CategoryId = 3,
                            Description = "Liverpool FC scarf with the club's logo and colors.",
                            ImageUrl = "https://flagman.ie/flags/wp-content/uploads/2021/12/Liverpool-FC-Official-Scarf.jpg",
                            Name = "Liverpool Scarf",
                            Price = 19.99m,
                            isDeleted = false
                        },
                        new
                        {
                            Id = 3,
                            AmountInStock = 30,
                            CategoryId = 2,
                            Description = "Liverpool FC training jacket, lightweight and perfect for training.",
                            ImageUrl = "https://media.karousell.com/media/photos/products/2021/11/16/liverpool_windbreaker_1637027553_ed4210a9_progressive.jpg",
                            Name = "Liverpool FC Training Jacket",
                            Price = 59.99m,
                            isDeleted = false
                        },
                        new
                        {
                            Id = 4,
                            AmountInStock = 120,
                            CategoryId = 5,
                            Description = "Liverpool FC Cup with golden badge.",
                            ImageUrl = "https://d3j2s6hdd6a7rg.cloudfront.net/v2/uploads/media/default/0002/28/9aaab721b8477e69bc4d8bf0ccba51b812ac1b0f.jpeg",
                            Name = "Liverpool FC Cup",
                            Price = 15.99m,
                            isDeleted = false
                        },
                        new
                        {
                            Id = 5,
                            AmountInStock = 75,
                            CategoryId = 4,
                            Description = "Liverpool FC thermo bottle with the club's logo.",
                            ImageUrl = "https://m.media-amazon.com/images/I/41PYj9U8ShL._AC_SL1024_.jpg",
                            Name = "Liverpool Thermo Bottle",
                            Price = 9.99m,
                            isDeleted = false
                        },
                        new
                        {
                            Id = 6,
                            AmountInStock = 15,
                            CategoryId = 6,
                            Description = "Liverpool FC crest badge set with all the badges from the history of the club.",
                            ImageUrl = "https://store.liverpoolfc.com/media/catalog/product/cache/6e0c7b53c0ed72fe014b8d12b60d479c/a/2/a21bd03_4_1.jpg",
                            Name = "Liverpool FC Crest Badge Set",
                            Price = 119.99m,
                            isDeleted = false
                        });
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = "6c5e5ff5-d61f-419d-8e0c-290b05e27e87"
                        },
                        new
                        {
                            Id = 2,
                            UserId = "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944"
                        });
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.ShoppingCartProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "ShoppingCartId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartProducts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "user:fullname",
                            ClaimValue = "Customer Customerov",
                            UserId = "6c5e5ff5-d61f-419d-8e0c-290b05e27e87"
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "user:fullname",
                            ClaimValue = "Guest Guestov",
                            UserId = "d8b8354a-d93e-4887-8390-3ba136739184"
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "user:fullname",
                            ClaimValue = "Great Admin",
                            UserId = "b0b67914-3b5a-4bb7-b0ac-37c4c1c03944"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Customer", b =>
                {
                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.ShoppingCart", "ShoppingCart")
                        .WithMany()
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ShoppingCart");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Order", b =>
                {
                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.Customer", null)
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.OrderProduct", b =>
                {
                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Product", b =>
                {
                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.ShoppingCart", b =>
                {
                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithOne()
                        .HasForeignKey("LiverpoolFanShop.Infrastructure.Data.Models.ShoppingCart", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.ShoppingCartProduct", b =>
                {
                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingCartProducts")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LiverpoolFanShop.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("LiverpoolFanShop.Infrastructure.Data.Models.ShoppingCart", b =>
                {
                    b.Navigation("ShoppingCartProducts");
                });
#pragma warning restore 612, 618
        }
    }
}