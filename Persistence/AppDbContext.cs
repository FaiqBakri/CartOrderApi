using shop.Domain;
using Microsoft.EntityFrameworkCore;


namespace shop.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Cart>()
             .HasMany(c => c.Items)
             .WithOne(ci => ci.Cart)
             .HasForeignKey(ci => ci.CartId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
               .HasMany(o => o.Items)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

             modelBuilder.Entity<Product>()
               .HasIndex(p => p.Sku)
               .IsUnique();

             modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = 1,
                        Sku = "PROD-001",
                        Name = "Laptop",
                        Description = "High-end gaming laptop",
                        UnitPrice = 1500.00m,
                        IsActive = true
                    },
                    new Product
                    {
                        Id = 2,
                        Sku = "PROD-002",
                        Name = "Mouse",
                        Description = "Wireless mouse",
                        UnitPrice = 25.50m,
                        IsActive = true
                    },
                    new Product
                    {
                        Id = 3,
                        Sku = "PROD-003",
                        Name = "Keyboard",
                        Description = "Mechanical keyboard",
                        UnitPrice = 70.00m,
                        IsActive = true
                    }
                );



        }



        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

    }
}