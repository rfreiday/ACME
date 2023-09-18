namespace ACME.DTO;

public class OrderPaymentRequest
{
    public string PaymentType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
