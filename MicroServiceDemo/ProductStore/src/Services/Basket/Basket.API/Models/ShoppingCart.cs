namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public ShoppingCart(string userName)
        {
            Id = userName;
        }

        public string Id { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal Total => CartItems.Sum(x => (decimal)x.Quantity * x.Price.Value);

        public void AddItemToCart(CartItem item)
        {
            var existingItem = CartItems.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem == null)
            {
                CartItems.Add(item);
            }
            else
            {
                existingItem.Quantity += item.Quantity;
            }

        }

        public void ChangePrice(int productId, decimal? newPrice)
        {
           var item =  CartItems.Find(c => c.Id == productId);
            if (item != null)
            {
                item.Price = newPrice;
            }
        }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }



    }
}
