using shop.Dtos;

namespace shop.Services
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProductAsync(CreateProductRequest request);
        Task<IEnumerable<ProductResponse>> GetProductsAsync(string? search, int skip, int take);
        Task<ProductResponse?> GetProductByIdAsync(int id);
    }
}
