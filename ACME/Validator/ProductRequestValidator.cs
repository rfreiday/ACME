using FluentValidation;
using ACME.DTO;

namespace ACME.Validator;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.UnitCost).GreaterThan(0);
    }
}