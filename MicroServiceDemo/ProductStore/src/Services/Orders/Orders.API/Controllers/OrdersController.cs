using Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.API.Data;
using Orders.API.Models;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersDbContext ordersDbContext;
        private readonly IPublishEndpoint publishEndpoint;

        public OrdersController(OrdersDbContext ordersDbContext, IPublishEndpoint publishEndpoint)
        {
            this.ordersDbContext = ordersDbContext;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            Order order = new Order
            {
                CustomerId = createOrderRequest.CustomerId,
                CreatedDate = DateTime.Now,
                OrderItems = createOrderRequest.OrderItems.Select(x => new OrderItem { ProductId = x.ProductId, Price = x.Price, Quantity = x.Quantity }).ToList(),
                OrderStatus = OrderStatus.Pending,
                TotalPrice = createOrderRequest.OrderItems.Sum(x => x.Quantity * x.Price)

            };

            await ordersDbContext.Orders.AddAsync(order);
            await ordersDbContext.SaveChangesAsync();

            OrderCreatedEvent orderCreatedEvent = new OrderCreatedEvent
            {
                CustomerId = createOrderRequest.CustomerId,
                OrderId = order.Id,
                OrderItemMessages = order.OrderItems.Select(x => new OrderItemMessage { Price = x.Price, ProductId = x.ProductId, Quantity = x.Quantity }).ToList(),
                TotalPrice = order.TotalPrice
            };

            await publishEndpoint.Publish(orderCreatedEvent);
            return Ok();
        }
    }
}
