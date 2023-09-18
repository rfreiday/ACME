namespace ACME.DTO;

public class OrderResponse
{
    private readonly List<OrderItemResponse> _items = new List<OrderItemResponse>();
    private readonly List<OrderPaymentResponse> _payments = new List<OrderPaymentResponse>();

    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int CustomerId { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Taxes { get; set; }
    public decimal Total { get; set; }
    public decimal TotalPayments { get; set; }
    public decimal AmountDue { get; set; }
    public bool PaidInFull { get; set; }
    public bool Overpaid { get; set; }
    public bool Shipped { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<OrderItemResponse> Items => _items;
    public List<OrderPaymentResponse> Payments => _payments;
}
