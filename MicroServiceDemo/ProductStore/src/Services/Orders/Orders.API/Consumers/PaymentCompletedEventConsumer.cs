using Events;
using MassTransit;
using Orders.API.Data;
using Orders.API.Models;

namespace Orders.API.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly OrdersDbContext ordersDbContext;

        public PaymentCompletedEventConsumer(OrdersDbContext ordersDbContext)
        {
            this.ordersDbContext = ordersDbContext;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            Order order = await ordersDbContext.Orders.FindAsync(context.Message.OrderId);
            if (order!=null)
            {
                order.OrderStatus = OrderStatus.Success;
                await ordersDbContext.SaveChangesAsync();
            }
        }
    }
}
