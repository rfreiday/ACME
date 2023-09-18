using ACME.Models;

namespace ACME.Exceptions;

public class OrderNotPaidException: Exception
{
    public OrderNotPaidException(Order order) 
        : base($"The order can not be shipped. Balance due: {order.AmountDue:C}.")
    {
    }
}
