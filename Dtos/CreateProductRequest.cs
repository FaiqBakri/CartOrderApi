namespace shop.Dtos
{
    public class CreateProductRequest
    {
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
