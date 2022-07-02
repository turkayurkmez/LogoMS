using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class PaymentCompletedEvent
    {
        public int OrderId { get; set; }
    }

    public class PaymentFailedEvent
    {
        public int OrderId { get; set; }
        public string Message { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}
