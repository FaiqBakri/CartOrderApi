namespace shop.Domain
{
    public class OrderItem:BaseItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public string Sku { get; set; } = null!;  
        public string Name { get; set; } = null!;

        public decimal LineTotal => Quantity * UnitPrice;
    }
}
