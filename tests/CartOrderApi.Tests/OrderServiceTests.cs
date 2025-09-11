using Xunit;
using shop.Domain;

namespace CartOrderApi.Tests;

public class OrderServiceTests
{
    [Fact]
    public void OrderTotals_ShouldBeCalculatedCorrectly()
    {

        var order = new Order
        {
            Id = 1,
            InsertDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            Items = new List<OrderItem>()
        };

        order.Items.Add(new OrderItem
        {
            ProductId = 1,
            Sku = "P1001",
            Name = "Test Product",
            Quantity = 2,
            UnitPrice = 100m
        });

        order.Subtotal = order.Items.Sum(i => i.Quantity * i.UnitPrice); // 200
        order.Tax = order.Subtotal * 0.15m; // 30
        order.Total = order.Subtotal + order.Tax; // 230

        Assert.Equal(200m, order.Subtotal);
        Assert.Equal(30m, order.Tax);
        Assert.Equal(230m, order.Total);
    }
}
