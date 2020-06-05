using Application.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Configurations
{

    public class LikeProductConfiguration : IEntityTypeConfiguration<LikeProduct>
    {
        public void Configure(EntityTypeBuilder<LikeProduct> builder)
        {
            builder.ToTable("LikeProduct");
            builder.HasKey(a => a.Id);

            builder.HasOne(x=>x.Product)
                .WithMany(x=>x.LikeProducts)
                .HasForeignKey(x=>x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x=>x.User)
                .WithMany()
                .HasForeignKey(x=>x.UserId);
        }
    }
}
