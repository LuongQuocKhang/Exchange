using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Common
{
    public enum OrderStatus
    {
        PLACED = 1,
        FILLED = 2,
        CANCELED = 3,
        CLOSED = 4,
        DELETED = 5
    }
}
