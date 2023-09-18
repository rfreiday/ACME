using ACME.Validator;
using FluentValidation.TestHelper;

namespace ACME.Test.Validator;

public class ProductRequestValidatorTest
{
    private ProductRequestValidator validator = new ProductRequestValidator();

    [Fact]
    public void ProductRequest_Valid()
    {
        var Product = new ProductRequest()
        {
            Description = "description",
            Price = 100,
            UnitCost = 50
        };
        var result = validator.TestValidate(Product);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ProductRequest_DescriptionMissing()
    {
        var Product = new ProductRequest()
        {
            Price = 100,
            UnitCost = 50
        };
        var result = validator.TestValidate(Product);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void ProductRequest_PriceMissing()
    {
        var Product = new ProductRequest()
        {
            Description = "description",
            UnitCost = 50
        };
        var result = validator.TestValidate(Product);
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void ProductRequest_UnitCostMissing()
    {
        var Product = new ProductRequest()
        {
            Description = "description",
            Price = 100
        };
        var result = validator.TestValidate(Product);
        result.ShouldHaveValidationErrorFor(x => x.UnitCost);
    }
}