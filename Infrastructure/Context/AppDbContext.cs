using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderProduct> OrderProduct { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User").HasQueryFilter(u => u.DeletionDate == null);
        modelBuilder.Entity<Customer>().ToTable("Customer").HasQueryFilter(u => u.DeletionDate == null);
        modelBuilder.Entity<Product>().ToTable("Product").HasQueryFilter(u => u.DeletionDate == null);
        modelBuilder.Entity<Order>().ToTable("Order").HasQueryFilter(u => u.DeletionDate == null);

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Order)
                .WithMany(p => p.OrderProductList)
                .HasForeignKey(e => e.OrderId);

            entity.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId);
        });

    }
}