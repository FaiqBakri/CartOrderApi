using Microsoft.EntityFrameworkCore;
using shop.Domain;
using shop.Dtos;
using shop.Persistence;

namespace shop.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartResponse> CreateCartAsync(CreateCartRequest request)
        {
            var cart = new Cart
            {
                SessionId = request.SessionId,
                UserEmail = request.UserEmail,
                StartDate = DateTime.UtcNow,
                Status = CartStatus.Active
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return MapCartToResponse(cart);
        }

        public async Task<CartResponse?> GetCartByIdAsync(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null) return null;

            return MapCartToResponse(cart);
        }

        public async Task<CartResponse> AddCartItemAsync(int cartId, AddCartItemRequest request)
        {
            if (request.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
                throw new KeyNotFoundException("Cart not found.");

            if (cart.Status != CartStatus.Active)
                throw new InvalidOperationException("Cart has already been converted or is not active.");

            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null || !product.IsActive)
                throw new KeyNotFoundException("Product not found or not active.");

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    UnitPriceSnapshot = product.UnitPrice
                });
            }

            await _context.SaveChangesAsync();

            return MapCartToResponse(cart);
        }

        public async Task<IEnumerable<CartItemResponse>> GetCartItemsAsync(int cartId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
                throw new KeyNotFoundException("Cart not found.");

            return cart.Items.Select(i => new CartItemResponse
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPriceSnapshot = i.UnitPriceSnapshot
            }).ToList();
        }

        private CartResponse MapCartToResponse(Cart cart)
        {
            return new CartResponse
            {
                Id = cart.Id,
                SessionId = cart.SessionId,
                UserEmail = cart.UserEmail,
                StartDate = cart.StartDate,
                Status = cart.Status.ToString(),
                Items = cart.Items.Select(i => new CartItemResponse
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPriceSnapshot = i.UnitPriceSnapshot
                }).ToList()
            };
        }
    }
}
