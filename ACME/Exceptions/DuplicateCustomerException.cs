using ACME.Models;

namespace ACME.Exceptions;

public class DuplicateCustomerException : Exception
{
    public DuplicateCustomerException(Customer customer)
        : base($"Duplicate Email: [{customer.Email}]. Another customer record ({customer.Name}) contains the same email.")
    {
    }
}
