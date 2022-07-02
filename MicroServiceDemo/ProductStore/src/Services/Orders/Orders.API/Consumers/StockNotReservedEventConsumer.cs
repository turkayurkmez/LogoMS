using Events;
using MassTransit;
using Orders.API.Data;
using Orders.API.Models;

namespace Orders.API.Consumers
{
    public class StockNotReservedEventConsumer : IConsumer<StockNotReserved>
    {
        private readonly OrdersDbContext ordersDbContext;

        public StockNotReservedEventConsumer(OrdersDbContext ordersDbContext)
        {
            this.ordersDbContext = ordersDbContext;
        }

        public async Task Consume(ConsumeContext<StockNotReserved> context)
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
