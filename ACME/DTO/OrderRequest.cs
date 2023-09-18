namespace ACME.DTO;

public class OrderRequest
{
    public DateTime DateTime => DateTime.Now;

    public int CustomerId { get; set; }

    public virtual ICollection<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();

    public virtual ICollection<OrderPaymentRequest> OrderPayments { get; set; } = new List<OrderPaymentRequest>();
}
