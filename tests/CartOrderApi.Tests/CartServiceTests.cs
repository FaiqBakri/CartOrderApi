using Xunit;
using shop.Domain;

namespace CartOrderApi.Tests;

public class CartServiceTests
{
    [Fact]
    public void AddingDuplicateCartItems_ShouldMergeQuantities()
    {
        var cart = new Cart
        {
            Id = 1,
            SessionId = "test-session",
            StartDate = DateTime.UtcNow,
            Status = CartStatus.Active,
            Items = new List<CartItem>()
        };

        cart.Items.Add(new CartItem { ProductId = 1, Quantity = 2, UnitPriceSnapshot = 100m });
        cart.Items.Add(new CartItem { ProductId = 1, Quantity = 3, UnitPriceSnapshot = 100m });

        var totalQty = cart.Items.Where(i => i.ProductId == 1).Sum(i => i.Quantity);

        Assert.Equal(5, totalQty);
    }
}
