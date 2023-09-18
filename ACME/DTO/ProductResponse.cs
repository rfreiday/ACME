namespace ACME.DTO;

public class ProductResponse
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal UnitCost { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public bool Taxable { get; set; }
}
