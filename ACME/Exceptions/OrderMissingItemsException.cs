using ACME.Models;

namespace ACME.Exceptions;

public class OrderMissingItemsException: Exception
{
    public OrderMissingItemsException(Order order) 
        : base($"Order is missing items and can not be shipped.") 
    { 
    }
}
