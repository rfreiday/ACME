namespace ACME.Test.Controller;

public class ProductControllerTest
{
    private readonly IProductRepository _repository = A.Fake<IProductRepository>();
    private readonly IMapper _mapper = A.Fake<IMapper>();

    [Fact]
    public void Get_Success()
    {
        // Set up
        var products = A.CollectionOfFake<Product>(1).ToList();
        A.CallTo(() => _repository.GetAll()).Returns(products);

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
        Product? product = A.Fake<Product>();
        A.CallTo(() => _repository.Get(1)).Returns(product);

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
        Product? product = null;
        A.CallTo(() => _repository.Get(1)).Returns(product);

        // Act
        var result = GetController().Get(1);

        // Assert
        A.CallTo(() => _repository.Get(1)).MustHaveHappened();
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Fact]
    public void Find_Success()
    {
        // Set up
        var products = new List<Product>
        {
            A.Fake<Product>()
        };
        var description = string.Empty;
        A.CallTo(() => _repository.Find(description)).Returns(products);

        // Act
        var result = GetController().Find(description);

        // Assert
        A.CallTo(() => _repository.Find(description)).MustHaveHappened();
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void Find_NotFound()
    {
        // Set up
        var products = new List<Product>();
        var description = string.Empty;
        A.CallTo(() => _repository.Find(description)).Returns(products);

        // Act
        var result = GetController().Find(description);

        // Assert
        A.CallTo(() => _repository.Find(description)).MustHaveHappened();
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Fact]
    public void Add_Success()
    {
        // Set up
        var productRequest = A.Fake<ProductRequest>();
        A.CallTo(() => _repository.Add(productRequest)).Returns(1);

        // Act
        var result = GetController().Add(productRequest);

        // Assert
        result.Should().BeOfType(typeof(OkObjectResult));
        A.CallTo(() => _repository.Add(productRequest)).MustHaveHappened();
        (result as OkObjectResult)?.Value.Should().Be(1);
    }

    [Fact]
    public void Add_BadRequest()
    {
        // Set up
        var productRequest = A.Fake<ProductRequest>();
        A.CallTo(() => _repository.Add(productRequest)).Throws(new Exception());

        // Act
        var result = GetController().Add(productRequest);

        // Assert
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    private ProductController GetController()
    {
        return new ProductController(_repository, _mapper);
    }

}
