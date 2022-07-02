using Events;
using MassTransit;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        private readonly IPublishEndpoint endpoint;

        public StockReservedEventConsumer(IPublishEndpoint endpoint)
        {
            this.endpoint = endpoint;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            var isPaymentSuccess = new Random().Next(0, 100) > 50;
            if (isPaymentSuccess)
            {
                PaymentCompletedEvent paymentCompletedEvent = new PaymentCompletedEvent { OrderId = context.Message.OrderId };
                await endpoint.Publish(paymentCompletedEvent);
            }
            else
            {
                PaymentFailedEvent paymentFailedEvent = new PaymentFailedEvent
                {
                    OrderId = context.Message.OrderId,
                    OrderItems = context.Message.OrderItemMessages,
                    Message = "Ödeme alınamadı"
                };

                await endpoint.Publish(paymentFailedEvent);
            }
        }
    }
}
