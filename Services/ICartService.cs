using shop.Dtos;

namespace shop.Services
{
    public interface ICartService
    {
        Task<CartResponse> CreateCartAsync(CreateCartRequest request);
        Task<CartResponse?> GetCartByIdAsync(int id);
        Task<CartResponse> AddCartItemAsync(int cartId, AddCartItemRequest request);
        Task<IEnumerable<CartItemResponse>> GetCartItemsAsync(int cartId);
    }
}
