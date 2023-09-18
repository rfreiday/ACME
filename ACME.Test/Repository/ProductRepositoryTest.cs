namespace ACME.Test.Repository;

public class ProductRepositoryTest
{
    private List<Product> _products;
    private IProductRepository _repository;
    private IMapper _mapper;
    private int _maxId;

    public ProductRepositoryTest()
    {
        RepositoryHelper.Initialize();

        _repository = RepositoryHelper.ProductRepository;
        _products = RepositoryHelper.ProductData;
        _mapper = RepositoryHelper.Mapper;
        _maxId = _products.Max(x => x.Id);
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
    public void Find_Success()
    {
        // Set Up
        var description = _products[0].Description;

        // Act
        var result = _repository.Find(description);

        // Assert
        Assert.True(result.Count > 0);
    }

    [Fact]
    public void Find_Fail()
    {
        // Set Up
        var description = string.Empty;

        // Act
        var result = _repository.Find(description);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Add_Success()
    {
        // Set Up
        var product = DataHelper.GenerateProductData(1).First()!;
        var request = _mapper.Map<ProductRequest>(product);

        // Act
        var result = _repository.Add(request);

        // Assert
        Assert.True(result >= 0);
    }

    [Fact]
    public void Add_Fail()
    {
        // Set Up
        var product = _products[0];
        var request = _mapper.Map<ProductRequest>(product);

        // Act
        try
        {
            var result = _repository.Add(request);
        }
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(DuplicateProductException));
        }
    }

    [Fact]
    public void Update_Success()
    {
        // Set Up
        var product = DataHelper.GenerateProductData(1).First()!;
        var request = new ProductRequest()
        {
            Description = product.Description + " Changed"
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
        var duplicateProduct = _products[0];
        var request = new ProductRequest()
        {
            Description = duplicateProduct.Description
        };

        // Act
        try
        {
            var result = _repository.Update(2, request);
        }

        // Assert
        catch (Exception ex)
        {
            Assert.True(ex.GetType() == typeof(DuplicateProductException));
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
