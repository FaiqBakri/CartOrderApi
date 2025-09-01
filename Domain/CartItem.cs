namespace shop.Domain
{
    public class CartItem : BaseItem
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        public decimal UnitPriceSnapshot
        {
            get => UnitPrice;
            set => UnitPrice = value;
        }
    }
}


