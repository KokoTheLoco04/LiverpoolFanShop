﻿using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Infrastructure.Data.SeedDb
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            var data = new SeedData();

            builder.HasData(new Product[] { data.Jersey, data.Scarf, data.Jacket, data.Cup, data.Bottle, data.BadgeSet });
        }
    }
}
