using ACME.DTO;

namespace ACME.Exceptions;

public class OrderPaymentExceedsAmountDue : Exception
{
    public OrderPaymentExceedsAmountDue(OrderPaymentRequest orderPayment)
        : base($"The payment amount {orderPayment.Amount:C} exceeds the amount due")
    {
    }
}
