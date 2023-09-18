namespace ACME.Exceptions;

public class OrderNotFoundException : Exception
{
    public OrderNotFoundException(int id) : 
        base($"Order Number [{id}] is invalid.")
    { 
    }
}
