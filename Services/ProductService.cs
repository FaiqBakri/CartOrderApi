using Microsoft.EntityFrameworkCore;
using shop.Dtos;
using shop.Domain;
using shop.Persistence;

namespace shop.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
        {
            // تحقق من SKU فريد
            if (await _context.Products.AnyAsync(p => p.Sku == request.Sku))
                throw new InvalidOperationException("SKU already exists.");

            var product = new Product
            {
                Sku = request.Sku,
                Name = request.Name,
                Description = request.Description,
                UnitPrice = request.UnitPrice,
                IsActive = true
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                IsActive = product.IsActive
            };
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsAsync(string? search, int skip, int take)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search) || p.Sku.Contains(search));

            var products = await query.Skip(skip).Take(take).ToListAsync();

            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Sku = p.Sku,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                IsActive = p.IsActive
            });
        }

        public async Task<ProductResponse?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductResponse
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                IsActive = product.IsActive
            };
        }
    }
}
