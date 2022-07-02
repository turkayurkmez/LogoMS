using Events;
using MassTransit;
using Stocks.API.Data;

namespace Stocks.API.Consumer
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ISendEndpointProvider sendEndpointProvider;
        private readonly IPublishEndpoint publishEndpoint;
        private StockInMemory stockInMemory;

        public OrderCreatedEventConsumer(ISendEndpointProvider sendEndpoint, StockInMemory stockInMemory, IPublishEndpoint publishEndpoint)
        {
            this.sendEndpointProvider = sendEndpoint;
            this.stockInMemory = stockInMemory;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            List<bool> availableAllProducts  = new List<bool>();
            context.Message.OrderItemMessages.ForEach(orderItem => availableAllProducts.Add(stockInMemory.GetAvailableStocks(orderItem.ProductId, orderItem.Quantity)));

            if (availableAllProducts.TrueForAll(x=>x==true))
            {
                context.Message.OrderItemMessages.ForEach(orderItem => stockInMemory.ChangeStock(orderItem.ProductId, orderItem.Quantity));
                ISendEndpoint sendEndpoint = await sendEndpointProvider.GetSendEndpoint( new Uri($"queue:{QueueRoadMap.Payment_StockReservedEventQueue}"));
                StockReservedEvent stockReservedEvent = new StockReservedEvent
                {
                    CustomerId = context.Message.CustomerId,
                    OrderId = context.Message.OrderId,
                    OrderItemMessages = context.Message.OrderItemMessages,
                    TotalPrice = context.Message.TotalPrice
                };

                await sendEndpoint.Send(stockReservedEvent);
            }
            else
            {
                StockNotReserved stockNotReserved = new StockNotReserved
                {
                    CustomerId = context.Message.CustomerId,
                    Message = "Stok yetersiz",
                    OrderId = context.Message.OrderId

                };

                await publishEndpoint.Publish(stockNotReserved);
            }
        }
    }
}
