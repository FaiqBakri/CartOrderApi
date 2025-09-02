
using Microsoft.EntityFrameworkCore;
using shop.Domain;
using shop.Persistence;
using shop.Services;

namespace shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseInMemoryDatabase("CartOrderDb"));

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICartService, CartService>();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product { Id = 1, Sku = "PROD-001", Name = "Laptop", Description = "High-end gaming laptop", UnitPrice = 1500m, IsActive = true },
                        new Product { Id = 2, Sku = "PROD-002", Name = "Mouse", Description = "Wireless mouse", UnitPrice = 25.50m, IsActive = true },
                        new Product { Id = 3, Sku = "PROD-003", Name = "Keyboard", Description = "Mechanical keyboard", UnitPrice = 70m, IsActive = true }
                    );
                    context.SaveChanges();
                }
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
