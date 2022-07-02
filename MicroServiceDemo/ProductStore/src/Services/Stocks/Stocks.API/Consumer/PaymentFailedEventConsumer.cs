using Events;
using MassTransit;
using Stocks.API.Data;

namespace Stocks.API.Consumer
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly StockInMemory stockInMemory;

        public PaymentFailedEventConsumer(StockInMemory stockInMemory)
        {
            this.stockInMemory = stockInMemory;
        }

        public Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            context.Message.OrderItems.ForEach(x => stockInMemory.ChangeStock(x.ProductId, -x.Quantity));
            return Task.CompletedTask;
        }
    }
}
