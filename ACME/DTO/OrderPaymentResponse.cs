namespace ACME.DTO;

public class OrderPaymentResponse
{
    public string PaymentType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
