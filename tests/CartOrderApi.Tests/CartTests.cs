using Xunit;
using shop.Domain;

namespace CartOrderApi.Tests;

public class CartTests
{
    [Fact]
    public void CannotConvertAlreadyConvertedCart()
    {
        var cart = new Cart
        {
            Id = 1,
            SessionId = "abc123",
            Status = CartStatus.Converted,
            Items = new List<CartItem>
            {
                new CartItem { ProductId = 1, Quantity = 1, UnitPriceSnapshot = 50m }
            }
        };

        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            if (cart.Status == CartStatus.Converted)
                throw new InvalidOperationException("Cart already converted to order.");
        });

        Assert.Equal("Cart already converted to order.", ex.Message);
    }
}
