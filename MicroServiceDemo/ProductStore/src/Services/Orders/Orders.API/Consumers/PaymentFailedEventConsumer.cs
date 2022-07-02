using Events;
using MassTransit;
using Orders.API.Data;
using Orders.API.Models;

namespace Orders.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly OrdersDbContext ordersDbContext;

        public PaymentFailedEventConsumer(OrdersDbContext ordersDbContext)
        {
            this.ordersDbContext = ordersDbContext;
        }

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            Order order = await ordersDbContext.Orders.FindAsync(context.Message.OrderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Failed;
                await ordersDbContext.SaveChangesAsync();
            }
        }
    }
}
