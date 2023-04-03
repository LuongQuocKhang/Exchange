using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class OrderNotPlacedException : ApplicationException
    {
        public OrderNotPlacedException(string name, object key)
            : base($"Order \"{name}\" ({key}) was not placed.")
        {
        }
    }
}
