namespace shop.Dtos
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }

    }
}
