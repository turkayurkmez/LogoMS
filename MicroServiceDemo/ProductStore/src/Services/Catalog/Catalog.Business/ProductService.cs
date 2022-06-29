using Catalog.Business.DTOs.Requests;
using Catalog.Business.DTOs.Responses;
using Catalog.DataAccess.Repositories;
using Catalog.Entities;
using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ProductResponse GetProductById(int id)
        {
            var product = repository.Get(id);
            return new ProductResponse { CategoryId = product.CategoryId, Description = product.Description, Id = product.Id, ImageUrl = product.ImageUrl, Name = product.Name, Price = product.Price };

        }

        public IEnumerable<ProductResponse> GetProducts()
        {
            var products = repository.GetAll();
            return products.Select(x => new ProductResponse { CategoryId = x.CategoryId, Description = x.Description, Price = x.Price, Id = x.Id, Name = x.Name, ImageUrl = x.ImageUrl });


        }

        public bool IsExists(int id)
        {
            return repository.IsExists(id);
        }

        public ProductPriceChanged Update(UpdateProductRequest request)
        {
            ProductPriceChanged priceChangedEvent = null;
            var existing = repository.Get(request.Id);

            if (existing.Price != request.Price)
            {
                priceChangedEvent = new ProductPriceChanged(request.Id, existing.Price, request.Price);
            }

            var product = new Product { Id = request.Id, Price = request.Price, CategoryId = request.CategoryId, Description = request.Description, Name = request.Name, ImageUrl = request.ImageUrl };

            repository.Update(product);
            return priceChangedEvent;
        }
    }
}
