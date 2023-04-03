using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0).WithMessage("Order Id {Id} must be greater than Zero ");
        }
    }
}
