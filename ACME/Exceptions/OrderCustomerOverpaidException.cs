using ACME.Models;

namespace ACME.Exceptions;

public class OrderCustomerOverpaidException: Exception
{
    public OrderCustomerOverpaidException(Order order)
        : base($"The customer has overpaid by {Math.Abs(order.AmountDue):C}. Please make adjustments before shipping.")
    {
    }
}
