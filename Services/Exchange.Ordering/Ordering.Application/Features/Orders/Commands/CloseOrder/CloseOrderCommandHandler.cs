using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Common;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Orders.Commands.PlaceOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CloseOrder
{
    public class CloseOrderCommandHandler : IRequestHandler<CloseOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        //private readonly IMapper _mapper;
        private readonly ILogger<CloseOrderCommandHandler> _logger;


        public CloseOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CloseOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            //_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CloseOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);
            if(order == null)
            {
                _logger.LogError($"Order {request.Id} not found.");
                throw new NotFoundException("Order", request.Id);
            }

            if(order.OrderStatus != OrderStatus.FILLED.ToString())
            {
                _logger.LogError($"Order {request.Id} is not filled.");
                throw new OrderNotFilledException("Order", request.Id);
            }

            order.OrderStatus = OrderStatus.CLOSED.ToString();

            await _orderRepository.UpdateAsync(order);

            return order.Id;
        }
    }
}
