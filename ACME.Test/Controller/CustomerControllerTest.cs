using Azure.Core;

namespace ACME.Test.Controller;

public class CustomerControllerTest
{
    private readonly ICustomerRepository _repository = A.Fake<ICustomerRepository>();
    private readonly IMapper _mapper = A.Fake<IMapper>();

    [Fact]
    public void Get_Success()
    {
        // Set up
        var customers = A.CollectionOfFake<Customer>(1).ToList();
        A.CallTo(() => _repository.GetAll()).Returns(customers);

        // Act
        var result = GetController().GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void GetSingle_Success()
    {
        // Set up
        Customer? customer = A.Fake<Customer>();
        A.CallTo(() => _repository.Get(1)).Returns(customer);

        // Act
        var result = GetController().Get(1);

        // Assert
        A.CallTo(() => _repository.Get(1)).MustHaveHappened();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void GetSingle_NotFound()
    {
        // Set up
        Customer? customer = null;
        A.CallTo(() => _repository.Get(1)).Returns(customer);

        // Act
        var result = GetController().Get(1);

        // Assert
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Fact]
    public void GetEmail_Success()
    {
        // Set up
        string email = string.Empty;
        Customer? customer = A.Fake<Customer>();
        A.CallTo(() => _repository.GetEmail(email)).Returns(customer);

        // Act
        var result = GetController().GetEmail(email);

        // Assert
        A.CallTo(() => _repository.GetEmail(email)).MustHaveHappened();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void GetEmail_NotFound()
    {
        // Set up
        string email = string.Empty;
        Customer? customer = null;
        A.CallTo(() => _repository.GetEmail(email)).Returns(customer);

        // Act
        var result = GetController().GetEmail(email);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Fact]
    public void Add_Success()
    {
        // Set up
        CustomerRequest customer = A.Fake<CustomerRequest>();
        Customer repositoryCustomer = A.Fake<Customer>();
        A.CallTo(() => _mapper.Map<Customer>(customer)).Returns(repositoryCustomer);
        A.CallTo(() => _repository.Add(repositoryCustomer)).Returns(1);

        // Act
        var result = GetController().Add(customer);

        // Assert
        (result as OkObjectResult)?.Value.Should().Be(1);
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void Add_BadRequest()
    {
        // Set up
        CustomerRequest? customer = A.Fake<CustomerRequest>();
        Customer repositoryCustomer = A.Fake<Customer>();
        A.CallTo(() => _mapper.Map<Customer>(customer)).Returns(repositoryCustomer);
        A.CallTo(() => _repository.Add(repositoryCustomer)).Throws(new Exception());

        // Act
        var result = GetController().Add(customer);

        // Assert
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    [Fact]
    public void Update_Success()
    {
        // Set up
        var customer = A.Fake<CustomerRequest>();
        Customer repositoryCustomer = A.Fake<Customer>();
        A.CallTo(() => _mapper.Map<Customer>(customer)).Returns(repositoryCustomer);
        A.CallTo(() => _repository.Update(1, customer)).Returns(true);

        // Act
        var result = GetController().Update(1, customer);

        // Assert
        result.Should().BeOfType(typeof(OkResult));
    }

    [Fact]
    public void Update_NoContents()
    {
        // Set up
        var customer = A.Fake<CustomerRequest>();
        Customer repositoryCustomer = A.Fake<Customer>();
        A.CallTo(() => _repository.Update(1, customer)).Returns(false);

        // Act
        var result = GetController().Update(1, customer);

        // Assert
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Fact]
    public void Update_BadRequest()
    {
        // Set up
        CustomerRequest? customer = A.Fake<CustomerRequest>();
        Customer repositoryCustomer = A.Fake<Customer>();
        A.CallTo(() => _mapper.Map<Customer>(customer)).Returns(repositoryCustomer);
        A.CallTo(() => _repository.Update(1, customer)).Throws(new Exception());

        // Act
        var result = GetController().Update(1, customer);

        // Assert
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    [Fact]
    public void Delete_Success()
    {
        // Set up
        A.CallTo(() => _repository.Delete(1)).Returns(true);

        // Act
        var result = GetController().Delete(1);

        // Assert
        result.Should().BeOfType(typeof(OkResult));
    }

    [Fact]
    public void Delete_NoContent()
    {
        // Set up
        A.CallTo(() => _repository.Delete(1)).Returns(false);

        // Act
        var result = GetController().Delete(1);

        // Assert
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Fact]
    public void Delete_BadRequest()
    {
        // Set up
        A.CallTo(() => _repository.Delete(1)).Throws(new Exception());

        // Act
        var result = GetController().Delete(1);

        // Assert
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    private CustomerController GetController()
    {
        return new CustomerController(_repository, _mapper);
    }
}
