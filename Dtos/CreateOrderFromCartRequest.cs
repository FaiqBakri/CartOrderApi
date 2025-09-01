namespace shop.Dtos
{
    public class CreateOrderFromCartRequest
    {
        public int CartId { get; set; }
        public decimal? TaxRate { get; set; }
    }
}
