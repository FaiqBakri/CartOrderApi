namespace shop.Dtos
{
    public class CreateCartRequest
    {
        public string SessionId { get; set; } = null!;
        public string? UserEmail { get; set; }
    }
}
