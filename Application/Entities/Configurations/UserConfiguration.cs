
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(a => a.Id);

            builder.HasOne(x=>x.OperationClaim)
                .WithMany()
                .HasForeignKey(x=>x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
