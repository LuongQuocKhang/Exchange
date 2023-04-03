using FluentValidation;
using Ordering.Application.Features.Orders.Commands.PlaceOrder;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator() {

            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0).WithMessage("Order Id {Id} must be greater than Zero ");

            RuleFor(p => p.Type)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{Type} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{Type} must not exceed 50 characters.");

            RuleFor(p => p.Side)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{Side} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{Side} must not exceed 50 characters.");

            RuleFor(p => p.Symbol)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{Symbol} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{Symbol} must not exceed 50 characters.");

            RuleFor(p => p.TimeInForce).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{TimeInForce} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{TimeInForce} must not exceed 50 characters.");

            RuleFor(p => p.Price).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{Price} is required")
                .GreaterThan(0).WithMessage("{Price} should be greater than zero.");

            RuleFor(p => p.Quantity).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{Price} is required")
                .GreaterThan(0).WithMessage("{Price} should be greater than zero.");
        }
    }
}
