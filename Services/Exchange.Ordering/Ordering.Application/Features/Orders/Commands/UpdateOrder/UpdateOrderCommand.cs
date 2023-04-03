using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public string Type { get; set; }
        public string TimeInForce { get; set; }
        public decimal Quantity { get; set; }
        public string QuoteOrderQty { get; set; }
        public decimal Price { get; set; }
        public decimal StopPrice { get; set; } // Used with STOP_LOSS, STOP_LOSS_LIMIT, TAKE_PROFIT, and TAKE_PROFIT_LIMIT orders.
        public long TrailingDelta { get; set; }
    }
}
