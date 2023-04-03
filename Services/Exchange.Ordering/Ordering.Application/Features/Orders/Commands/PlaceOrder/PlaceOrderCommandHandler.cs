using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Common;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.PlaceOrder
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PlaceOrderCommandHandler> _logger;


        public PlaceOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<PlaceOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var placeOrder = _mapper.Map<TBL_ADM_ORDERS>(request);
            placeOrder.OrderStatus = OrderStatus.PLACED.ToString();

            // check user wallet money

            await _orderRepository.AddAsync(placeOrder);

            // update user wallet money

            _logger.LogInformation($"Order {placeOrder.Id} is successfully created.");

            return placeOrder.Id;
        }

    }
}
