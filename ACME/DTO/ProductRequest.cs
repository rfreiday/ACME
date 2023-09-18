namespace ACME.DTO;

public class ProductRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal UnitCost { get; set; }
    public decimal Price { get; set; }
    public bool Taxable { get; set; }
}
