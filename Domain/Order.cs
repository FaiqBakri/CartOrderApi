namespace shop.Domain
{

    public enum OrderStatus
    {
        Pending,
        Paid,
        Cancelled
    }
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = null!;  
        public string? UserEmail { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }


        public int SourceCartId { get; set; }
        public Cart? SourceCart { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
