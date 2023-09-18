using ACME.DTO;
using FluentValidation;

namespace ACME.Validator;

public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
{
    public CustomerRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
    }
}
