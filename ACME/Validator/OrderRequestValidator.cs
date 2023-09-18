using ACME.DTO;
using FluentValidation;

namespace ACME.Validator;

public class OrderRequestValidator : AbstractValidator<OrderRequest>
{
    public OrderRequestValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
