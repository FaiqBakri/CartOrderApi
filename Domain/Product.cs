namespace shop.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
