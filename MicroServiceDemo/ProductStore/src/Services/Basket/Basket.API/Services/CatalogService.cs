using Basket.API.Models;
using Microsoft.Extensions.Options;

namespace Basket.API.Services
{
    public class CatalogService : ICatalogService
    {

        private readonly HttpClient httpClient;
        private string baseUrl = "http://localhost:5200/";
        string productApi = "api/products";
        string productsUrl = string.Empty;



        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            baseUrl = settings.Value.CatalogAPI;
            this.httpClient = httpClient;
            productsUrl = $"{baseUrl}{productApi}";
        }

        public async Task<ProductResponse> GetProduct(int id)
        {
            productsUrl = $"{baseUrl}{productApi}/{id}";
            return await  httpClient.GetFromJsonAsync<ProductResponse>(productsUrl);
        }
    }
}
