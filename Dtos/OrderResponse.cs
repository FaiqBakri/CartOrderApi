namespace shop.Dtos
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTime InsertDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public string? UserEmail { get; set; }
        public int SourceCartId { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
    }
}
