using shop.Dtos;

namespace shop.Services
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderFromCartAsync(CreateOrderFromCartRequest request);
        Task<OrderResponse?> GetOrderByIdAsync(int id);
    }
}
