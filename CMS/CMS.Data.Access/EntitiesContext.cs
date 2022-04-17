using CMS.Data.Access.Configuration;
using CMS.Data.Access.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data.Access
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext(IConfigurationManagerService configurationManager) 
            : base(configurationManager.GetDbContextOptions("DefaultConnection"))
        {
            
        }

        public virtual DbSet<Customer> Customers { get; set; } 
        public virtual DbSet<Order> Orders { get; set; }    
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
                .HasMany(p => p.Orders)
                .WithOne(b => b.Customer)
                .HasForeignKey(s => s.OrderCustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.Customer)
                .WithMany(b => b.Orders)
                .HasForeignKey(p => p.OrderCustomerId);

            modelBuilder.Entity<Order>()
                  .HasMany(p => p.Items)
                  .WithOne(b => b.Order)
                  .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Item>()
                .HasOne(p => p.Order)
                .WithMany(b => b.Items)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Item>()
                    .HasOne(p => p.Product)
                    .WithOne(b => b.Item);

            modelBuilder.Entity<Product>()
                 .HasOne(p => p.Item)
                 .WithOne(b => b.Product);
        }
    }
}
