using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Common;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;

namespace Ordering.Application.Features.Orders.Queries.GetCurrentOpenOrders
{
    public class GetCurrentOpenOrdersQueryHandler : IRequestHandler<GetCurrentOpenOrdersQuery, List<OrdersViewModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetCurrentOpenOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrdersViewModel>> Handle(GetCurrentOpenOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAsync(x => x.OrderStatus == OrderStatus.FILLED.ToString() || 
                                                        x.OrderStatus == OrderStatus.PLACED.ToString());
            return _mapper.Map<List<OrdersViewModel>>(orders);
        }
    }
}
