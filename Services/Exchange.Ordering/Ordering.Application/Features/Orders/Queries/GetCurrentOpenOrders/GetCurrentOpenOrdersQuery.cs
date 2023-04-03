using MediatR;
using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetCurrentOpenOrders
{
    public class GetCurrentOpenOrdersQuery : IRequest<List<OrdersViewModel>>
    {
    }
}
