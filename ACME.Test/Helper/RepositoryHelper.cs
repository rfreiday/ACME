namespace ACME.Test.Helper;

internal static class RepositoryHelper
{
    private static ICustomerRepository _customerRepository;
    private static IProductRepository _productRepository;
    private static IOrderRepository _orderRepository;
    private static List<Customer> _customerData;
    private static List<Product> _productData;
    private static List<Order> _orderData;
    private static IMapper _mapper;
    private static bool _initialized;

    internal static void Initialize()
    {
        if (_initialized)
        {
            return;
        }
        _customerData = DataHelper.GenerateCustomerData(10);
        _productData = DataHelper.GenerateProductData(10);
        _orderData = DataHelper.GenerateOrderData(10);

        var mockDbContext = new Mock<AcmeContext>();

        mockDbContext.Setup(c => c.Customers).Returns(GetMockDbSet<Customer>(_customerData.AsQueryable()).Object);
        mockDbContext.Setup(c => c.Products).Returns(GetMockDbSet<Product>(_productData.AsQueryable()).Object);
        mockDbContext.Setup(c => c.Orders).Returns(GetMockDbSet<Order>(_orderData.AsQueryable()).Object);

        mockDbContext.Setup(c => c.SaveChanges()).Returns(1);

        _mapper = A.Fake<IMapper>();
        _customerRepository = new CustomerRepository(mockDbContext.Object, _mapper);
        _productRepository = new ProductRepository(mockDbContext.Object, _mapper);
        _orderRepository = new OrderRepository(mockDbContext.Object, _mapper);

        _initialized = true;
    }
    internal static List<Customer> CustomerData => _customerData;
    internal static List<Product> ProductData => _productData;
    internal static List<Order> OrderData => _orderData;
    internal static ICustomerRepository CustomerRepository => _customerRepository;
    internal static IProductRepository ProductRepository => _productRepository;
    internal static IOrderRepository OrderRepository => _orderRepository;
    internal static IMapper Mapper => _mapper;

    internal static Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> data) where T : class
    {
        var mockDbSet = new Mock<DbSet<T>>();

        mockDbSet.As<IQueryable<T>>().Setup(c => c.Provider).Returns(data.Provider);
        mockDbSet.As<IQueryable<T>>().Setup(c => c.ElementType).Returns(data.ElementType);
        mockDbSet.As<IQueryable<T>>().Setup(c => c.Expression).Returns(data.Expression);
        mockDbSet.As<IQueryable<T>>().Setup(c => c.GetEnumerator()).Returns(data.GetEnumerator());
        
        return mockDbSet;
    }
}
