namespace ACME.Test.Helper;

internal class DataHelper
{
    internal static List<Customer> GenerateCustomerData(int count)
    {
        var faker = new Faker<Customer>()
            .RuleFor(c => c.Id, f => f.IndexFaker)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Address1, f => $"{f.Person.Address.Suite} {f.Person.Address.Street}")
            .RuleFor(c => c.City, f => f.Person.Address.City)
            .RuleFor(c => c.State, f => f.Person.Address.State)
            .RuleFor(c => c.PostalCode, f => f.Person.Address.ZipCode)
            .RuleFor(c => c.Phone1, f => f.Person.Phone)
            .RuleFor(c => c.Phone2, f => f.Person.Phone);
        return faker.Generate(count);
    }

    internal static List<Product> GenerateProductData(int count)
    {
        var faker = new Faker<Product>()
            .RuleFor(c => c.Id, f => f.IndexFaker)
            .RuleFor(c => c.Description, f => f.Commerce.Product())
            .RuleFor(c => c.UnitCost, f => f.Finance.Random.Number(1, 1000))
            .RuleFor(c => c.Price, f => f.Finance.Random.Number(1, 1000))
            .RuleFor(c => c.Taxable, true);

        return faker.Generate(count);
    }

    internal static List<Order> GenerateOrderData(int count, bool paid = false)
    {
        var orderFaker = new Faker<Order>()
            .RuleFor(c => c.Id, f => f.IndexFaker)
            .RuleFor(c => c.CustomerId, f => f.IndexFaker);

        var result = orderFaker.Generate(count);

        foreach (var order in result)
        {
            var itemCount = new Random().Next(1, 5);
            AddOrderItems(order, itemCount);
        }

        if (paid)
        {
            foreach (var order in result)
            {
                var paymentCount = new Random().Next(1, 3);
                AddOrderPayments(order, paymentCount);
            }
        }

        return result;
    }

    internal static void AddOrderItems(Order order, int count)
    {
        var orderItemFaker = new Faker<OrderItem>()
            .RuleFor(i => i.Id, f => f.IndexFaker)
            .RuleFor(i => i.ProductId, f => f.IndexFaker)
            .RuleFor(i => i.ProductDescription, f => f.Commerce.Product())
            .RuleFor(i => i.Quantity, f => f.Finance.Random.Number(1, 2))
            .RuleFor(i => i.Price, f => f.Finance.Random.Number(1, 1000));

        var items = orderItemFaker.Generate(count);
        items.ForEach(item =>
        {
            item.OrderId = order.Id;
            item.Total = item.Quantity * item.Price;
            order.OrderItems.Add(item);
        });

        order.Recalculate();
    }

    internal static void AddOrderPayments(Order order, int count)
    {
        var averagePayment = Math.Floor(order.Subtotal / count);
        var paymentFaker = new Faker<OrderPayment>()
            .RuleFor(p => p.Id, f => f.IndexFaker)
            .RuleFor(p => p.OrderId, order.Id)
            .RuleFor(p => p.PaymentType, f => f.Finance.TransactionType())
            .RuleFor(p => p.Amount, averagePayment);

        var payments = paymentFaker.Generate(count);
        var totalPayments = payments.Sum(p => p.Amount);
        payments[count - 1].Amount += (order.AmountDue - totalPayments);

        payments.ForEach(p => order.OrderPayments.Add(p));
    }
}
