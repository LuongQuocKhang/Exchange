using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    [Table("TBL_ADM_ORDERS")]
    public class TBL_ADM_ORDERS : EntityBase
    {
        public string Symbol { get; set; }
        public string Side { get; set; }
        public string Type { get; set; }
        public string TimeInForce { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuoteOrderQty { get; set; }
        public decimal Price { get; set; }
        public decimal StopPrice { get; set; } // Used with STOP_LOSS, STOP_LOSS_LIMIT, TAKE_PROFIT, and TAKE_PROFIT_LIMIT orders.
        public long TrailingDelta { get; set; }
        public string OrderStatus { get; set; }
        public decimal PNLPercent { get; set; }
        public decimal PNLPrice { get; set; }
    }
}
