using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NorthwindAspNetCore.Models;

namespace NorthwindAspNetCore.Data
{
    public class NorthwindContext : IdentityDbContext<SiteUser>
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Category>().Property(p => p.Name).HasColumnName("CategoryName");

            modelBuilder.Entity<Region>().ToTable("Region");
            modelBuilder.Entity<Region>().Property(p => p.Name).HasColumnName("RegionDescription");

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnName("ProductName");

            modelBuilder.Entity<Supplier>().ToTable("Supplier");

            base.OnModelCreating(modelBuilder);
        }
    }
}