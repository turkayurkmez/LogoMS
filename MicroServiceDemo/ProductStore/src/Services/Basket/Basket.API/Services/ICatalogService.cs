using Basket.API.Models;

namespace Basket.API.Services
{
    public interface ICatalogService
    {
        Task<ProductResponse> GetProduct(int id);
    }
}
