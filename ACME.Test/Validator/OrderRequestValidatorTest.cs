using ACME.DTO;
using ACME.Validator;
using FluentValidation.TestHelper;

namespace ACME.Test.Validator;

public class OrderRequestValidatorTest
{
    private OrderRequestValidator validator = new OrderRequestValidator();

    [Fact]
    public void OrderRequest_Valid()
    {
        var order = new OrderRequest()
        {
            CustomerId = 1
        };
        var result = validator.TestValidate(order);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void OrderRequest_CustomerIdMissing()
    {
        var order = new OrderRequest();
        var result = validator.TestValidate(order);
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }
}
