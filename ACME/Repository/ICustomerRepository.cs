using ACME.DTO;
using ACME.Models;

namespace ACME.Repository;

public interface ICustomerRepository
{
    int Add(Customer customer);
    bool Update(int id, CustomerRequest request);
    bool Delete(int id);
    List<Customer> GetAll();
    Customer? Get(int id);
    Customer? GetEmail(string email);
    List<Customer> Find(CustomerFindRequest request);
}