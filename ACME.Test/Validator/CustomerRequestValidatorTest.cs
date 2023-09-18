using ACME.DTO;
using ACME.Validator;
using FluentValidation.TestHelper;

namespace ACME.Test.Validator;

public class CustomerRequestValidatorTest
{
    private CustomerRequestValidator validator = new CustomerRequestValidator();

    [Fact]
    public void CustomerRequest_Valid()
    {
        var customer = new CustomerRequest()
        {
            Name = "Test",
            Email = "a@b.com"
        };
        var result = validator.TestValidate(customer);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void CustomerRequest_NameMissing()
    {
        var customer = new CustomerRequest()
        {
            Email = "a@b.com"
        };
        var result = validator.TestValidate(customer);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void CustomerRequest_EmailMissing()
    {
        var customer = new CustomerRequest()
        {
            Name = "Test"
        };
        var result = validator.TestValidate(customer);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void CustomerRequest_EmailInvalid()
    {
        var customer = new CustomerRequest()
        {
            Name = "Test",
            Email = "Invalid"
        };
        var result = validator.TestValidate(customer);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
}
