namespace shop.Domain
{
    public abstract class BaseItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }  
        public decimal UnitPrice { get; set; } 
        
    }
}
