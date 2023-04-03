using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Common;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Orders.Commands.CloseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        //private readonly IMapper _mapper;
        private readonly ILogger<CancelOrderCommandHandler> _logger;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CancelOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);
            if (order == null)
            {
                _logger.LogError($"Order {request.Id} not found.");
                throw new NotFoundException("Order", request.Id);
            }

            if(order.OrderStatus != OrderStatus.PLACED.ToString())
            {
                _logger.LogError($"Order {request.Id} is not placed.");
                throw new OrderNotPlacedException("Order", request.Id);
            }

            order.OrderStatus = OrderStatus.CANCELED.ToString();

            await _orderRepository.UpdateAsync(order);

            return order.Id;
        }
    }
}
