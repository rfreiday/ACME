namespace ACME.Test.Models;

public class OrderTest
{
    [Fact]
    public void OrderTotalsValid()
    {
        // Set up
        var order = DataHelper.GenerateOrderData(1).First();
        order.Recalculate();

        // Assert
        order.Subtotal.Should().Be(GetSubotal(order));
        order.Taxes.Should().Be(GetTaxes(order));
        order.Total.Should().Be(GetTotal(order));
    }

    [Fact]
    public void OrderStateShouldBe_New()
    {
        // Set up
        var order = A.Fake<Order>();

        // Assert
        order.PaidInFull.Should().Be(false);
        order.Overpaid.Should().Be(false);
        order.Subtotal.Should().Be(0);
        order.Total.Should().Be(0);
        order.TotalPayments.Should().Be(0);
        order.AmountDue.Should().Be(0);
        order.Status.Should().Be(OrderStatus.New);
        order.PaidInFull.Should().Be(false);
    }

    [Fact]
    public void OrderStateShouldBe_InProgress()
    {
        // Set up
        var order = DataHelper.GenerateOrderData(1).First();
        var payment = new OrderPayment()
        {
            Amount = order.Total - 1,
            PaymentType = "CASH"
        };
        order.OrderPayments.Add(payment);
        order.Recalculate();

        // Assert
        order.PaidInFull.Should().Be(false);
        order.Overpaid.Should().Be(false);
        order.Status.Should().Be(OrderStatus.InProgress);
        order.AmountDue.Should().Be(1);
    }

    [Fact]
    public void OrderStateShouldBe_PaidInFull()
    {
        // Set up
        var order = DataHelper.GenerateOrderData(1).First();
        var payment = new OrderPayment()
        {
            Amount = order.Total,
            PaymentType = "CASH"
        };
        order.OrderPayments.Add(payment);
        order.Recalculate();

        // Assert
        order.PaidInFull.Should().Be(true);
        order.Overpaid.Should().Be(false);
        order.Status.Should().Be(OrderStatus.PaidInFull);
        order.AmountDue.Should().Be(0);
    }

    [Fact]
    public void OrderStateShouldBe_ChangeDue()
    {
        // Set up
        var order = DataHelper.GenerateOrderData(1).First();
        var payment = new OrderPayment()
        {
            Amount = order.Total + 1,
            PaymentType = "CASH"
        };
        order.OrderPayments.Add(payment);
        order.Recalculate();

        // Assert
        order.PaidInFull.Should().Be(true);
        order.Overpaid.Should().Be(true);
        order.Status.Should().Be(OrderStatus.ChangeDue);
        order.AmountDue.Should().Be(-1);
    }
    
    private decimal GetSubotal(Order order)
    {
        return order.OrderItems.Sum(i => i.Total);
    }

    private decimal GetTaxes(Order order)
    {
        return GetSubotal(order) * (decimal)0.05;
    }

    private decimal GetTotal(Order order)
    {
        return GetSubotal(order) + GetTaxes(order);
    }
}
