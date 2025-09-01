namespace shop.Dtos
{
    public class CartResponse
    {
        public int Id { get; set; }
        public string SessionId { get; set; } = null!;
        public string? UserEmail { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; } = null!;
        public List<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
    }
}
