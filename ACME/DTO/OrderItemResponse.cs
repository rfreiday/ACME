namespace ACME.DTO;

public class OrderItemResponse
{
    public int ProductId { get; set; }
    public string ProductDescription { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}
