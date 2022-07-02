namespace Orders.API.Models
{
    public enum OrderStatus
    {
        Pending,
        Success,
        Failed,
        Canceled
    }
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }

    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
