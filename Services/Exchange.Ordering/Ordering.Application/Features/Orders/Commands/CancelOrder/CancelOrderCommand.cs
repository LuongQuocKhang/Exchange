using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
