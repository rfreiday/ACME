namespace ACME.Test.Controller;

public class OrderControllerTest
{
    private readonly IOrderRepository _repository = A.Fake<IOrderRepository>();
    private readonly IMapper _mapper = A.Fake<IMapper>();

    [Fact]
    public void GetSingle_Success()
    {
        // Set up
        Order? order = A.Fake<Order>();
        A.CallTo(() => _repository.Get(1)).Returns(order);

        // Act
        var result = GetController().GetSingle(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
        A.CallTo(() => _repository.Get(1)).MustHaveHappened();
    }

    [Fact]
    public void GetSingle_NotFound()
    {
        // Set up
        Order? order = null;
        A.CallTo(() => _repository.Get(1)).Returns(order);

        // Act
        var result = GetController().GetSingle(1);

        // Assert
        result.Should().BeOfType(typeof(NoContentResult));
        A.CallTo(() => _repository.Get(1)).MustHaveHappened();
    }

    [Fact]
    public void Add_Success()
    {
        // Set up
        var orderRequest = A.Fake<OrderRequest>();
        A.CallTo(() => _repository.Add(orderRequest)).Returns(1);

        // Act
        var result = GetController().Add(orderRequest);

        // Assert
        result.Should().BeOfType(typeof(OkObjectResult));
        A.CallTo(() => _repository.Add(orderRequest)).MustHaveHappened();
        (result as OkObjectResult)?.Value.Should().Be(1);
    }

    [Fact]
    public void Add_BadRequest()
    {
        // Set up
        var orderRequest = A.Fake<OrderRequest>();
        A.CallTo(() => _repository.Add(orderRequest)).Throws(new Exception());

        // Act
        var result = GetController().Add(orderRequest);

        // Assert
        A.CallTo(() => _repository.Add(orderRequest)).MustHaveHappened();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    [Fact]
    public void AddItem_Success()
    {
        // Set up
        var orderItemRequest = A.Fake<OrderItemRequest>();
        var orderResponse = A.Fake<Order>();
        A.CallTo(() => _repository.AddOrderItem(1, orderItemRequest));

        // Act
        var result = GetController().AddItem(1, orderItemRequest);

        // Assert
        A.CallTo(() => _repository.AddOrderItem(1, orderItemRequest)).MustHaveHappened();
        result.Should().BeOfType(typeof(OkResult));
    }

    [Fact]
    public void AddItem_BadRequest()
    {
        // Set up
        var orderItemRequest = A.Fake<OrderItemRequest>();
        var orderResponse = A.Fake<Order>();
        A.CallTo(() => _repository.AddOrderItem(1, orderItemRequest)).Throws(new Exception());

        // Act
        var result = GetController().AddItem(1, orderItemRequest);

        // Assert
        A.CallTo(() => _repository.AddOrderItem(1, orderItemRequest)).MustHaveHappened();
        result.Should().BeOfType(typeof(BadRequestObjectResult)); ;
    }

    [Fact]
    public void DeleteItem_Success()
    {
        // Set up
        var orderItemRequest = A.Fake<OrderItemRequest>();
        A.CallTo(() => _repository.DeleteOrderItem(1, orderItemRequest));

        // Act
        var result = GetController().DeleteItem(1, orderItemRequest);

        // Assert
        A.CallTo(() => _repository.DeleteOrderItem(1, orderItemRequest)).MustHaveHappened();
        result.Should().BeOfType(typeof(OkResult));
    }

    [Fact]
    public void DeleteItem_BadRequest()
    {
        // Set up
        var orderItemRequest = A.Fake<OrderItemRequest>();
        A.CallTo(() => _repository.DeleteOrderItem(1, orderItemRequest)).Throws(new Exception());

        // Act
        var result = GetController().DeleteItem(1, orderItemRequest);

        // Assert
        A.CallTo(() => _repository.DeleteOrderItem(1, orderItemRequest)).MustHaveHappened();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    [Fact]
    public void AddPayment_Success()
    {
        // Set up
        var orderPaymentRequest = A.Fake<OrderPaymentRequest>();
        A.CallTo(() => _repository.AddOrderPayment(1, orderPaymentRequest));

        // Act
        var result = GetController().AddPayment(1, orderPaymentRequest);

        // Assert
        A.CallTo(() => _repository.AddOrderPayment(1, orderPaymentRequest)).MustHaveHappened();
        result.Should().BeOfType(typeof(OkResult));
    }

    [Fact]
    public void AddPayment_BadRequest()
    {
        // Set up
        var orderPaymentRequest = A.Fake<OrderPaymentRequest>();
        A.CallTo(() => _repository.AddOrderPayment(1, orderPaymentRequest)).Throws(new Exception());

        // Act
        var result = GetController().AddPayment(1, orderPaymentRequest);

        // Assert
        A.CallTo(() => _repository.AddOrderPayment(1, orderPaymentRequest)).MustHaveHappened();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    [Fact]
    public void DeletePayment_Success()
    {
        // Set up
        var orderPaymentRequest = A.Fake<OrderPaymentRequest>();
        A.CallTo(() => _repository.DeleteOrderPayment(1, orderPaymentRequest));

        // Act
        var result = GetController().DeletePayment(1, orderPaymentRequest);

        // Assert
        A.CallTo(() => _repository.DeleteOrderPayment(1, orderPaymentRequest)).MustHaveHappened();
        result.Should().BeOfType(typeof(OkResult));
    }

    [Fact]
    public void DeletePayment_BadRequest()
    {
        // Set up
        var orderPaymentRequest = A.Fake<OrderPaymentRequest>();
        A.CallTo(() => _repository.DeleteOrderPayment(1, orderPaymentRequest)).Throws(new Exception());

        // Act
        var result = GetController().DeletePayment(1, orderPaymentRequest);

        // Assert
        A.CallTo(() => _repository.DeleteOrderPayment(1, orderPaymentRequest));
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    private OrderController GetController()
    {
        return new OrderController(_repository, _mapper);
    }
}
