using MediatR;

namespace Ordering.Application.Features.Orders.Commands.CloseOrder
{
    public class CloseOrderCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
