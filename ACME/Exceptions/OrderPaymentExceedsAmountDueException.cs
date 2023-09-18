using ACME.DTO;

namespace ACME.Exceptions;

public class OrderPaymentExceedsAmountDueException: Exception
{
    public OrderPaymentExceedsAmountDueException(OrderPaymentRequest payment)
        :base($"The payment amount {payment.Amount:C} exceeds the amount due.")
    {

    }
}
