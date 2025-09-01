namespace shop.Domain
{


    public enum CartStatus
    {
        Active,
        Converted,
        Abandoned
    }

    public class Cart
    {
        public int Id { get; set; }
        public string SessionId { get; set; } = null!;
        public string? UserEmail { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public CartStatus Status { get; set; } = CartStatus.Active;

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }

}