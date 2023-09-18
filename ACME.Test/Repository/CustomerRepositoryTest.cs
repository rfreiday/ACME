namespace ACME.Test.Repository;

public class CustomerRepositoryTest
{
    private List<Customer> _customers;
    private ICustomerRepository _repository;
    private IMapper _mapper;
    private int _maxId;

    public CustomerRepositoryTest()
    {
        RepositoryHelper.Initialize();

        _repository = RepositoryHelper.CustomerRepository;
        _customers = RepositoryHelper.CustomerData;
        _mapper = RepositoryHelper.Mapper;
        _maxId = _customers.Max(x => x.Id);
    }

    [Fact]
    public void GetAll_Success()
    {
        // Act
        var result = _repository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
    }

    [Fact]
    public void Get_Success()
    {
        // Act
        var result = _repository.Get(_maxId);

        // Assert
        Assert.NotNull(result);
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
    public void GetEmail_Success()
    {
        // Set Up
        var email = _customers[0].Email;

        // Act
        var result = _repository.GetEmail(email);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetEmail_Fail()
    {
        // Set Up
        var email = string.Empty;

        // Act
        var result = _repository.GetEmail(email);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Add_Success()
    {
        // Set Up
        var customer = DataHelper.GenerateCustomerData(1).First()!;

        // Act
        var result = _repository.Add(customer);

        // Assert
        Assert.True(result >= 0);
    }

    [Fact]
    public void Add_Fail()
    {
        // Set Up
        var customer = _customers[0];

        // Act
        try
        {
            var result = _repository.Add(customer);
        }

        // Assert
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(DuplicateCustomerException));
        }
    }

    [Fact]
    public void Update_Success()
    {
        // Set Up
        var customer = DataHelper.GenerateCustomerData(1).First()!;
        var request = new CustomerRequest()
        {
            Name = customer.Name,
            Email = customer.Email
        };

        // Act
        var result = _repository.Update(1, request);

        // Assert
        Assert.True(result == true);
    }

    [Fact]
    public void Update_Fail()
    {
        // Set Up
        var duplicateCustomer = _customers[0];
        var request = _mapper.Map<CustomerRequest>(duplicateCustomer);

        // Act
        try
        {
            var result = _repository.Update(2, request);
        }

        // Assert
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(DuplicateCustomerException));
        }
    }

    [Fact]
    public void Delete_Success()
    {
        // Set Up
        var id = _maxId;

        // Act
        var result = _repository.Delete(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_Fail()
    {
        // Set Up
        var id = _maxId + 1;

        // Act
        try
        {
            _repository.Delete(id);
        }

        // Assert
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(NotFoundException));
        }
    }
}