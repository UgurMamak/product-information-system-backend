using Application.Entities.Configurations;
using Application.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text;

namespace Application.Entities
{
    public class ProductInformationContext : DbContext
    {
        public ProductInformationContext()
        {
        }

        public ProductInformationContext(DbContextOptions<ProductInformationContext> options) :
        base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<LikeProduct> LikeProducts { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductPoint> ProductPoints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ProductInfo;User ID=sa;Password=123456;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new CommentLikeConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new LikeProductConfiguration());
            modelBuilder.ApplyConfiguration(new OperationClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPointConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());          
        }
    }
}
