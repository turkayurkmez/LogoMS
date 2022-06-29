using Basket.API.Models;
using System.Collections.Concurrent;

namespace Basket.API.Services
{
    public class BasketRepository
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> _carts = new ConcurrentDictionary<string, ShoppingCart>();

        public ShoppingCart GetShoppingCart(string basketId)
        {
            addBasketIfNotExists(basketId);
            return _carts[basketId];
        }

        private void addBasketIfNotExists(string basketId)
        {
            if (!_carts.ContainsKey(basketId))
            {
                _carts.TryAdd(basketId, new ShoppingCart(basketId));
            }
        }

        internal void UpdatePrice(int productId, decimal? newPrice)
        {
            foreach (var cart in _carts)
            {
                _carts[cart.Key].ChangePrice(productId, newPrice);
            }
        }
    }
}
