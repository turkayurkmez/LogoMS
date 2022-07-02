using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class StockReservedEvent
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemMessage> OrderItemMessages { get; set; }

    }

    public class StockNotReserved
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string Message { get; set; }
    }
}
