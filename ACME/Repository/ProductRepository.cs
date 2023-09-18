using AutoMapper;
using ACME.Data;
using ACME.DTO;
using ACME.Models;
using ACME.Exceptions;

namespace ACME.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private readonly AcmeContext _context;

    public ProductRepository(AcmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<Product> GetAll()
    {
        var result = from p in _context.Products
                     orderby p.Description
                     select p;

        return result.ToList();
    }

    public Product? Get(int id)
    {
        var result = from p in _context.Products
                     where p.Id == id
                     select p;

        return result.FirstOrDefault();
    }

    public List<Product> Find(string description)
    {
        var result = from p in _context.Products
                     where p.Description == description
                     orderby p.Description
                     select p;

        return result.ToList();
    }

    public int Add(ProductRequest request)
    {
        var duplicateProducts = Find(request.Description);
        if (duplicateProducts.Count > 0)
        {
            throw new DuplicateProductException(duplicateProducts[0]);
        }
        var product = _mapper.Map<Product>(request);
        _context.Products.Add(product);
        _context.SaveChanges();
        return product.Id;
    }

    public bool Update(int id, ProductRequest request)
    {
        var product = Get(id);
        if (product is null)
        {
            throw new NotFoundException($"Invalid product Id {id}.");
        }

        var duplicateProducts = Find(request.Description);
        if (duplicateProducts.Any(d => d.Id != id))
        {
            throw new DuplicateProductException(duplicateProducts[0]);
        }

        _mapper.Map(request, product);
        return _context.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        var Product = Get(id);
        if (Product is null)
        {
            throw new NotFoundException($"Invalid product Id {id}.");
        }
        _context.Products.Remove(Product);
        _context.SaveChanges();
        return true;
    }
}
