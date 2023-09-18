namespace ACME.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
         : base(message)
    {
    }
}
