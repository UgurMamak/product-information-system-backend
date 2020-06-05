using Application.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Configurations
{

    public class ProductPointConfiguration : IEntityTypeConfiguration<ProductPoint>
    {
        public void Configure(EntityTypeBuilder<ProductPoint> builder)
        {
            builder.ToTable("ProductPoint");
            builder.HasKey(a => a.Id);


            builder.HasOne(x=>x.Product)
                .WithMany()
                .HasForeignKey(x=>x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x=>x.User)
                .WithMany()
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
