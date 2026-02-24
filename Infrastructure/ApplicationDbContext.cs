using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Store> Stores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Products)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.userId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email_Unique");


            modelBuilder.Entity<Order>()
           .HasOne(o => o.Customer)
           .WithMany(u => u.OrdersAsCustomer)
           .HasForeignKey(o => o.CustomerId)
           .OnDelete(DeleteBehavior.Restrict); // Prevent accidental deletion of customer

            // Configure Order-Vendor relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Vendor)
                .WithMany(u => u.OrdersAsVendor)
                .HasForeignKey(o => o.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
            // Product-Supplier Relationship (One-to-Many)
            modelBuilder.Entity<OrderItem>().
                HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.userId)
                .OnDelete(DeleteBehavior.Cascade);


            // Product-Size Relationship (One-to-Many)


            modelBuilder.Entity<Product>()

                .HasMany(p => p.Sizes)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product-Color Relationship (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Colors)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product-Image Relationship (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ImageUrls)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");



            modelBuilder.Entity<Review>()
       .HasOne(r => r.User)
       .WithMany(u => u.Reviews)
       .HasForeignKey(r => r.UserId)
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent same user reviewing same product twice
            modelBuilder.Entity<Review>()
                .HasIndex(r => new { r.UserId, r.ProductId })
                .IsUnique();


            modelBuilder.Entity<Store>()
             .HasOne(s => s.Owner)
              .WithOne(u => u.Store)
             .HasForeignKey<Store>(s => s.OwnerId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
