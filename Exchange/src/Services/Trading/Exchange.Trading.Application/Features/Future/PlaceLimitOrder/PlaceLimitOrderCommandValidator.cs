using FluentValidation;

namespace Exchange.Trading.Application.Features.Future.PlaceLimitOrder;

internal class PlaceLimitOrderCommandValidator : AbstractValidator<PlaceLimitOrderCommand>
{
    public PlaceLimitOrderCommandValidator()
    {
        RuleFor(x => x.Symbol)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Symbol is required");

        RuleFor(x => x.Amount)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");

        RuleFor(x => x.TargetPrice)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("Target price must be greater than 0");

        RuleFor(x => x.Position)
            .Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithMessage("Invalid position");
    }
}
