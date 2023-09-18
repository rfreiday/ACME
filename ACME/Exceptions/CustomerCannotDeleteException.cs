using ACME.DTO;
using ACME.Models;

namespace ACME.Exceptions;

public class CustomerCannotDeleteException : Exception
{
    public CustomerCannotDeleteException(Customer customer)
        : base($"Customer {customer.Name} has existing orders and cannot be deleted.")
    {
    }
}
