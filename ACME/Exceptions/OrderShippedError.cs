namespace ACME.Exceptions;

public class OrderShippedError: Exception
{
    public OrderShippedError(int orderId)
        : base($"Order [{orderId}] has already been shipped and cannot accept any modifications.")
    {

    }
}
