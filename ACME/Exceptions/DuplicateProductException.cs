using ACME.Models;

namespace ACME.Exceptions;

public class DuplicateProductException: Exception
{
    public DuplicateProductException(Product product)
        : base($"Duplicate Product Description: There is another product named '{product.Description}'.")
    {
    }
}
