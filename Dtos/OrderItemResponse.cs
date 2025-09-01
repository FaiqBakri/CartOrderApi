namespace shop.Dtos
{
    public class OrderItemResponse
    {
        public int ProductId { get; set; }
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
    }
}
