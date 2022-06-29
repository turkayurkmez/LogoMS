using Catalog.Business;
using Catalog.Business.DTOs.Requests;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductService productService;
        readonly IPublishEndpoint publishEndpoint;

        public ProductsController(IProductService productService, IPublishEndpoint publishEndpoint)
        {
            this.productService = productService;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(productService.GetProducts());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = productService.GetProductById(id);
            return Ok(product); 
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateProductRequest request)
        {
            if (productService.IsExists(id))
            {
                var priceChangedEvent = productService.Update(request);
                if (priceChangedEvent != null)
                {
                    publishEndpoint.Publish(priceChangedEvent);
                }
                return Ok();
            }

            return BadRequest();
        }

    }
}
