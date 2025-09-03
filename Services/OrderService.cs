using Microsoft.EntityFrameworkCore;
using shop.Domain;
using shop.Dtos;
using shop.Persistence;

namespace shop.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> CreateOrderFromCartAsync(CreateOrderFromCartRequest request)
        {
            // 1) Load cart with items
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == request.CartId);

            if (cart == null)
                throw new KeyNotFoundException("Cart not found.");

            if (cart.Status != CartStatus.Active)
                throw new InvalidOperationException("Cart already converted or not active.");

            if (!cart.Items.Any())
                throw new InvalidOperationException("Cart is empty.");

            // 2) Create order
            var order = new Order
            {
                InsertDate = DateTime.UtcNow,
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyy}-{Guid.NewGuid().ToString().Substring(0, 8)}",
                Status = OrderStatus.Pending,
                SourceCartId = cart.Id,
                UserEmail = cart.UserEmail
            };

            var orderItems = new List<OrderItem>();

            foreach (var item in cart.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    throw new InvalidOperationException("Product not found.");

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Sku = product.Sku,
                    Name = product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPriceSnapshot
                };

                orderItems.Add(orderItem);
            }

            order.Items = orderItems;

            // 3) Totals
            order.Subtotal = order.Items.Sum(i => i.LineTotal);
            order.Tax = (request.TaxRate ?? 0) * order.Subtotal;
            order.Total = order.Subtotal + order.Tax;

            // 4) Mark cart as converted
            cart.Status = CartStatus.Converted;

            // 5) Save transaction
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // 6) Map to response
            return new OrderResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                InsertDate = order.InsertDate,
                Status = order.Status.ToString(),
                Subtotal = order.Subtotal,
                Tax = order.Tax,
                Total = order.Total,
                SourceCartId = order.SourceCartId,
                UserEmail = order.UserEmail,
                Items = order.Items.Select(i => new OrderItemResponse
                {
                    ProductId = i.ProductId,
                    Sku = i.Sku,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                }).ToList()
            };
        }

        public async Task<OrderResponse?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            return new OrderResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                InsertDate = order.InsertDate,
                Status = order.Status.ToString(),
                Subtotal = order.Subtotal,
                Tax = order.Tax,
                Total = order.Total,
                SourceCartId = order.SourceCartId,
                UserEmail = order.UserEmail,
                Items = order.Items.Select(i => new OrderItemResponse
                {
                    ProductId = i.ProductId,
                    Sku = i.Sku,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                }).ToList()
            };
        }
    }
}
