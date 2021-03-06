﻿using Application.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Application.Entities.Configurations
{

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(a => a.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            /*
            builder.HasOne(x=>x.ProductType)
                .WithOne(x=>x.Product)
                .HasForeignKey<Product>(x=>x.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade);*/
            builder.HasOne(x=>x.ProductType)
                .WithMany()
                .HasForeignKey(x=>x.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
