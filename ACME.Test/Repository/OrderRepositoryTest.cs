namespace ACME.Test.Repository;

public class OrderRepositoryTest
{
    private List<Order> _orders;
    private IOrderRepository _repository;
    private IMapper _mapper;
    private int _maxId;

    public OrderRepositoryTest()
    {
        RepositoryHelper.Initialize();

        _repository = RepositoryHelper.OrderRepository;
        _orders = RepositoryHelper.OrderData;
        _mapper = RepositoryHelper.Mapper;
        _maxId = _orders.Max(x => x.Id);
    }

    [Fact]
    public void Get_Success()
    {
        // Set Up
        var id = 1;
        var order = _orders.FirstOrDefault(o => o.Id == id);

        // Act
        var result = _repository.Get(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(order?.CustomerId, result.CustomerId);
    }

    [Fact]
    public void Get_Fail()
    {
        // Act
        var result = _repository.Get(_maxId + 1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Add_Items_Increment_Quantity_Success()
    {
        //// Set Up
        //var order = _orders.FirstOrDefault()!;
        //A.CallTo(o => _repository.MapOrderItemProduct(order)).
        //var item = order.OrderItems.First()!;
        //var request = new OrderItemRequest()
        //{
        //    ProductId = item.ProductId,
        //    Quantity = 1
        //};

        //// Act
        //_repository.AddOrderItem(0, request);

        // Assert
    }

    [Fact]
    public void Add_Payment_Success()
    {
        // Set Up
        var order = _repository.Get(1)!;
        var request = new OrderPaymentRequest()
        {
            Amount = order.AmountDue,
            PaymentType = "CASH"
        };
        var payment = new OrderPayment()
        {
            Amount = request.Amount,
            PaymentType = request.PaymentType
        };
        A.CallTo(() => _mapper.Map<OrderPayment>(request)).Returns(payment);

        // Act
        _repository.AddOrderPayment(1, request);

        // Assert
        Assert.True(order.PaidInFull);
        Assert.True(order.Status == OrderStatus.PaidInFull);
        Assert.Equal(order.Total, order.TotalPayments);
    }

    [Fact]
    public void Add_Payment_Overpaid_Fail()
    {
        // Set Up
        var order = _repository.Get(1)!;

        // Act
        try
        {
            var request = new OrderPaymentRequest()
            {
                Amount = order.AmountDue + 1,
                PaymentType = "CASH"
            };
            var payment = new OrderPayment()
            {
                Amount = request.Amount,
                PaymentType = request.PaymentType
            };
            A.CallTo(() => _mapper.Map<OrderPayment>(request)).Returns(payment);
            _repository.AddOrderPayment(1, request);

            Assert.Fail();
        }

        // Assert
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(OrderPaymentExceedsAmountDueException));
        }
    }

}

