using AutoMapper;
using ACME.Data;
using ACME.DTO;
using ACME.Models;
using ACME.Exceptions;

namespace ACME.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMapper _mapper;
    private readonly AcmeContext _context;

    public CustomerRepository(AcmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<Customer> GetAll()
    {
        var customers = from c in _context.Customers
                        orderby c.Name
                        select c;
        return customers.ToList();
    }

    public Customer? Get(int id)
    {
        var customers = from c in _context.Customers
                        where c.Id == id
                        select c;
        return customers.FirstOrDefault();
    }

    public Customer? GetEmail(string email)
    {
        var customers = from c in _context.Customers
                        orderby c.Name
                        where c.Email == email
                        select c;
        return customers.FirstOrDefault();
    }

    public List<Customer> Find(CustomerFindRequest request)
    {
        var customers = from c in _context.Customers
                        orderby c.Name
                        where
                            ((request.Email == null) || (c.Email == request.Email)) &&
                            ((request.Name == null) || c.Name.Contains(request.Name)) &&
                            ((request.Phone == null) || (c.Phone1 == request.Phone) || (c.Phone2 == request.Phone)) &&
                            ((request.City == null) || (c.City == request.City)) &&
                            ((request.State == null) || (c.State == request.State)) &&
                            ((request.PostalCode == null) || (c.PostalCode == request.PostalCode))
                        select c;
        return customers.ToList();
    }

    public int Add(Customer customer)
    {
        var duplicateCustomer = GetEmail(customer.Email);
        if (duplicateCustomer is not null)
        {
            throw new DuplicateCustomerException(duplicateCustomer);
        }

        //var customer = _mapper.Map<Customer>(request);
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer.Id;
    }

    public bool Update(int id, CustomerRequest request)
    {
        var updateCustomer = Get(id);
        if (updateCustomer is null)
        {
            throw new NotFoundException($"Invalid customer Id {id}.");
        }

        var duplicateCustomer = GetEmail(request.Email);
        if (duplicateCustomer is not null && duplicateCustomer.Id != id)
        {
            throw new DuplicateCustomerException(duplicateCustomer);
        }

        _mapper.Map(request, updateCustomer);
        return _context.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        var customer = Get(id);
        if (customer is null)
        {
            throw new NotFoundException($"Invalid customer Id [{id}].");
        }
        _context.Customers.Remove(customer);
        _context.SaveChanges();
        return true;
    }
}
