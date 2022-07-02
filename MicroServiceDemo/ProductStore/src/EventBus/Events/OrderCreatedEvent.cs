using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemMessage> OrderItemMessages { get; set; }
    }

    public class OrderItemMessage
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
