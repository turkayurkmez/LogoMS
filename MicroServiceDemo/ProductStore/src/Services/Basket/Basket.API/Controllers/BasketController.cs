using Basket.API.Models;
using Basket.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        readonly ICatalogService catalogService;
        readonly BasketRepository basketRepository;

        public BasketController(ICatalogService catalogService, BasketRepository basketRepository)
        {
            this.catalogService = catalogService;
            this.basketRepository = basketRepository;
        }

        [HttpPost("{basketId}/items")]
        public async Task<IActionResult> AddItem(string basketId, AddItemRequest addItemRequest)
        {
            var product = await catalogService.GetProduct(addItemRequest.Id);
            if (product == null)
            {
                return BadRequest("Ürün bulunamadı");
            }

            var shoppingCart = basketRepository.GetShoppingCart(basketId);
            shoppingCart.AddItemToCart(new CartItem { Id = addItemRequest.Id, Quantity = addItemRequest.Quantity, Name = product.Name, Price = product.Price });

            return Ok(new { message= "Ürün sepete eklendi" });
        }

        [HttpGet("{basketId}")]
        public async Task<IActionResult> GetBasket(string basketId)
        {
            var cart = basketRepository.GetShoppingCart(basketId);
            return Ok(cart);
        }
    }
}
