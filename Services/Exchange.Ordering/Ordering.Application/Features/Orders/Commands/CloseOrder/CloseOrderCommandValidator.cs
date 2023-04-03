using FluentValidation;
using Ordering.Application.Features.Orders.Commands.PlaceOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CloseOrder
{
    public class CloseOrderCommandValidator : AbstractValidator<CloseOrderCommand>
    {
        public CloseOrderCommandValidator()
        {
            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0).WithMessage("Order Id {Id} must be greater than Zero ");
        }
    }
}
