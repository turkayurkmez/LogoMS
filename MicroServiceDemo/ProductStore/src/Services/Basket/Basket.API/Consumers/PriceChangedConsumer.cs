using Basket.API.Services;
using Events;
using MassTransit;

namespace Basket.API.Consumers
{
    public class PriceChangedConsumer : IConsumer<ProductPriceChanged>
    {
        private BasketRepository basketRepository;
        private ILogger<PriceChangedConsumer> logger;

        public PriceChangedConsumer(BasketRepository basketRepository, ILogger<PriceChangedConsumer> logger)
        {
            this.basketRepository = basketRepository;
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<ProductPriceChanged> context)
        {

            logger.LogInformation("Event tetiklendi...");
            basketRepository.UpdatePrice(context.Message.ProductId, context.Message.NewPrice);
            return Task.CompletedTask;
        }
    }
}
